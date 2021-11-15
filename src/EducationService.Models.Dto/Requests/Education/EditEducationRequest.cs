using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using System;

namespace LT.DigitalOffice.EducationService.Models.Dto.Requests.Education
{
  public class EditEducationRequest
  {
    public string UniversityName { get; set; }
    public string QualificationName { get; set; }
    public FormEducation FormEducation { get; set; }
    public DateTime AdmissionAt { get; set; }
    public DateTime? IssueAt { get; set; }
    public bool IsActive { get; set; }
  }
}
