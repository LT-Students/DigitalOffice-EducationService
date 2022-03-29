using LT.DigitalOffice.EducationService.Business.Commands.Education.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.EducationService.Validation.Education.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.Education
{
  public class CreateEducationCommand : ICreateEducationCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IDbUserEducationMapper _mapper;
    private readonly IUserEducationRepository _educationRepository;
    private readonly ICreateEducationRequestValidator _validator;
    private readonly IResponseCreator _responseCreator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateEducationCommand(
      IAccessValidator accessValidator,
      IDbUserEducationMapper mapper,
      IUserEducationRepository educationRepository,
      ICreateEducationRequestValidator validator,
      IResponseCreator responseCreator,
      IHttpContextAccessor httpContextAccessor)
    {
      _accessValidator = accessValidator;
      _mapper = mapper;
      _educationRepository = educationRepository;
      _validator = validator;
      _responseCreator = responseCreator;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateEducationRequest request)
    {
      if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers)
        && _httpContextAccessor.HttpContext.GetUserId() != request.UserId)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.Forbidden);
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest, errors);
      }

      OperationResultResponse<Guid?> response = new();

      response.Body = await _educationRepository.CreateAsync(_mapper.Map(request));

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return response;
    }
  }
}
