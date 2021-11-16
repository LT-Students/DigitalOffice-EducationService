using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using System;

namespace LT.DigitalOffice.EducationService.Mappers.Db
{
  public class DbCertificateImageMapper : IDbCertificateImageMapper
  {
    public DbCertificateImage Map(Guid imageId, Guid certificateId)
    {
      return new DbCertificateImage
      {
        Id = Guid.NewGuid(),
        ImageId = imageId,
        CertificateId = certificateId
      };
    }
  }
}
