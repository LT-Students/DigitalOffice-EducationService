using System;

namespace LT.DigitalOffice.EducationService.Models.Dto.Requests.Education
{
  public record CreateEducationRequest
  {
    public Guid UserId { get; set; }
    public string UniversityName { get; set; }
    public string QualificationName { get; set; }
    public Guid FormEducationId { get; set; }
    public Guid EducationtTypeId { get; set; }
    public int Сompleteness { get; set; }
    public DateTime AdmissionAt { get; set; }
    public DateTime? IssueAt { get; set; }
  }
}
