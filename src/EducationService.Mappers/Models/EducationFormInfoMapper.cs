using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Models;

namespace LT.DigitalOffice.EducationService.Mappers.Models;

public class EducationFormInfoMapper : IEducationFormInfoMapper
{
  public EducationFormInfo Map(DbUserEducation dbUserEducation)
  {
    return dbUserEducation is null
      ? null
      : new EducationFormInfo
      {
        Id = dbUserEducation.EducationForm.Id,
        Name = dbUserEducation.EducationForm.Name
      };
  }
}
