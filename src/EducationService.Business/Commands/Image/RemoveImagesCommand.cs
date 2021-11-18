using FluentValidation.Results;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.EducationService.Business.Commands.Image.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.EducationService.Validation.Image.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;

namespace LT.DigitalOffice.EducationService.Business.Commands.Image
{
  public class RemoveImagesCommand : IRemoveImagesCommand
  {
    private readonly IImageRepository _imageRepository;
    private readonly ICertificateRepository _certificateRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRequestClient<IRemoveImagesRequest> _rcRemoveImages;
    private readonly IRemoveImagesRequestValidator _removeRequestValidator;
    private readonly IAccessValidator _accessValidator;
    private readonly ILogger<RemoveImagesCommand> _logger;
    private readonly IResponseCreater _responseCreator;

    private async Task<bool> RemoveAsync(List<Guid> imagesIds, List<string> errors)
    {
      try
      {
        Response<IOperationResult<bool>> removeResponse =
          await _rcRemoveImages.GetResponse<IOperationResult<bool>>(
            IRemoveImagesRequest.CreateObj(imagesIds, ImageSource.User));

        if (removeResponse.Message.IsSuccess)
        {
          return removeResponse.Message.Body;
        }

        _logger.LogWarning(
          "Errors while removing images with ids: {ImagesIds}. Errors: {Errors}",
          string.Join(", ", imagesIds),
          string.Join('\n', removeResponse.Message.Errors));
      }
      catch (Exception e)
      {
        _logger.LogError(e, "Errors while removing images with ids: {ImagesIds}.", string.Join(", ", imagesIds));
      }

      errors.Add("Can't remove images. Please try again later.");

      return false;
    }

    public RemoveImagesCommand(
      IImageRepository imageRepository,
      ICertificateRepository certificateRepository,
      IHttpContextAccessor httpContextAccessor,
      IRequestClient<IRemoveImagesRequest> rcRemoveImages,
      IRemoveImagesRequestValidator removeRequestValidator,
      IAccessValidator accessValidator,
      ILogger<RemoveImagesCommand> logger,
      IResponseCreater responseCreator)
    {
      _imageRepository = imageRepository;
      _certificateRepository = certificateRepository;
      _httpContextAccessor = httpContextAccessor;
      _rcRemoveImages = rcRemoveImages;
      _removeRequestValidator = removeRequestValidator;
      _accessValidator = accessValidator;
      _logger = logger;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(RemoveImagesRequest request)
    {
      OperationResultResponse<bool> response = new();

      Guid senderId = _httpContextAccessor.HttpContext.GetUserId();

      if (!await _accessValidator.HasRightsAsync(senderId, Rights.AddEditRemoveUsers)
        && senderId != (await _certificateRepository.GetAsync(request.CerificateId)).UserId)
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      ValidationResult validationResult = await _removeRequestValidator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<bool>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(validationFailure => validationFailure.ErrorMessage).ToList());
      }

      response.Body = await _imageRepository.RemoveAsync(request.ImagesIds);

      if (!response.Body)
      {
        await RemoveAsync(request.ImagesIds, response.Errors);
      }

      response.Status = response.Errors.Any()
        ? OperationResultStatusType.PartialSuccess
        : OperationResultStatusType.FullSuccess;

      return response;
    }
  }
}
