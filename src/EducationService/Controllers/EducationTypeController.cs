using LT.DigitalOffice.EducationService.Business.Commands.EducationType.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class EducationTypeController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> CreateAsync(
      [FromServices] ICreateEducationTypeCommand command,
      [FromBody] CreateEducationTypeRequest request)
    {
      return await command.ExecuteAsync(request);
    }
  }
}
