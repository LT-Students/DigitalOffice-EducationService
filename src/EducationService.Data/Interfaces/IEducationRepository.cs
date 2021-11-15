using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data.Interfaces
{
  [AutoInject]
  public interface IEducationRepository
  {
    Task AddAsync(DbUserEducation education);

    DbUserEducation Get(Guid educationId);

    Task<bool> EditAsync(DbUserEducation educationId, JsonPatchDocument<DbUserEducation> request);

    Task<bool> RemoveAsync(DbUserEducation education);
  }
}
