using FluentValidation;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.EducationService.Validation.Education.Interfaces;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.EducationService.Validation.Education
{
  public class CreateEducationRequestValidator : AbstractValidator<CreateEducationRequest>, ICreateEducationRequestValidator
  {
    public CreateEducationRequestValidator()
    {
      RuleFor(education => education.UniversityName)
        .NotEmpty().WithMessage("University name must not be empty.")
        .MaximumLength(100).WithMessage("University name is too long");

      RuleFor(education => education.QualificationName)
        .NotEmpty().WithMessage("Qualification name must not be empty.")
        .MaximumLength(100).WithMessage("Qualification name is too long");

      RuleFor(education => education.FormEducation)
        .IsInEnum().WithMessage("Wrong form education.");
    }
  }
}
