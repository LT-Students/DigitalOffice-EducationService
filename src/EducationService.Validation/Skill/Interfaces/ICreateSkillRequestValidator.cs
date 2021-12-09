using FluentValidation;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Skills;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.EducationService.Validation.Skill.Interfaces
{
  [AutoInject]
  public interface ICreateSkillRequestValidator : IValidator<CreateSkillRequest>
  {
  }
}