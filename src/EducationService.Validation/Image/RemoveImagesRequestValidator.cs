using FluentValidation;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.EducationService.Validation.Image.Interfaces;

namespace LT.DigitalOffice.EducationService.Validation.Image
{
  public class RemoveImagesRequestValidator : AbstractValidator<RemoveImagesRequest>, IRemoveImagesRequestValidator
  {
    public RemoveImagesRequestValidator()
    {
      RuleFor(list => list.ImagesIds)
        .NotNull().WithMessage("List must not be null.")
        .NotEmpty().WithMessage("List must not be empty.")
        .ForEach(x => x.NotEmpty().WithMessage("Image's Id must not be empty."));
    }
  }
}
