using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.Certificate.Interfaces
{
  [AutoInject]
  public interface IEditCertificateCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid certificateId, JsonPatchDocument<EditCertificateRequest> request);
  }
}
