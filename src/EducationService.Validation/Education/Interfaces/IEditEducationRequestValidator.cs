using FluentValidation;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.User.Education;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.EducationService.Validation.Education.Interfaces
{
  [AutoInject]
  public interface IEditEducationRequestValidator : IValidator<JsonPatchDocument<EditEducationRequest>>
  {
  }
}
