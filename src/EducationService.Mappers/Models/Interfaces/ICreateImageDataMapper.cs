using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Models.Broker.Models;
using System.Collections.Generic;

namespace LT.DigitalOffice.EducationService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface ICreateImageDataMapper
  {
    List<CreateImageData> Map(List<ImageContent> request);

    List<CreateImageData> Map(string name, string content, string extension);
  }
}
