using FluentValidation;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Skills;
using LT.DigitalOffice.EducationService.Validation.Skill.Interfaces;

namespace LT.DigitalOffice.EducationService.Validation.Skill
{
  public class CreateSkillRequestValidator : AbstractValidator<CreateSkillRequest>, ICreateSkillRequestValidator
  {
    public CreateSkillRequestValidator(ISkillRepository skillRepository)
    {
      RuleFor(s => s.Name)
        .Cascade(CascadeMode.Stop)
        .Must(s => string.IsNullOrWhiteSpace(s)).WithMessage("Name of Skill must not be empty.")
        .Must(s => s.Trim().Length <= 100).WithMessage("Name of Skill is too long.")
        .MustAsync(async (name, _) => !await skillRepository.DoesSkillAlreadyExistAsync(name))
        .WithMessage("Skill with this name already exists.");
    }
  }
}
