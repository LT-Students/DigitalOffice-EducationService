using LT.DigitalOffice.EducationService.Models.Dto.Requests.Skills;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.Skill.Interfaces
{
  [AutoInject]
  public interface ICreateSkillCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateSkillRequest request);
  }
}