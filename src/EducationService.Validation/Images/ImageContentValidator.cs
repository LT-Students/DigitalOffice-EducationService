using FluentValidation;
using LT.DigitalOffice.Kernel.Validators.Interfaces;
using LT.DigitalOffice.EducationService.Validation.Image.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;

namespace LT.DigitalOffice.EducationService.Validation.Image
{
  public class ImageContentValidator : AbstractValidator<ImageContent>, IImageValidator
  {
    public ImageContentValidator(
      IImageContentValidator contentValidator,
      IImageExtensionValidator extensionValidator)
    {
      RuleFor(i => i.Content)
        .SetValidator(contentValidator)
        .WithMessage("Incorrect image content.");

      RuleFor(i => i.Extension)
        .SetValidator(extensionValidator)
        .WithMessage("Incorrect image extension.");
    }
  }
}
