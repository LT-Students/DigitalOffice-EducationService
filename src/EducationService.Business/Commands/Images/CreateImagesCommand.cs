using LT.DigitalOffice.EducationService.Business.Commands.Image.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.EducationService.Validation.Image.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
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

namespace LT.DigitalOffice.UserService.Business.Commands.Image
{
  public class CreateImagesCommand : ICreateImagesCommand
  {
    private readonly IImageRepository _repository;
    private readonly IRequestClient<ICreateImagesRequest> _rcImages;
    private readonly ILogger<CreateImagesCommand> _logger;
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEducationRepository _educationRepository;
    private readonly IResponseCreator _responseCreator;
    private readonly ICreateImageDataMapper _mapper;
    private readonly IDbEducationImageMapper _imageMapper;
    private readonly ICreateImagesRequestValidator _validator;

    private async Task<List<Guid>> CreateAsync(List<ImageContent> images, Guid certificateId, List<string> errors)
    {
      if (images is null || !images.Any())
      {
        return null;
      }

      try
      {
        Response<IOperationResult<ICreateImagesResponse>> response = await
          _rcImages.GetResponse<IOperationResult<ICreateImagesResponse>>(
            ICreateImagesRequest.CreateObj(_mapper.Map(images), ImageSource.User));

        if (response.Message.IsSuccess && response.Message.Body.ImagesIds is not null)
        {
          return response.Message.Body.ImagesIds;
        }

        _logger.LogWarning(
          "Errors while creating images for education id {EducationId}.\nErrors: {Errors}",
          certificateId,
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(
          exc,
          "Cannot create images for education id {EducationId}.",
          certificateId);
      }

      errors.Add("Cannot create images. Please try again later.");

      return null;
    }

    public CreateImagesCommand(
      IImageRepository repository,
      IRequestClient<ICreateImagesRequest> rcImages,
      ILogger<CreateImagesCommand> logger,
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IEducationRepository educationRepository,
      IResponseCreator responseCreator,
      ICreateImageDataMapper mapper,
      IDbEducationImageMapper imageMapper,
      ICreateImagesRequestValidator validator)
    {
      _repository = repository;
      _rcImages = rcImages;
      _logger = logger;
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _educationRepository = educationRepository;
      _responseCreator = responseCreator;
      _mapper = mapper;
      _imageMapper = imageMapper;
      _validator = validator;
    }

    public async Task<OperationResultResponse<List<Guid>>> ExecuteAsync(CreateImagesRequest request)
    {
      Guid senderId = _httpContextAccessor.HttpContext.GetUserId();

      if (senderId != (await _educationRepository.GetAsync(request.EducationId)).UserId
        && !await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
      {
        return _responseCreator.CreateFailureResponse<List<Guid>>(HttpStatusCode.Forbidden);
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<List<Guid>>(HttpStatusCode.BadRequest, errors);
      }

      OperationResultResponse<List<Guid>> response = new();

      List<Guid> imagesIds = await CreateAsync(
        request.Images,
        request.EducationId,
        response.Errors);

      if (response.Errors.Any())
      {
        return _responseCreator.CreateFailureResponse<List<Guid>>(
          HttpStatusCode.BadRequest,
          response.Errors);
      }

      response.Body = await _repository.CreateAsync(imagesIds.Select(imageId =>
        _imageMapper.Map(imageId, request.EducationId))
        .ToList());

      response.Status = OperationResultStatusType.FullSuccess;
      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return response;
    }
  }
}
