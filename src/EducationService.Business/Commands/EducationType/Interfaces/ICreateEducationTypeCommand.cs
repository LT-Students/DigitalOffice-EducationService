using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.EducationType.Interfaces
{
  [AutoInject]
  public interface ICreateEducationTypeCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateEducationTypeRequest request);
  }
}
