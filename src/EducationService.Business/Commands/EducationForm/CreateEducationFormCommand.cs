using LT.DigitalOffice.EducationService.Business.Commands.EducationForm.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.EducationService.Validation.Education.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.EducationForm
{
  public class CreateEducationFormCommand : ICreateEducationFormCommand
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDbEducationFormMapper _mapper;
    private readonly IEducationFormRepository _educationFormRepository;
    private readonly ICreateEducationFormRequestValidator _validator;
    private readonly IAccessValidator _accessValidator;
    private readonly IResponseCreator _responseCreator;


    public CreateEducationFormCommand(
      IHttpContextAccessor httpContextAccessor,
      IDbEducationFormMapper mapper,
      IEducationFormRepository educationFormRepository,
      ICreateEducationFormRequestValidator validator,
      IAccessValidator accessValidator,
      IResponseCreator responseCreator)
    {
      _httpContextAccessor = httpContextAccessor;
      _mapper = mapper;
      _educationFormRepository = educationFormRepository;
      _validator = validator;
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateEducationFormRequest request)
    {
      if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.Forbidden);
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest, errors);
      }

      OperationResultResponse<Guid?> response = new();

      response.Body = await _educationFormRepository.CreateAsync(_mapper.Map(request));
      response.Status = OperationResultStatusType.FullSuccess;

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      if (response.Body == default)
      {
        response = _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
      }

      return response;
    }
  }
}
