using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.Certificate.Interfaces
{
  [AutoInject]
  public interface ICreateCertificateCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateCertificateRequest request);
  }
}
