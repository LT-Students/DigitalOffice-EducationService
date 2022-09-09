using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Models;

namespace LT.DigitalOffice.EducationService.Mappers.Models;

public class EducationInfoMapper : IEducationInfoMapper
{
  public EducationInfo Map(DbUserEducation dbUserEducation)
  {
    return dbUserEducation is null
      ? null
      : new EducationInfo
      {
        Id = dbUserEducation.Id,
        UserId = dbUserEducation.UserId,
        UniversityName = dbUserEducation.UniversityName,
        QualificationName = dbUserEducation.QualificationName,
        EducationFormId = dbUserEducation.EducationFormId,
        EducationTypeId = dbUserEducation.EducationTypeId,
        Completeness = dbUserEducation.Completeness,
        AdmissionAt = dbUserEducation.AdmissionAt,
        IssueAt = dbUserEducation.IssueAt
      };
  }
}
