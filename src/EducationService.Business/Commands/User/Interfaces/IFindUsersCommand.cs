using LT.DigitalOffice.EducationService.Models.Dto.Models;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.User;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.User.Interfaces;

[AutoInject]
public interface IFindUsersCommand
{
  Task<FindResultResponse<EducationInfo>> ExecuteAsync(FindUsersFilter filter);
}
