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
  public class EducationRepository : IEducationRepository
  {
    private readonly IDataProvider _provider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EducationRepository(
      IDataProvider provider,
      IHttpContextAccessor httpContextAccessor)
    {
      _provider = provider;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid?> CreateAsync(DbUserEducation dbEducation)
    {
      if (dbEducation is null)
      {
        return null;
      }

      _provider.UsersEducations.Add(dbEducation);
      await _provider.SaveAsync();

      return dbEducation.Id;
    }

    public async Task<DbUserEducation> GetAsync(Guid educationId)
    {
      return await _provider.UsersEducations.FirstOrDefaultAsync(e => e.Id == educationId);
    }

    public async Task<bool> EditAsync(DbUserEducation education, JsonPatchDocument<DbUserEducation> request)
    {
      if (request is null || education is null)
      {
        return false;
      }

      request.ApplyTo(education);
      education.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
      education.ModifiedAtUtc = DateTime.UtcNow;
      await _provider.SaveAsync();

      return true;
    }

    public async Task<bool> RemoveAsync(DbUserEducation dbEducation)
    {
      if (dbEducation is null)
      {
        return false;
      }

      dbEducation.IsActive = false;
      dbEducation.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
      dbEducation.ModifiedAtUtc = DateTime.UtcNow;
      await _provider.SaveAsync();

      return true;
    }
  }
}
