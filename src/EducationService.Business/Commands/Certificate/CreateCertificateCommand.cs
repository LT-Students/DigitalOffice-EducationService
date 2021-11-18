using LT.DigitalOffice.EducationService.Business.Commands.Certificate.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.EducationService.Validation.Certificates.Interfaces;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
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

namespace LT.DigitalOffice.EducationService.Business.Commands.Certificate
{
  public class CreateCertificateCommand : ICreateCertificateCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICertificateRepository _certificateRepository;
    private readonly IDbUserCertificateMapper _mapper;
    private readonly ICreateImageDataMapper _createImageDataMapper;
    private readonly IRequestClient<ICreateImagesRequest> _rcImage;
    private readonly ILogger<CreateCertificateCommand> _logger;
    private readonly IResponseCreater _responseCreator;
    private readonly ICreateCertificateRequestValidator _validator;

    private async Task<List<Guid>> CreateImagesAsync(List<ImageContent> images, List<string> errors)
    {
      if (images is null)
      {
        return null;
      }

      string errorMessage = "Can not add certificate images to certificate. Please try again later.";

      try
      {
        Response<IOperationResult<ICreateImagesResponse>> response = await _rcImage.GetResponse<IOperationResult<ICreateImagesResponse>>(
          ICreateImagesRequest.CreateObj(
            _createImageDataMapper.Map(images), ImageSource.User));

        if (response.Message.IsSuccess && response.Message.Body.ImagesIds != null)
        {
          return response.Message.Body.ImagesIds;
        }

        _logger.LogWarning(
          errorMessage + "Reason:\n{Errors}",
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, errorMessage);
      }

      errors.Add(errorMessage);

      return null;
    }

    public CreateCertificateCommand(
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IDbUserCertificateMapper mapper,
      ICertificateRepository certificateRepository,
      IRequestClient<ICreateImagesRequest> rcAddIImage,
      ICreateImageDataMapper createImageDataMapper,
      ILogger<CreateCertificateCommand> logger,
      IResponseCreater responseCreator,
      ICreateCertificateRequestValidator validator)
    {
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _mapper = mapper;
      _certificateRepository = certificateRepository;
      _rcImage = rcAddIImage;
      _logger = logger;
      _createImageDataMapper = createImageDataMapper;
      _responseCreator = responseCreator;
      _validator = validator;
    }

    public async Task<OperationResultResponse<Guid>> ExecuteAsync(CreateCertificateRequest request)
    {
      if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers)
        && _httpContextAccessor.HttpContext.GetUserId() != request.UserId)
      {
        return _responseCreator.CreateFailureResponse<Guid>(HttpStatusCode.Forbidden);
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<Guid>(HttpStatusCode.BadRequest, errors);
      }

      List<Guid> imagesId = await CreateImagesAsync(request?.Images, errors);

      if (errors.Any())
      {
        return _responseCreator.CreateFailureResponse<Guid>(HttpStatusCode.BadRequest, errors);
      }

      DbUserCertificate dbUserCertificate = _mapper.Map(request, imagesId);

      await _certificateRepository.CreateAsync(dbUserCertificate);

      return new OperationResultResponse<Guid>
      {
        Status = OperationResultStatusType.FullSuccess,
        Body = dbUserCertificate.Id
      };
    }
  }
}
