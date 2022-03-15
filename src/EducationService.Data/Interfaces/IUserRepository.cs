using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data.Interfaces
{
  [AutoInject]
  public interface IUserRepository
  {
    Task<List<DbUserEducation>> GetAsync(Guid userId);

    Task DisactivateCertificateAndEducationsAsync(Guid userId, Guid modifiedBy);
  }
}
