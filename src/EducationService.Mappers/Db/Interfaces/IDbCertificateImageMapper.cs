using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;

namespace LT.DigitalOffice.EducationService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbCertificateImageMapper
  {
    DbCertificateImage Map(Guid imageId, Guid certificateId);
  }
}
