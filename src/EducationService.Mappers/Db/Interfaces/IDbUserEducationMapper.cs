using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.User.Education;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.EducationService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbUserEducationMapper
  {
    DbUserEducation Map(CreateEducationRequest request);
  }
}
