using FluentValidation;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.EducationService.Validation.Education.Interfaces
{
  [AutoInject]
  public interface ICreateEducationFormRequestValidator : IValidator<CreateEducationFormRequest>
  {

  }
}
