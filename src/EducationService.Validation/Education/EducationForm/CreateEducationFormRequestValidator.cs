using FluentValidation;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.EducationService.Validation.Education.Interfaces;

namespace LT.DigitalOffice.EducationService.Validation.Education
{
  public class CreateEducationFormRequestValidator : AbstractValidator<CreateEducationFormRequest>, ICreateEducationFormRequestValidator
  {
    public CreateEducationFormRequestValidator(IEducationFormRepository educationFormRepository)
    {
      RuleFor(educationForm => educationForm.Name)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("Education form must not be empty.")
        .MustAsync(async (form, _) => !await educationFormRepository.DoesEducationFormAlreadyExistAsync(form))
        .WithMessage("Education form with this name already exists.");
    }
  }
}
