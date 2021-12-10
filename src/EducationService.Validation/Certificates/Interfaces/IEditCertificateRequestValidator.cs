using FluentValidation;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.EducationService.Validation.Certificates.Interfaces
{
  [AutoInject]
  public interface IEditCertificateRequestValidator : IValidator<JsonPatchDocument<EditCertificateRequest>>
  {
  }
}
