using LT.DigitalOffice.EducationService.Business.Commands.Education.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.EducationService.Validation.Education.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.Education
{
  public class CreateEducationCommand : ICreateEducationCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IDbUserEducationMapper _mapper;
    private readonly IEducationRepository _educationRepository;
    private readonly ICreateEducationRequestValidator _validator;
    private readonly IResponseCreator _responseCreator;
    private readonly ICreateImageDataMapper _createImageDataMapper;
    private readonly IRequestClient<ICreateImagesRequest> _rcImage;
    private readonly ILogger<CreateEducationCommand> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private async Task<List<Guid>> CreateImagesAsync(List<ImageContent> images, List<string> errors)
    {
      if (images is null || !images.Any())
      {
        return null;
      }

      try
      {
        Response<IOperationResult<ICreateImagesResponse>> response = await _rcImage.GetResponse<IOperationResult<ICreateImagesResponse>>(
          ICreateImagesRequest.CreateObj(
            _createImageDataMapper.Map(images), ImageSource.User));

        if (response.Message.IsSuccess && response.Message.Body.ImagesIds is not null)
        {
          return response.Message.Body.ImagesIds;
        }

        _logger.LogWarning(
          "Error while adding images to education.\nErrors: {Errors}",
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Cannot add images to education.");
      }

      errors.Add("Can not add images to education. Please try again later.");

      return null;
    }

    public CreateEducationCommand(
      IAccessValidator accessValidator,
      IDbUserEducationMapper mapper,
      IEducationRepository educationRepository,
      ICreateEducationRequestValidator validator,
      IResponseCreator responseCreator,
      IHttpContextAccessor httpContextAccessor,
      IRequestClient<ICreateImagesRequest> rcImage,
      ICreateImageDataMapper createImageDataMapper,
      ILogger<CreateEducationCommand> logger)
    {
      _accessValidator = accessValidator;
      _mapper = mapper;
      _educationRepository = educationRepository;
      _validator = validator;
      _responseCreator = responseCreator;
      _httpContextAccessor = httpContextAccessor;
      _rcImage = rcImage; //
      _createImageDataMapper = createImageDataMapper; //
      _logger = logger; //

    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateEducationRequest request)
    {
      if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers)
        && _httpContextAccessor.HttpContext.GetUserId() != request.UserId)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.Forbidden);
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest, errors);
      }

      List<Guid> imagesIds = await CreateImagesAsync(request.Images, errors);

      if (errors.Any())
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest, errors);
      }

      OperationResultResponse<Guid?> response = new();

      response.Body = await _educationRepository.CreateAsync(_mapper.Map(request, imagesIds));

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return response;
    }
  }
}
