﻿using LT.DigitalOffice.EducationService.Data.Interfaces;
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

    public async Task AddAsync(DbUserCertificate certificate)
    {
      if (certificate == null)
      {
        throw new ArgumentNullException(nameof(certificate));
      }

      _provider.UserCertificates.Add(certificate);
      await _provider.SaveAsync();
    }

    public DbUserCertificate Get(Guid certificateId)
    {
      var certificate = _provider.UserCertificates.FirstOrDefault(x => x.Id == certificateId);

      if (certificate == null)
      {
        throw new NotFoundException($"User certificate with ID '{certificateId}' was not found.");
      }

      return certificate;
    }

    public async Task<bool> EditAsync(DbUserCertificate certificate, JsonPatchDocument<DbUserCertificate> request)
    {
      if (certificate == null)
      {
        throw new ArgumentNullException(nameof(certificate));
      }

      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      request.ApplyTo(certificate);
      certificate.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
      certificate.ModifiedAtUtc = DateTime.UtcNow;
      await _provider.SaveAsync();

      return true;
    }

    public async Task<bool> RemoveAsync(DbUserCertificate certificate)
    {
      if (certificate == null)
      {
        throw new ArgumentNullException(nameof(certificate));
      }

      certificate.IsActive = false;
      certificate.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
      certificate.ModifiedAtUtc = DateTime.UtcNow;
      await _provider.SaveAsync();

      return true;
    }
  }
}
