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
  public class UserEducationRepository : IUserEducationRepository
  {
    private readonly IDataProvider _provider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserEducationRepository(
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
