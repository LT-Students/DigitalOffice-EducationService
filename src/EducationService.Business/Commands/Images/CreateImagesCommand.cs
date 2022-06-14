using LT.DigitalOffice.EducationService.Business.Commands.Image.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.EducationService.Validation.Image.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Publishing.Subscriber.Image;
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
    private readonly IRequestClient<ICreateImagesPublish> _rcImage;
    private readonly ILogger<CreateImagesCommand> _logger;
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEducationRepository _educationRepository;
    private readonly IResponseCreator _responseCreator;
    private readonly ICreateImageDataMapper _mapper;
    private readonly IDbEducationImageMapper _imageMapper;
    private readonly ICreateImagesRequestValidator _validator;

    private async Task<List<Guid>> CreateAsync(List<ImageContent> images, List<string> errors)
    {
      if (images is null || !images.Any())
      {
        return null;
      }

      return (await RequestHandler.ProcessRequest<ICreateImagesPublish, ICreateImagesResponse>(
         _rcImage,
         ICreateImagesPublish.CreateObj(_mapper.Map(images), ImageSource.User),
         errors,
         _logger))
       .ImagesIds;
    }

    public CreateImagesCommand(
      IImageRepository repository,
      IRequestClient<ICreateImagesPublish> rcImage,
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
      _rcImage = rcImage;
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

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return response;
    }
  }
}
