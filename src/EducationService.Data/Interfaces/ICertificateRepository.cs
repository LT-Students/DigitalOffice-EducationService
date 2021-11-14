using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.EducationService.Models.Db;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data.Interfaces
{
    [AutoInject]
    public interface ICertificateRepository
    {
        Task AddAsync(DbUserCertificate certificate);

        DbUserCertificate Get(Guid certificateId);

        Task<bool> EditAsync(DbUserCertificate certificateId, JsonPatchDocument<DbUserCertificate> request);

        Task<bool> RemoveAsync(DbUserCertificate certificate);
    }
}
