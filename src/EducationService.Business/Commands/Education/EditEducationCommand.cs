using LT.DigitalOffice.EducationService.Business.Commands.Education.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Patch.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.EducationService.Validation.Education.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.Education
{
  public class EditEducationCommand : IEditEducationCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserEducationRepository _educationRepository;
    private readonly IPatchDbUserEducationMapper _mapper;
    private readonly IEditEducationRequestValidator _validator;
    private readonly IResponseCreator _responseCreator;

    public EditEducationCommand(
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IUserEducationRepository educationRepository,
      IPatchDbUserEducationMapper mapper,
      IEditEducationRequestValidator validator,
      IResponseCreator responseCreator)
    {
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _educationRepository = educationRepository;
      _mapper = mapper;
      _validator = validator;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(
      Guid educationId,
      JsonPatchDocument<EditEducationRequest> request)
    {
      DbUserEducation userEducation = await _educationRepository.GetAsync(educationId);

      if (userEducation is null)
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound);
      }

      if (_httpContextAccessor.HttpContext.GetUserId() != userEducation.UserId
        && !await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
      }

      return new OperationResultResponse<bool>
      {
        Status = OperationResultStatusType.FullSuccess,
        Body = await _educationRepository.EditAsync(userEducation, _mapper.Map(request))
      };
    }
  }
}
