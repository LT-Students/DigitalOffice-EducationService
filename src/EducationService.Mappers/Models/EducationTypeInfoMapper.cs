using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Models;

namespace LT.DigitalOffice.EducationService.Mappers.Models;

public class EducationTypeInfoMapper : IEducationTypeInfoMapper
{
  public EducationTypeInfo Map(DbUserEducation dbUserEducation)
  {
    return dbUserEducation is null
      ? null
      : new EducationTypeInfo
      {
        Id = dbUserEducation.EducationType.Id,
        Name = dbUserEducation.EducationType.Name
      };
  }
}
