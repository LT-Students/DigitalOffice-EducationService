using LT.DigitalOffice.EducationService.Business.Commands.EducationType.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.EducationService.Validation.Education.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.EducationType
{
  public class CreateEducationTypeCommand : ICreateEducationTypeCommand
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDbEducationTypeMapper _mapper;
    private readonly IEducationTypeRepository _educationTypeRepository;
    private readonly ICreateEducationTypeRequestValidator _validator;
    private readonly IAccessValidator _accessValidator;
    private readonly IResponseCreator _responseCreator;

    public CreateEducationTypeCommand(
      IHttpContextAccessor httpContextAccessor,
      IDbEducationTypeMapper mapper,
      IEducationTypeRepository educationTypeRepository,
      ICreateEducationTypeRequestValidator validator,
      IAccessValidator accessValidator,
      IResponseCreator responseCreator)
    {
      _httpContextAccessor = httpContextAccessor;
      _mapper = mapper;
      _educationTypeRepository = educationTypeRepository;
      _validator = validator;
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateEducationTypeRequest request)
    {
      OperationResultResponse<Guid?> response = new();

      if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.Forbidden);
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest, errors);
      }

      response.Body = await _educationTypeRepository.CreateAsync(_mapper.Map(request));

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      if (response.Body == default)
      {
        response = _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
      }

      return response;
    }
  }
}
