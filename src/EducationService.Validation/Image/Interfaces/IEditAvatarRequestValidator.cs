using FluentValidation;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using System;

namespace LT.DigitalOffice.EducationService.Validation.Image.Interfaces
{
  [AutoInject]
  public interface IEditAvatarRequestValidator : IValidator<(Guid userId, Guid imageId)>
  {
  }
}
