using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Data.Provider;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data
{
  public class UserCertificateRepository : IUserCertificateRepository
  {
    private readonly IDataProvider _provider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserCertificateRepository(
      IDataProvider provider,
      IHttpContextAccessor httpContextAccessor)
    {
      _provider = provider;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid?> CreateAsync(DbUserCertificate dbCertificate)
    {
      if (dbCertificate is null)
      {
        return null;
      }

      _provider.UsersCertificates.Add(dbCertificate);
      await _provider.SaveAsync();

      return dbCertificate.Id;
    }

    public async Task<DbUserCertificate> GetAsync(Guid certificateId)
    {
      return await _provider.UsersCertificates.FirstOrDefaultAsync(x => x.Id == certificateId);
    }

    public async Task<bool> EditAsync(DbUserCertificate certificate, JsonPatchDocument<DbUserCertificate> request)
    {
      if (request is null || certificate is null)
      {
        return false;
      }

      request.ApplyTo(certificate);
      certificate.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
      certificate.ModifiedAtUtc = DateTime.UtcNow;
      await _provider.SaveAsync();

      return true;
    }

    public async Task<bool> RemoveAsync(DbUserCertificate dbCertificate)
    {
      if (dbCertificate is null)
      {
        return false;
      }

      dbCertificate.IsActive = false;
      dbCertificate.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
      dbCertificate.ModifiedAtUtc = DateTime.UtcNow;
      await _provider.SaveAsync();

      return true;
    }
  }
}
