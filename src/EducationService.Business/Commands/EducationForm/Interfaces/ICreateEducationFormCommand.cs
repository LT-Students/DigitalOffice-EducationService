using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.EducationForm.Interfaces
{
  [AutoInject]
  public interface ICreateEducationFormCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateEducationFormRequest request);
  }
}
