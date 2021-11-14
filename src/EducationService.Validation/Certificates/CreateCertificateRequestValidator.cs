using FluentValidation;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;

namespace LT.DigitalOffice.EducationService.Validation.Certificates
{
  public class CreateCertificateRequestValidator : AbstractValidator<CreateCertificateRequest>
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

      RuleFor(x => x.Image)
        .NotNull();
    }
  }
}
