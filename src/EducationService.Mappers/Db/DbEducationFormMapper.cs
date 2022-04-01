using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using System;

namespace LT.DigitalOffice.EducationService.Mappers.Db
{
  public class DbEducationFormMapper : IDbEducationFormMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbEducationFormMapper(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public DbEducationForm Map(CreateEducationFormRequest request)
    {
      if (request is null)
      {
        return null;
      }

      return new DbEducationForm
      {
        Id = Guid.NewGuid(),
        Name = request.Name,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow
      };
    }
  }
}
