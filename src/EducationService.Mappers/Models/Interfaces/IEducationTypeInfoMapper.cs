using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;

[AutoInject]
public interface IEducationTypeInfoMapper
{
  EducationTypeInfo Map(DbUserEducation dbUserEducation);
}
