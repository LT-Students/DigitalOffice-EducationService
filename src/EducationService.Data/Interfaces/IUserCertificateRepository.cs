using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data.Interfaces
{
  [AutoInject]
  public interface IUserCertificateRepository
  {
    Task<Guid?> CreateAsync(DbUserCertificate dbCertificate);

    Task<DbUserCertificate> GetAsync(Guid certificateId);

    Task<bool> EditAsync(DbUserCertificate certificateId, JsonPatchDocument<DbUserCertificate> request);

    Task<bool> RemoveAsync(DbUserCertificate dbCertificate);
  }
}
