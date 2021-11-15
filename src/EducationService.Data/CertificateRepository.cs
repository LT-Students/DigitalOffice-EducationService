using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Data.Provider;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Exceptions.Models;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data
{
  public class CertificateRepository : ICertificateRepository
  {
    private readonly IDataProvider _provider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CertificateRepository(
      IDataProvider provider,
      IHttpContextAccessor httpContextAccessor)
    {
      _provider = provider;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> AddAsync(DbUserCertificate certificate)
    {
      if (certificate is null)
      {
        return false;
      }

      _provider.UserCertificates.Add(certificate);
      await _provider.SaveAsync();

      return true;
    }

    public DbUserCertificate Get(Guid certificateId)
    {
      var certificate = _provider.UserCertificates.FirstOrDefault(x => x.Id == certificateId);

      if (certificate is null)
      {
        return null;
      }

      return certificate;
    }

    public async Task<bool> EditAsync(DbUserCertificate certificate, JsonPatchDocument<DbUserCertificate> request)
    {
      if (certificate is null)
      {
        return false;
      }

      if (request is null)
      {
        return false;
      }

      request.ApplyTo(certificate);
      certificate.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
      certificate.ModifiedAtUtc = DateTime.UtcNow;
      await _provider.SaveAsync();

      return true;
    }

    public async Task<bool> RemoveAsync(DbUserCertificate certificate)
    {
      if (certificate is null)
      {
        return false;
      }

      certificate.IsActive = false;
      certificate.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
      certificate.ModifiedAtUtc = DateTime.UtcNow;
      await _provider.SaveAsync();

      return true;
    }
  }
}
