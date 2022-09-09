using System;

namespace LT.DigitalOffice.EducationService.Models.Dto.Models;

public record EducationInfo
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public string UniversityName { get; set; }
  public string QualificationName { get; set; }
  public Guid EducationFormId { get; set; }
  public Guid EducationTypeId { get; set; }
  public int Completeness { get; set; }
  public DateTime AdmissionAt { get; set; }
  public DateTime? IssueAt { get; set; }
  public bool IsActive { get; set; }
}
