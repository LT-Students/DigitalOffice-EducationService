﻿namespace LT.DigitalOffice.EducationService.Models.Dto.Requests.Images
{
  public record AddImageRequest
  {
    public string Name { get; set; }
    public string Content { get; set; }
    public string Extension { get; set; }
    public bool IsCurrentAvatar { get; set; } = false;
  }
}
