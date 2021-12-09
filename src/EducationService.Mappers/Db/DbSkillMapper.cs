using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Skills;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using System;

namespace LT.DigitalOffice.EducationService.Mappers.Db
{
  public class DbSkillMapper : IDbSkillMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbSkillMapper(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public DbSkill Map(CreateSkillRequest request)
    {
      if (request is null)
      {
        return null;
      }

      return new DbSkill
      {
        Id = Guid.NewGuid(),
        Name = request.Name.Trim(),
        IsActive = true,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow
      };
    }
  }
}
