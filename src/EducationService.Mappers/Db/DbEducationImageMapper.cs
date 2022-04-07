using LT.DigitalOffice.EducationService.Mappers.Db.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using System;

namespace LT.DigitalOffice.EducationService.Mappers.Db
{
  public class DbEducationImageMapper : IDbEducationImageMapper
  {
    public DbEducationImage Map(Guid imageId, Guid educationId)
    {
      return new DbEducationImage
      {
        Id = Guid.NewGuid(),
        ImageId = imageId,
        EducationId = educationId
      };
    }
  }
}
