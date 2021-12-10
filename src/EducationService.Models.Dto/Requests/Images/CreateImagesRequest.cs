using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.EducationService.Models.Dto.Requests.Images
{
  public record CreateImagesRequest
  {
    public Guid CertificateId { get; set; }
    public List<ImageContent> Images { get; set; }
  }
}
