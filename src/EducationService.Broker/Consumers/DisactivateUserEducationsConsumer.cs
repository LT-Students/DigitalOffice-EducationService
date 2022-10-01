using LT.DigitalOffice.EducationService.Broker.Publishes.Interfaces;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.Models.Broker.Publishing;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Broker.Consumers
{
  public class DisactivateUserEducationsConsumer : IConsumer<IDisactivateUserPublish>
  {
    private readonly IUserEducationRepository _repository;
    private readonly IPublish _publish;

    public DisactivateUserEducationsConsumer(
      IUserEducationRepository repository,
      IPublish publish)
    {
      _repository = repository;
      _publish = publish;
    }

    public async Task Consume(ConsumeContext<IDisactivateUserPublish> context)
    {
      List<Guid> imagesIds = await _repository.DisactivateEducationsAsync(context.Message.UserId, context.Message.ModifiedBy);

      await _publish.RemoveImagesAsync(imagesIds);
    }
  }
}
