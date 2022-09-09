using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.User;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data.Interfaces
{
  [AutoInject]
  public interface IUserEducationRepository
  {
    Task<List<DbUserEducation>> GetAsync(Guid userId);

    Task DisactivateEducationsAsync(Guid userId, Guid modifiedBy);

    Task<List<DbUserEducation>> FindAsync(FindUsersFilter filter);
  }
}
