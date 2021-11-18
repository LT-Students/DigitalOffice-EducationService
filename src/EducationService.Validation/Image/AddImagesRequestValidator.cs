using FluentValidation;
using LT.DigitalOffice.Kernel.Validators.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.EducationService.Validation.Image.Interfaces;

namespace LT.DigitalOffice.EducationService.Validation.Avatars
{
  public class AddImagesRequestValidator : AbstractValidator<CreateImagesRequest>, IAddImagesRequestValidator
  {
    public AddImagesRequestValidator(
      IImageContentValidator imageContentValidator,
      IImageExtensionValidator imageExtensionValidator,
      ICertificateRepository certificateRepository,
      IEducationRepository educationRepository)
    {
      RuleFor(x => x)
        .MustAsync(async (x, _) =>
          await certificateRepository.GetAsync(x.CertificateId) != null
          || await educationRepository.GetAsync(x.CertificateId) != null)
        .WithMessage("Entity doesn't exist.");

     /* RuleFor(x => x.Content)
        .SetValidator(imageContentValidator);

      RuleFor(x => x.Extension)
        .SetValidator(imageExtensionValidator);*/
    }
  }
}
