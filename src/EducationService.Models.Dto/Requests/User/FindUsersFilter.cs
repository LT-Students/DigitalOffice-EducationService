using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using LT.DigitalOffice.Kernel.Requests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.EducationService.Models.Dto.Requests.User;

public record FindUsersFilter : BaseFindFilter
{
  [FromQuery(Name = "userid")]
  public Guid UserId { get; set; }

  [FromQuery(Name = "educationformid")]
  public Guid? EducationFormId { get; set; }

  [FromQuery(Name = "educationtypeid")]
  public Guid? EducationTypeId { get; set; }

  [FromQuery(Name = "completeness")]
  public EducationCompleteness? Completeness { get; set; }
}
