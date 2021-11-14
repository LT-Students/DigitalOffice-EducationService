using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using System;

namespace LT.DigitalOffice.EducationService.Models.Dto.Requests.User.Images
{
  public record CreateImageRequest
  {
    public Guid EntityId { get; set; }
    public EntityType EntityType { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public string Extension { get; set; }
    public bool IsCurrentAvatar { get; set; } = false;
  }
}
