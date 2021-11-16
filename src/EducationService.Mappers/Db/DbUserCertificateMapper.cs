using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.EducationService.Mappers.Db
{
  public class DbUserCertificateMapper : IDbUserCertificateMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDbCertificateImageMapper _dbCertificateImageMapper;

    public DbUserCertificateMapper(
      IHttpContextAccessor httpContextAccessor,
      IDbCertificateImageMapper dbCertificateImageMapper)
    {
      _httpContextAccessor = httpContextAccessor;
      _dbCertificateImageMapper = dbCertificateImageMapper;
    }

    public DbUserCertificate Map(CreateCertificateRequest request, List<Guid> filesIds)
    {
      if (request == null)
      {
        return null;
      }

      Guid certificateId = Guid.NewGuid();

      return new DbUserCertificate
      {
        Id = certificateId,
        UserId = request.UserId,
        Name = request.Name,
        SchoolName = request.SchoolName,
        EducationType = (int)request.EducationType,
        ReceivedAt = request.ReceivedAt,
        IsActive = true,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow,
        Images = filesIds?
          .Select(fileId => _dbCertificateImageMapper.Map(fileId, certificateId))
          .ToList(),
      };
    }
  }
}
