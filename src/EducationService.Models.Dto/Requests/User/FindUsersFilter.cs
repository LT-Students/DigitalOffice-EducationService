using LT.DigitalOffice.Kernel.Requests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.EducationService.Models.Dto.Requests.User;

public record FindUsersFilter : BaseFindFilter
{
  [FromQuery(Name = "educationFormId")]
  public Guid EducationFormId { get; set; }

  [FromQuery(Name = "educationTypeId")]
  public Guid EducationTypeId { get; set; }

  [FromQuery(Name = "completeness")]
  public int Completeness { get; set; }
}
