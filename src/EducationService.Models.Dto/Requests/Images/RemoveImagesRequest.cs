using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.EducationService.Models.Dto.Requests.Images
{
  public record RemoveImagesRequest
  {
    public Guid EntityId { get; set; }
    public EntityType EntityType { get; set; }
    public List<Guid> ImagesIds { get; set; }
  }
}
