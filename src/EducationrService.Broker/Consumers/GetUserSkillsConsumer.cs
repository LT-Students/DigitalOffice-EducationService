using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Skill;
using LT.DigitalOffice.Models.Broker.Responses.Skill;
using MassTransit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Broker.Consumers
{
  public class GetUserSkillsConsumer : IConsumer<IGetUserSkillsRequest>
  {
    private readonly IUserSkillRepository _repository;
    private readonly IUserSkillDataMapper _skillMapper;

    private async Task<object> GetUserSkillsAsync(IGetUserSkillsRequest request)
    {
      List<DbUserSkill> userSkills = await _repository.FindAsync(request.UserId);

      return IGetUserSkillsResponse
        .CreateObj(userSkills.Select(_skillMapper.Map).ToList());
    }

    public GetUserSkillsConsumer(
      IUserSkillRepository repository,
      IUserSkillDataMapper skillMapper)
    {
      _repository = repository;
      _skillMapper = skillMapper;
    }

    public async Task Consume(ConsumeContext<IGetUserSkillsRequest> context)
    {
      object response = OperationResultWrapper.CreateResponse(GetUserSkillsAsync, context.Message);

      await context.RespondAsync<IOperationResult<IGetUserSkillsResponse>>(response);
    }
  }
}
