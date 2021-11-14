using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Models.Broker.Models;
using System;
using System.Collections.Generic;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;

namespace LT.DigitalOffice.EducationService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface ICreateImageDataMapper
  {
    List<CreateImageData> Map(List<AddImageRequest> request);

    List<CreateImageData> Map(string name, string content, string extension);
  }
}
