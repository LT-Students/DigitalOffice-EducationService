using LT.DigitalOffice.EducationService.Business.Commands.Certificate.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using LT.DigitalOffice.EducationService.Validation.Certificates.Interfaces;
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

namespace LT.DigitalOffice.EducationService.Business.Commands.Certificate
{
  public class EditCertificateCommand : IEditCertificateCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICertificateRepository _certificateRepository;
    private readonly IPatchDbUserCertificateMapper _mapper;
    private readonly IResponseCreator _responseCreator;
    private readonly IEditCertificateRequestValidator _validator;

    public EditCertificateCommand(
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      ICertificateRepository certificateRepository,
      IPatchDbUserCertificateMapper mapper,
      IResponseCreator responseCreator,
      IEditCertificateRequestValidator validator)
    {
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _certificateRepository = certificateRepository;
      _mapper = mapper;
      _responseCreator = responseCreator;
      _validator = validator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(
      Guid certificateId,
      JsonPatchDocument<EditCertificateRequest> request)
    {
      DbUserCertificate certificate = await _certificateRepository.GetAsync(certificateId);

      if (_httpContextAccessor.HttpContext.GetUserId() != certificate.UserId
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
        Body = await _certificateRepository.EditAsync(certificate, _mapper.Map(request))
      };
    }
  }
}
