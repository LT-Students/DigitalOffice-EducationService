using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Education;
using LT.DigitalOffice.Models.Broker.Responses.Education;
using MassTransit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationService.Broker.Consumers
{
  public class GetUserEducationsConsumer : IConsumer<IGetUserEducationsRequest>
  {
    private readonly IUserRepository _repository;
    private readonly IUserEducationDataMapper _educationMapper;
    //private readonly IUserCertificateDataMapper _certificateMapper;

    private async Task<object> GetUserEducationsAsync(IGetUserEducationsRequest request)
    {
      //List < DbUserEducation > userEducations =
      //   await _repository.GetAsync(request.UserId);

      //return IGetUserEducationsResponse
      //  .CreateObj(
      //    userEducations?.Select(_educationMapper.Map).ToList()); // поправить это в моделсах

      return null;
    }

    public GetUserEducationsConsumer(
      IUserRepository repository,
      IUserEducationDataMapper educationMapper)
    {
      _repository = repository;
      _educationMapper = educationMapper;
    }

    public async Task Consume(ConsumeContext<IGetUserEducationsRequest> context)
    {
      object response = OperationResultWrapper.CreateResponse(GetUserEducationsAsync, context.Message);

      await context.RespondAsync<IOperationResult<IGetUserEducationsResponse>>(response);
    }
  }
}
