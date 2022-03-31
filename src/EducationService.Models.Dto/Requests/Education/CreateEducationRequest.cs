using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.EducationService.Models.Dto.Requests.Education
{
  public record CreateEducationRequest
  {
    public Guid UserId { get; set; }
    public string UniversityName { get; set; }
    public string QualificationName { get; set; }
    public Guid EducationFormId { get; set; }
    public Guid EducationTypeId { get; set; }
    public EducationCompleteness Completeness { get; set; }   
    public DateTime AdmissionAt { get; set; }
    public DateTime? IssueAt { get; set; }
    public List<ImageContent> Images { get; set; }
  }
}
