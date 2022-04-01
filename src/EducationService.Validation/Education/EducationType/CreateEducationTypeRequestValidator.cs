using FluentValidation;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.EducationService.Validation.Education.Interfaces;

namespace LT.DigitalOffice.EducationService.Validation.Education
{
  public class CreateEducationTypeRequestValidator : AbstractValidator<CreateEducationTypeRequest>, ICreateEducationTypeRequestValidator
  {
    public CreateEducationTypeRequestValidator(IEducationTypeRepository educationTypeRepository)
    {
      RuleFor(educationType => educationType.Name)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("Education type must not be empty.")
        .MustAsync(async (type, _) => !await educationTypeRepository.DoesNameExistAsync(type))
        .WithMessage("Education type with this name already exists.");
    }
  }
}
