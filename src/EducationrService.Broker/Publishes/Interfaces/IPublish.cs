using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Models.Broker.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Broker.Publishes.Interfaces
{
  [AutoInject]
  public interface IPublish
  {
    Task RemoveImagesAsync(List<Guid> imagesIds, ImageSource imageSource);
  }
}
