using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Models.Broker.Models.Education;

namespace LT.DigitalOffice.EducationService.Mappers.Models
{
  public class EducationFormDataMapper : IEducationFormDataMapper
  {
    public EducationFormData Map(DbEducationForm dbEducationForm)
    {
      if (dbEducationForm is null)
      {
        return null;
      }

      return new(
        dbEducationForm.Id,
        dbEducationForm.Name);
    }
  }
}
