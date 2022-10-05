using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Data.Provider;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.User;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserEducationRepository(
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

    public async Task<List<Guid>> DisactivateEducationsAsync(Guid userId, Guid modifiedBy)
    {
      IQueryable<DbUserEducation> dbUserEducations = _provider.UsersEducations
        .Where(e => e.UserId == userId && e.IsActive)
        .Include(e => e.Images)
        .AsQueryable();

      List<Guid> imagesIds = new();

      foreach (DbUserEducation dbUserEducation in dbUserEducations)
      {
        dbUserEducation.IsActive = false;
        dbUserEducation.ModifiedBy = modifiedBy;
        dbUserEducation.ModifiedAtUtc = DateTime.UtcNow;

        foreach (DbEducationImage image in dbUserEducation.Images)
        {
          _provider.EducationsImages.Remove(image);
          imagesIds.Add(image.ImageId);
        }
      }

      await _provider.SaveAsync();

      return imagesIds;
    }

    public Task<List<DbUserEducation>> FindAsync(FindUsersFilter filter)
    {
      IQueryable<DbUserEducation> query = _provider.UsersEducations.AsQueryable();

      query = query.Where(e => e.UserId == filter.UserId);

      if (filter.EducationFormId.HasValue)
      {
        query = query.Where(e => e.EducationForm.Id == filter.EducationFormId.Value);
      }

      if (filter.EducationTypeId.HasValue)
      {
        query = query.Where(e => e.EducationType.Id == filter.EducationTypeId.Value);
      }

      if (filter.Completeness.HasValue)
      {
        query = query.Where(e => e.Completeness == (int)filter.Completeness.Value);
      }

      return query.Skip(filter.SkipCount).Take(filter.TakeCount).ToListAsync();
    }
  }
}
