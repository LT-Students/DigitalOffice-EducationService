using LT.DigitalOffice.EducationService.Broker.Requests.Interfaces;
using LT.DigitalOffice.EducationService.Business.Commands.Image.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.EducationService.Validation.Image.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
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
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEducationRepository _educationRepository;
    private readonly IResponseCreator _responseCreator;
    private readonly IDbEducationImageMapper _imageMapper;
    private readonly ICreateImagesRequestValidator _validator;
    private readonly IImageService _imageService;

    public CreateImagesCommand(
      IImageRepository repository,
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IEducationRepository educationRepository,
      IResponseCreator responseCreator,
      IDbEducationImageMapper imageMapper,
      ICreateImagesRequestValidator validator,
      IImageService imageService)
    {
      _repository = repository;
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _educationRepository = educationRepository;
      _responseCreator = responseCreator;
      _imageMapper = imageMapper;
      _validator = validator;
      _imageService = imageService;
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

      List<Guid> imagesIds = await _imageService.CreateImagesAsync(
        request.Images,
        response.Errors);

      if (response.Errors.Any() || imagesIds is null || !imagesIds.Any())
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
