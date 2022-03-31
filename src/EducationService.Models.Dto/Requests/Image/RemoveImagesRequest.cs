using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.EducationService.Models.Dto.Requests.Images
{
  public record RemoveImagesRequest
  {
    public Guid EducationId { get; set; }
    public List<Guid> ImagesIds { get; set; }
  }
}
