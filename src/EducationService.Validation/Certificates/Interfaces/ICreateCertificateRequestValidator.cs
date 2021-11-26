using FluentValidation;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.EducationService.Validation.Certificates.Interfaces
{
  [AutoInject]
  public interface ICreateCertificateRequestValidator : IValidator<CreateCertificateRequest>
  {
  }
}
