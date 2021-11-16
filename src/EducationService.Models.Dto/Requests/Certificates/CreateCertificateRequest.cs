using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates
{
  public record CreateCertificateRequest
  {
    public Guid UserId { get; set; }
    public EducationType EducationType { get; set; }
    public string Name { get; set; }
    public string SchoolName { get; set; }
    public DateTime ReceivedAt { get; set; }
    public List<ImageContent> Images { get; set; }
  }
}
