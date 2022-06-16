using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Broker.Requests.Interfaces
{
  [AutoInject]
  public interface IImageService
  {
    Task<List<Guid>> CreateImagesAsync(List<ImageContent> images, List<string> errors);
  }
}
