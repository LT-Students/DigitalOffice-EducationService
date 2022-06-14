using FluentValidation.Results;
using LT.DigitalOffice.EducationService.Broker.Publishes.Interfaces;
using LT.DigitalOffice.EducationService.Business.Commands.Image.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.EducationService.Validation.Image.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.Image
{
  public class RemoveImagesCommand : IRemoveImagesCommand
  {
    private readonly IImageRepository _imageRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRemoveImagesRequestValidator _removeRequestValidator;
    private readonly IAccessValidator _accessValidator;
    private readonly ILogger<RemoveImagesCommand> _logger;
    private readonly IResponseCreator _responseCreator;
    private readonly IPublish _publish;

    public RemoveImagesCommand(
      IImageRepository imageRepository,
      IEducationRepository educationRepository,
      IHttpContextAccessor httpContextAccessor,
      IRemoveImagesRequestValidator removeRequestValidator,
      IAccessValidator accessValidator,
      ILogger<RemoveImagesCommand> logger,
      IResponseCreator responseCreator,
      IPublish publish)
    {
      _imageRepository = imageRepository;
      _educationRepository = educationRepository;
      _httpContextAccessor = httpContextAccessor;
      _removeRequestValidator = removeRequestValidator;
      _accessValidator = accessValidator;
      _logger = logger;
      _responseCreator = responseCreator;
      _publish = publish;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(RemoveImagesRequest request)
    {
      OperationResultResponse<bool> response = new();

      Guid senderId = _httpContextAccessor.HttpContext.GetUserId();

      if (senderId != (await _educationRepository.GetAsync(request.EducationId)).UserId
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
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest);
      }

      await Task.WhenAll(
        request.ImagesIds.Count > 0
          ? _publish.RemoveImagesAsync(
            imagesIds: request.ImagesIds,
            imageSource: ImageSource.User)
          : Task.CompletedTask);

      response.Status = response.Errors.Any()
        ? OperationResultStatusType.PartialSuccess
        : OperationResultStatusType.FullSuccess;

      return response;
    }
  }
}
