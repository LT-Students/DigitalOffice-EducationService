using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.EducationService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbEducationTypeMapper
  {
    DbEducationType Map(CreateEducationTypeRequest request);
  }
}
