using LT.DigitalOffice.EducationService.Models.Dto.Requests.User.Education;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.Education.Interfaces
{
  [AutoInject]
  public interface IEditEducationCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid educationId, JsonPatchDocument<EditEducationRequest> request);
  }
}
