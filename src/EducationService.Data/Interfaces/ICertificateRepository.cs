using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data.Interfaces
{
  [AutoInject]
  public interface ICertificateRepository
  {
    Task<bool> AddAsync(DbUserCertificate certificate);

    DbUserCertificate Get(Guid certificateId);

    Task<bool> EditAsync(DbUserCertificate certificateId, JsonPatchDocument<DbUserCertificate> request);

    Task<bool> RemoveAsync(DbUserCertificate certificate);
  }
}
