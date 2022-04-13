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
  public class UserEducationRepository : IUserEducationRepository
  {
    private readonly IDataProvider _provider;

    public UserEducationRepository(
      IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<List<DbUserEducation>> GetAsync(Guid userId)
    {
      IQueryable<DbUserEducation> dbEducation = _provider.UsersEducations.AsQueryable();

      dbEducation = dbEducation.Include(dbEducation => dbEducation.EducationForm);
      dbEducation = dbEducation.Include(dbEducation => dbEducation.EducationType);

      return await dbEducation
        .Where(uc => uc.UserId == userId)
        .ToListAsync();
    }

    public async Task DisactivateEducationsAsync(Guid userId, Guid modifiedBy)
    {
      IQueryable<DbUserEducation> dbUserEducations = _provider.UsersEducations
        .Where(e => e.UserId == userId && e.IsActive)
        .AsQueryable();

      foreach (DbUserEducation dbUserEducation in dbUserEducations)
      {
        dbUserEducation.IsActive = false;
        dbUserEducation.ModifiedBy = modifiedBy;
        dbUserEducation.ModifiedAtUtc = DateTime.UtcNow;
      }

      await _provider.SaveAsync();
    }
  }
}
