using LT.DigitalOffice.EducationService.Business.Commands.Education.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.EducationService.Validation.Education.Interfaces;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.EducationService.Data.Interfaces;
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
    private readonly IEducationRepository _educationRepository;
    private readonly IPatchDbUserEducationMapper _mapper;
    private readonly IEditEducationRequestValidator _validator;
    private readonly IResponseCreater _responseCreator;

    public EditEducationCommand(
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IEducationRepository educationRepository,
      IPatchDbUserEducationMapper mapper,
      IEditEducationRequestValidator validator,
      IResponseCreater responseCreator)
    {
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _educationRepository = educationRepository;
      _mapper = mapper;
      _validator = validator;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid educationId, JsonPatchDocument<EditEducationRequest> request)
    {
      Guid senderId = _httpContextAccessor.HttpContext.GetUserId();
      DbUserEducation userEducation = _educationRepository.Get(educationId);

      if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers)
        && senderId != userEducation.UserId)
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
      }

      JsonPatchDocument<DbUserEducation> dbRequest = _mapper.Map(request);

      bool result = await _educationRepository.EditAsync(userEducation, dbRequest);

      return new OperationResultResponse<bool>
      {
        Status = OperationResultStatusType.FullSuccess,
        Body = result
      };
    }
  }
}