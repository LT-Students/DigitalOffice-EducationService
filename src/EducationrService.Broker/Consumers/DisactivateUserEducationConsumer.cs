using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Models.Broker.Common;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Broker.Consumers
{
  public class DisactivateUserEducationConsumer : IConsumer<IDisactivateUserRequest>
  {
    private readonly IUserEducationRepository _repository;

    public DisactivateUserEducationConsumer(
      IUserEducationRepository repository)
    {
      _repository = repository;
    }

    public async Task Consume(ConsumeContext<IDisactivateUserRequest> context)
    {
      await _repository.DisactivateEducation(context.Message.UserId, context.Message.ModifiedBy);
    }
  }
}
