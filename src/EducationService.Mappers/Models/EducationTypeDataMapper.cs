using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Models.Broker.Models.Education;

namespace LT.DigitalOffice.EducationService.Mappers.Models
{
  public class EducationTypeDataMapper : IEducationTypeDataMapper
  {
    public EducationTypeData Map(DbEducationType dbEducationType)
    {
      if (dbEducationType is null)
      {
        return null;
      }

      return new(
        dbEducationType.Id,
        dbEducationType.Name);
    }
  }
}
