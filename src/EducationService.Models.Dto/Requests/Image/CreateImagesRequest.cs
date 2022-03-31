using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.EducationService.Models.Dto.Requests.Images
{
  public record CreateImagesRequest
  {
    public Guid EducationId { get; set; }
    public List<ImageContent> Images { get; set; }
  }
}
