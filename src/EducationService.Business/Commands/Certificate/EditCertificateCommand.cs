using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.EducationService.Business.Commands.Certificate.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.EducationService.Validation.Certificates.Interfaces;
using System.Net;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;

namespace LT.DigitalOffice.EducationService.Business.Commands.Certificate
{
  public class EditCertificateCommand : IEditCertificateCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICertificateRepository _certificateRepository;
    private readonly IPatchDbUserCertificateMapper _mapper;
    private readonly IResponseCreater _responseCreator;
    private readonly IEditCertificateRequestValidator _validator;

    public EditCertificateCommand(
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      ICertificateRepository certificateRepository,
      IPatchDbUserCertificateMapper mapper,
      IResponseCreater responseCreator,
      IEditCertificateRequestValidator validator)
    {
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _certificateRepository = certificateRepository;
      _mapper = mapper;
      _responseCreator = responseCreator;
      _validator = validator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid certificateId, JsonPatchDocument<EditCertificateRequest> request)
    {
      DbUserCertificate certificate = await _certificateRepository.GetAsync(certificateId);

      if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers)
        && _httpContextAccessor.HttpContext.GetUserId() != certificate.UserId)
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
      }

      JsonPatchDocument<DbUserCertificate> dbRequest = _mapper.Map(request);

      bool result = await _certificateRepository.EditAsync(certificate, dbRequest);

      return new OperationResultResponse<bool>
      {
        Status = errors.Any() ? OperationResultStatusType.PartialSuccess : OperationResultStatusType.FullSuccess,
        Body = result,
        Errors = errors
      };
    }
  }
}
