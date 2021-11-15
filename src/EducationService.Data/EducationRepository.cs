using LT.DigitalOffice.EducationService.Data.Provider;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.UserService.Data
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

    public async Task<bool> AddAsync(DbUserEducation education)
    {
      if (education is null)
      {
        return false;
      }

      _provider.UserEducations.Add(education);
      await _provider.SaveAsync();

      return true;
    }

    public DbUserEducation Get(Guid educationId)
    {
      DbUserEducation education = _provider.UserEducations.FirstOrDefault(e => e.Id == educationId);

      if (education is null)
      {
        return null;
      }

      return education;
    }

    public async Task<bool> EditAsync(DbUserEducation education, JsonPatchDocument<DbUserEducation> request)
    {
      if (education is null)
      {
        return false;
      }

      if (request is null)
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
