using LT.DigitalOffice.EducationService.Business.Commands.Education.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.EducationService.Validation.Education.Interfaces;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;

namespace LT.DigitalOffice.EducationService.Business.Commands.Education
{
  public class CreateEducationCommand : ICreateEducationCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IDbUserEducationMapper _mapper;
    private readonly IEducationRepository _educationRepository;
    private readonly ICreateEducationRequestValidator _validator;
    private readonly IResponseCreater _responseCreator;

    public CreateEducationCommand(
      IAccessValidator accessValidator,
      IDbUserEducationMapper mapper,
      IEducationRepository educationRepository,
      ICreateEducationRequestValidator validator,
      IResponseCreater responseCreator)
    {
      _accessValidator = accessValidator;
      _mapper = mapper;
      _educationRepository = educationRepository;
      _validator = validator;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateEducationRequest request)
    {
      if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.Forbidden);
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest, errors);
      }

      DbUserEducation dbEducation = _mapper.Map(request);

      await _educationRepository.AddAsync(dbEducation);

      return new OperationResultResponse<Guid?>
      {
        Status = OperationResultStatusType.FullSuccess,
        Body = dbEducation.Id
      };
    }
  }
}
