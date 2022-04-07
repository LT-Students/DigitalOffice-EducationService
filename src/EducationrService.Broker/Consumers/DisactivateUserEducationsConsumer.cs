using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.Models.Broker.Common;
using MassTransit;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Broker.Consumers
{
  public class DisactivateUserEducationsConsumer : IConsumer<IDisactivateUserRequest>
  {
    private readonly IUserEducationRepository _repository;

    public DisactivateUserEducationsConsumer(
      IUserEducationRepository repository)
    {
      _repository = repository;
    }

    public async Task Consume(ConsumeContext<IDisactivateUserRequest> context)
    {
      await _repository.DisactivateEducationsAsync(context.Message.UserId, context.Message.ModifiedBy);
    }
  }
}
