using LT.DigitalOffice.EducationService.Broker.Publishes.Interfaces;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Publishing.Subscriber.Image;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Broker.Publishes
{
  public class Publish : IPublish
  {
    private readonly IBus _bus;

    public Publish(
      IBus bus)
    {
      _bus = bus;
    }

    public async Task RemoveImagesAsync(List<Guid> imageIds, ImageSource imageSource)
    {
      await _bus.Publish<IRemoveImagesPublish>(IRemoveImagesPublish.CreateObj(
        imagesIds: imageIds,
        imageSource: imageSource));
    }
  }
}
