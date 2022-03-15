using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using LT.DigitalOffice.Models.Broker.Models.Education;

namespace LT.DigitalOffice.EducationService.Mappers.Models
{
  public class UserEducationDataMapper : IUserEducationDataMapper
  {
    public EducationData Map(DbUserEducation dbUserEducation)
    {
      if (dbUserEducation is null)
      {
        return null;
      }

      //   return new EducationData(
      //     id: dbUserEducation.Id,
      //     universityName: dbUserEducation.UniversityName,
      //     qualificationName: dbUserEducation.QualificationName,
      //     formEducation: ((FormEducation)dbUserEducation.FormEducation).ToString(),
      //     admissionAt: dbUserEducation.AdmissionAt,
      //     issueAt: dbUserEducation.IssueAt,
      //     imageId: null);
      return null;
    }
  }
}
