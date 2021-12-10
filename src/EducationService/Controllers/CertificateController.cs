using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.EducationService.Business.Commands.Certificate.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class CertificateController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> CreateAsync(
      [FromServices] ICreateCertificateCommand command,
      [FromBody] CreateCertificateRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpPatch("edit")]
    public async Task<OperationResultResponse<bool>> EditAsync(
      [FromServices] IEditCertificateCommand command,
      [FromQuery] Guid certificateId,
      [FromBody] JsonPatchDocument<EditCertificateRequest> request)
    {
      return await command.ExecuteAsync(certificateId, request);
    }

    [HttpDelete("remove")]
    public async Task<OperationResultResponse<bool>> RemoveAsync(
      [FromServices] IRemoveCertificateCommand command,
      [FromQuery] Guid certificateId)
    {
      return await command.ExecuteAsync(certificateId);
    }
  }
}
