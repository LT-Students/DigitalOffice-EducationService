using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.Education.Interfaces
{
  [AutoInject]
  public interface IRemoveEducationCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid educationId);
  }
}
