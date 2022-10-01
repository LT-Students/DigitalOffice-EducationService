using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.User;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data.Interfaces
{
  [AutoInject]
  public interface IUserEducationRepository
  {
    Task<Guid?> CreateAsync(DbUserEducation dbEducation);

    Task<DbUserEducation> GetAsync(Guid educationId);

    Task<bool> EditAsync(DbUserEducation educationId, JsonPatchDocument<DbUserEducation> request);

    Task<bool> RemoveAsync(DbUserEducation dbEducation);

    Task<List<Guid>> DisactivateEducationsAsync(Guid userId, Guid modifiedBy);

    Task<List<DbUserEducation>> FindAsync(FindUsersFilter filter);
  }
}
