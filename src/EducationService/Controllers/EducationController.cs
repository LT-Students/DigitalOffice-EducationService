using LT.DigitalOffice.EducationService.Business.Commands.Education.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class EducationController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> Create(
      [FromServices] ICreateEducationCommand command,
      [FromBody] CreateEducationRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpPatch("edit")]
    public async Task<OperationResultResponse<bool>> Edit(
      [FromServices] IEditEducationCommand command,
      [FromQuery] Guid educationId,
      [FromBody] JsonPatchDocument<EditEducationRequest> request)
    {
      return await command.ExecuteAsync(educationId, request);
    }

    [HttpDelete("remove")]
    public async Task<OperationResultResponse<bool>> Remove(
      [FromServices] IRemoveEducationCommand command,
      [FromQuery] Guid educationId)
    {
      return await command.ExecuteAsync(educationId);
    }
  }
}
