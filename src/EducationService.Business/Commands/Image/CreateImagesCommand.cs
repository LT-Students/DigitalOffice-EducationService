using FluentValidation.Results;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using LT.DigitalOffice.EducationService.Business.Commands.Image.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;

namespace LT.DigitalOffice.UserService.Business.Commands.Image
{
  public class CreateImagesCommand : ICreateImagesCommand
  {
    private readonly IImageRepository _repository;
    private readonly IRequestClient<ICreateImagesRequest> _rcImages;
    private readonly ILogger<CreateImagesCommand> _logger;
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICertificateRepository _certificateRepository;
    private readonly IResponseCreater _responseCreator;
    private readonly ICreateImageDataMapper _mapper;
    private readonly IDbCertificateImageMapper _imageMapper;

    private async Task<List<Guid>> CreateAsync(List<ImageContent> images, Guid certificateId, List<string> errors)
    {
      string logMessage = $"Errors while creating images for certificate id {certificateId}.";

      try
      {
        Response<IOperationResult<ICreateImagesResponse>> response = await
          _rcImages.GetResponse<IOperationResult<ICreateImagesResponse>>(
            ICreateImagesRequest.CreateObj(_mapper.Map(images), ImageSource.User));

        if (response.Message.IsSuccess && response.Message.Body.ImagesIds != null)
        {
          return response.Message.Body.ImagesIds;
        }

        _logger.LogWarning(
          logMessage + "Errors: { Errors}",
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, logMessage);
      }

      errors.Add("Can not create images. Please try again later.");

      return null;
    }

    public CreateImagesCommand(
      IImageRepository repository,
      IRequestClient<ICreateImagesRequest> rcImages,
      ILogger<CreateImagesCommand> logger,
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      ICertificateRepository certificateRepository,
      IResponseCreater responseCreator,
      ICreateImageDataMapper mapper,
      IDbCertificateImageMapper imageMapper)
    {
      _repository = repository;
      _rcImages = rcImages;
      _logger = logger;
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _certificateRepository = certificateRepository;
      _responseCreator = responseCreator;
      _mapper = mapper;
      _imageMapper = imageMapper;
    }

    public async Task<OperationResultResponse<List<Guid>>> ExecuteAsync(CreateImagesRequest request)
    {
      Guid senderId = _httpContextAccessor.HttpContext.GetUserId();

      if (senderId != (await _certificateRepository.GetAsync(request.CertificateId)).UserId
        && !await _accessValidator.HasRightsAsync(senderId, Rights.AddEditRemoveUsers))
      {
        return _responseCreator.CreateFailureResponse<List<Guid>> (HttpStatusCode.Forbidden);
      }

      /*if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return new OperationResultResponse<List<Guid>>
        {
          Status = OperationResultStatusType.Failed,
          Errors = errors
        };
      }*/

      OperationResultResponse<List<Guid>> response = new();

      List<Guid> imagesIds = await CreateAsync(
        request.Images,
        request.CertificateId,
        response.Errors);

      if (response.Errors.Any())
      {
        response.Status = OperationResultStatusType.Failed;
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return response;
      }

      response.Body = await _repository.CreateAsync(imagesIds.Select(imageId =>
        _imageMapper.Map(imageId, request.CertificateId))
        .ToList());

      response.Status = OperationResultStatusType.FullSuccess;
      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return response;
    }
  }
}
