using LT.DigitalOffice.EducationService.Business.Commands.EducationForm.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class EducationFormController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> CreateAsync(
      [FromServices] ICreateEducationFormCommand command,
      [FromBody] CreateEducationFormRequest request)
    {
      return await command.ExecuteAsync(request);
    }
  }
}
