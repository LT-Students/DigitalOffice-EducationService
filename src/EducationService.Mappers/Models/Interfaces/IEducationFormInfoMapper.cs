using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Models;

namespace LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;

public interface IEducationFormInfoMapper
{
  EducationFormInfo Map(DbUserEducation dbUserEducation);
}
