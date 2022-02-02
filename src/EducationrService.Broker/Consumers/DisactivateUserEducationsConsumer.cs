using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.Models.Broker.Common;
using MassTransit;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Broker.Consumers
{
  public class DisactivateUserEducationsConsumer : IConsumer<IDisactivateUserRequest>
  {
    private readonly IUserRepository _repository;

    public DisactivateUserEducationsConsumer(
      IUserRepository repository)
    {
      _repository = repository;
    }

    public async Task Consume(ConsumeContext<IDisactivateUserRequest> context)
    {
      await _repository.DisactivateCertificateAndEducationsAsync(context.Message.UserId, context.Message.ModifiedBy);
    }
  }
}
