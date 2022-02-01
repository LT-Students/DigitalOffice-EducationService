using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.Models.Broker.Common;
using MassTransit;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Broker.Consumers
{
  public class DisactivateUserEducationConsumer : IConsumer<IDisactivateUserRequest>
  {
    private readonly IUserRepository _repository;

    public DisactivateUserEducationConsumer(
      IUserRepository repository)
    {
      _repository = repository;
    }

    public async Task Consume(ConsumeContext<IDisactivateUserRequest> context)
    {
      await _repository.DisactivateCertificateAndEducations(context.Message.UserId, context.Message.ModifiedBy);
    }
  }
}
