using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Data.Provider;
using LT.DigitalOffice.EducationService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly IDataProvider _provider;

    public UserRepository(
      IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<(List<DbUserCertificate> userCertificates, List<DbUserEducation> userEducations)> 
      GetAsync(Guid userId)
    {
      return (
        await _provider.UsersCertificates
          .Include(uc => uc.Images)
          .Where(uc => uc.UserId == userId)
          .ToListAsync(),
        await _provider.UsersEducations
          .Where(uc => uc.UserId == userId)
          .ToListAsync());
    }

    public async Task<bool> DisactivateCertificateAndEducations(Guid userId, Guid modifiedBy)
    {
      IQueryable<DbUserEducation> dbUserEducations = _provider.UsersEducations
        .Where(e => e.UserId == userId && e.IsActive)
        .AsQueryable();

      IQueryable<DbUserCertificate> dbUserCertificates = _provider.UsersCertificates
        .Where(e => e.UserId == userId && e.IsActive)
        .AsQueryable();

      foreach (DbUserEducation dbUserEducation in dbUserEducations)
      {
        dbUserEducation.IsActive = false;
        dbUserEducation.ModifiedBy = modifiedBy;
        dbUserEducation.ModifiedAtUtc = DateTime.UtcNow;
      }

      foreach (DbUserCertificate dbUserCertificate in dbUserCertificates)
      {
        dbUserCertificate.IsActive = false;
        dbUserCertificate.ModifiedBy = modifiedBy;
        dbUserCertificate.ModifiedAtUtc = DateTime.UtcNow;
      }
      await _provider.SaveAsync();

      return true;
    }
  }
}
