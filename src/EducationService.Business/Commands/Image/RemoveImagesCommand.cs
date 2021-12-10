using FluentValidation.Results;
using LT.DigitalOffice.EducationService.Business.Commands.Image.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.EducationService.Validation.Image.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.Image
{
  public class RemoveImagesCommand : IRemoveImagesCommand
  {
    private readonly IImageRepository _imageRepository;
    private readonly IUserCertificateRepository _certificateRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRequestClient<IRemoveImagesRequest> _rcRemoveImages;
    private readonly IRemoveImagesRequestValidator _removeRequestValidator;
    private readonly IAccessValidator _accessValidator;
    private readonly ILogger<RemoveImagesCommand> _logger;
    private readonly IResponseCreator _responseCreator;

    private async Task<bool> RemoveAsync(List<Guid> imagesIds, List<string> errors)
    {
      try
      {
        Response<IOperationResult<bool>> response =
          await _rcRemoveImages.GetResponse<IOperationResult<bool>>(
            IRemoveImagesRequest.CreateObj(imagesIds, ImageSource.User));

        if (response.Message.IsSuccess)
        {
          return response.Message.Body;
        }

        _logger.LogWarning(
          "Errors while removing images with ids: {ImagesIds}.\n Errors: {Errors}",
          string.Join(", ", imagesIds),
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception ex)
      {
        _logger.LogError(
          ex,
          "Cannot remove images with ids: {ImagesIds}.",
          string.Join(", ", imagesIds));
      }

      errors.Add("Cannot remove images. Please try again later.");

      return false;
    }

    public RemoveImagesCommand(
      IImageRepository imageRepository,
      IUserCertificateRepository certificateRepository,
      IHttpContextAccessor httpContextAccessor,
      IRequestClient<IRemoveImagesRequest> rcRemoveImages,
      IRemoveImagesRequestValidator removeRequestValidator,
      IAccessValidator accessValidator,
      ILogger<RemoveImagesCommand> logger,
      IResponseCreator responseCreator)
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

      if (senderId != (await _certificateRepository.GetAsync(request.CerificateId)).UserId
        && !await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
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
