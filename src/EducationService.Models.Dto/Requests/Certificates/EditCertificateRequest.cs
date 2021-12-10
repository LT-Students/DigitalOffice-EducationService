﻿using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using System;

namespace LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates
{
  public record EditCertificateRequest
  {
    public EducationType EducationType { get; set; }
    public string Name { get; set; }
    public string SchoolName { get; set; }
    public DateTime ReceivedAt { get; set; }
    public bool IsActive { get; set; }
  }
}
