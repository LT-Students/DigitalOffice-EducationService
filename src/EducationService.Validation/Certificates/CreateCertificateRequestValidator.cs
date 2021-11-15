using FluentValidation;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using LT.DigitalOffice.EducationService.Validation.Certificates.Interfaces;

namespace LT.DigitalOffice.EducationService.Validation.Certificates
{
  public class CreateCertificateRequestValidator : AbstractValidator<CreateCertificateRequest>, ICreateCertificateRequestValidator
  {
    public CreateCertificateRequestValidator()
    {
      RuleFor(x => x.Name)
        .NotEmpty()
        .MaximumLength(100)
        .WithMessage("Name of Certificate is too long");

      RuleFor(x => x.SchoolName)
        .NotEmpty()
        .MaximumLength(100)
        .WithMessage("Name of school is too long");

      RuleFor(x => x.EducationType)
        .IsInEnum()
        .WithMessage("Wrong education type value.");
    }
  }
}
