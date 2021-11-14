using FluentValidation;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.User.Education;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.EducationService.Validation.Education.Interfaces
{
  [AutoInject]
  public interface ICreateEducationRequestValidator : IValidator<CreateEducationRequest>
  {
  }
}
