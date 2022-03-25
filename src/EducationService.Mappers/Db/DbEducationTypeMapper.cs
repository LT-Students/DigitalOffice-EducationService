using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using System;

namespace LT.DigitalOffice.EducationService.Mappers.Db
{
  public class DbEducationTypeMapper : IDbEducationTypeMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbEducationTypeMapper(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public DbEducationType Map(CreateEducationTypeRequest request)
    {
      if (request is null)
      {
        return null;
      }

      return new DbEducationType
      {
        Id = Guid.NewGuid(),
        Name = request.Name,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow,
        ModifiedBy = _httpContextAccessor.HttpContext.GetUserId(),
        ModifiedAtUtc = DateTime.UtcNow,
      };
    }
  }
}
