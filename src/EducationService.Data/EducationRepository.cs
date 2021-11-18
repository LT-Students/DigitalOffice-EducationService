using LT.DigitalOffice.EducationService.Data.Provider;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> CreateAsync(DbUserEducation education)
    {
      if (education is null)
      {
        return false;
      }

      _provider.UsersEducations.Add(education);
      await _provider.SaveAsync();

      return true;
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

    public async Task<bool> RemoveAsync(DbUserEducation education)
    {
      if (education is null)
      {
        return false;
      }

      education.IsActive = false;
      education.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
      education.ModifiedAtUtc = DateTime.UtcNow;
      await _provider.SaveAsync();

      return true;
    }
  }
}
