using System;

namespace LT.DigitalOffice.EducationService.Models.Dto.Models;

public record EducationFormInfo
{
  public Guid Id { get; set; }
  public string Name { get; set; }
}
