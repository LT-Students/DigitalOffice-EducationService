using FluentValidation;
using LT.DigitalOffice.Kernel.Validators.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.EducationService.Validation.Image.Interfaces;
using System.Collections.Generic;

namespace LT.DigitalOffice.EducationService.Validation.Avatars
{
  public class CreateImagesRequestValidator : AbstractValidator<CreateImagesRequest>, ICreateImagesRequestValidator
  {
    public CreateImagesRequestValidator(
      IImageValidator imageValidator)
    {
      List<string> errors = new();

      RuleFor(images => images)
        .NotNull().WithMessage("List must not be null.")
        .NotEmpty().WithMessage("List must not be empty.");

      RuleFor(images => images.CertificateId)
        .NotEmpty().WithMessage("Certificate id must not be empty.");

      RuleForEach(images => images.Images)
        .SetValidator(imageValidator)
        .WithMessage("Incorrect image.");
    }
  }
}
