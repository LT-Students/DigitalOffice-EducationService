using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;

namespace LT.DigitalOffice.EducationService.Mappers.Db
{
  public class DbUserCertificateMapper : IDbUserCertificateMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbUserCertificateMapper(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public DbUserCertificate Map(CreateCertificateRequest request, Guid imageId)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      return new DbUserCertificate
      {
        Id = Guid.NewGuid(),
        UserId = request.UserId,
        ImageId = imageId,
        Name = request.Name,
        SchoolName = request.SchoolName,
        EducationType = (int)request.EducationType,
        ReceivedAt = request.ReceivedAt,
        IsActive = true,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow,
      };
    }
  }
}
