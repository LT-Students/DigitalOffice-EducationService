using FluentValidation.Results;
using LT.DigitalOffice.EducationService.Business.Commands.User.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Models;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.User;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Kernel.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.User;

public class FindUsersCommand : IFindUsersCommand
{
  private readonly IBaseFindFilterValidator _baseFindFilterValidator;
  private readonly IResponseCreator _responseCreator;
  private readonly IUserEducationRepository _userEducationRepository;
  private readonly IEducationInfoMapper _educationInfoMapper;

  public FindUsersCommand(
    IBaseFindFilterValidator baseFindFilterValidator,
    IResponseCreator responseCreator,
    IUserEducationRepository userEducationRepository,
    IEducationInfoMapper educationInfoMapper)
  {
    _baseFindFilterValidator = baseFindFilterValidator;
    _responseCreator = responseCreator;
    _userEducationRepository = userEducationRepository;
    _educationInfoMapper = educationInfoMapper;
  }

  public async Task<FindResultResponse<EducationInfo>> ExecuteAsync(FindUsersFilter filter)
  {
    ValidationResult validationResult = _baseFindFilterValidator.Validate(filter);

    if (!validationResult.IsValid)
    {
      return _responseCreator.CreateFailureFindResponse<EducationInfo>(HttpStatusCode.BadRequest,
        validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
    }

    List<DbUserEducation> dbUserEducations = await _userEducationRepository.FindAsync(filter);

    return new FindResultResponse<EducationInfo>(
      body: dbUserEducations.Select(_educationInfoMapper.Map).ToList());
  }
}
