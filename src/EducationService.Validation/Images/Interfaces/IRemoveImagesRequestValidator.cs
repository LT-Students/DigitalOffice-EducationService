using FluentValidation;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;

namespace LT.DigitalOffice.EducationService.Validation.Image.Interfaces
{
  [AutoInject]
  public interface IRemoveImagesRequestValidator : IValidator<RemoveImagesRequest>
  {
  }
}
