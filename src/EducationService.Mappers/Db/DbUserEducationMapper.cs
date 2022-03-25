using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using System;

namespace LT.DigitalOffice.EducationService.Mappers.Db
{
  public class DbUserEducationMapper : IDbUserEducationMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbUserEducationMapper(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public DbUserEducation Map(CreateEducationRequest request)
    {
      if (request is null)
      {
        return null;
      }

      return new DbUserEducation
      {
        Id = Guid.NewGuid(),
        UserId = request.UserId,
        UniversityName = request.UniversityName,
        QualificationName = request.QualificationName,
        EducationFormId = request.EducationFormId,
        EducationTypeId = request.EducationTypeId,
        Ñompleteness = (int)request.Ñompleteness,
        AdmissionAt = request.AdmissionAt,
        IssueAt = request.IssueAt,
        IsActive = true,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow,
        ModifiedBy = _httpContextAccessor.HttpContext.GetUserId(),
        ModifiedAtUtc = DateTime.UtcNow
      };
    }
  }
}
