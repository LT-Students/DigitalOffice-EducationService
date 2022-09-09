using LT.DigitalOffice.EducationService.Business.Commands.User.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Models;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.User;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
  [HttpGet("find")]
  public async Task<FindResultResponse<EducationInfo>> FindAsync(
    [FromServices] IFindUsersCommand command,
    [FromQuery] FindUsersFilter filter)
  {
    return await command.ExecuteAsync(filter);
  }
}
