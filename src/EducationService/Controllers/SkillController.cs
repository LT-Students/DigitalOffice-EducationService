using LT.DigitalOffice.EducationService.Business.Commands.Skill.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Skills;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class SkillController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> Create(
     [FromServices] ICreateSkillCommand command,
     [FromBody] CreateSkillRequest request)
    {
      return await command.ExecuteAsync(request);
    }
  }
}
