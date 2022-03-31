using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.EducationService.Mappers.Db
{
  public class DbUserEducationMapper : IDbUserEducationMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDbEducationImageMapper _dbEducationImageMapper;

    public DbUserEducationMapper(
      IHttpContextAccessor httpContextAccessor,
      IDbEducationImageMapper dbEducationImageMapper)
    {
      _httpContextAccessor = httpContextAccessor;
      _dbEducationImageMapper = dbEducationImageMapper;
    }

    public DbUserEducation Map(CreateEducationRequest request, List<Guid> filesIds)
    {
      if (request is null)
      {
        return null;
      }

      Guid educationId = Guid.NewGuid();

      return new DbUserEducation
      {
        Id = educationId,
        UserId = request.UserId,
        UniversityName = request.UniversityName,
        QualificationName = request.QualificationName,
        EducationFormId = request.EducationFormId,
        EducationTypeId = request.EducationTypeId,
        Completeness = (int)request.Completeness,
        AdmissionAt = request.AdmissionAt,
        IssueAt = request.IssueAt,
        IsActive = true,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow,
        ModifiedBy = _httpContextAccessor.HttpContext.GetUserId(),
        ModifiedAtUtc = DateTime.UtcNow,
        Images = filesIds?
          .Select(fileId => _dbEducationImageMapper.Map(fileId, educationId))
          .ToList(),
      };
    }
  }
}
