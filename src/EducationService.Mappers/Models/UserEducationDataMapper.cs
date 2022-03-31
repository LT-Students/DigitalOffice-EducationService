using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using LT.DigitalOffice.Models.Broker.Models.Education;

namespace LT.DigitalOffice.EducationService.Mappers.Models
{
  public class UserEducationDataMapper : IUserEducationDataMapper
  {
    private readonly IEducationFormDataMapper _educationFormDataMapper;
    private readonly IEducationTypeDataMapper _educationTypeDataMapper;

    public UserEducationDataMapper(
      IEducationFormDataMapper educationFormDataMapper,
      IEducationTypeDataMapper educationTypeDataMapper)
    {
      _educationFormDataMapper = educationFormDataMapper;
      _educationTypeDataMapper = educationTypeDataMapper;
    }

    public EducationData Map(DbUserEducation dbUserEducation)
    {
      if (dbUserEducation is null)
      {
        return null;
      }

      EducationFormData formData = _educationFormDataMapper.Map(dbUserEducation.EducationForm);
      EducationTypeData typeData = _educationTypeDataMapper.Map(dbUserEducation.EducationType);

      return new EducationData(
        id: dbUserEducation.Id,
        universityName: dbUserEducation.UniversityName,
        qualificationName: dbUserEducation.QualificationName,
        completeness: ((EducationCompleteness)dbUserEducation.Completeness).ToString(),
        educationForm: formData,
        educationType: typeData,
        admissionAt: dbUserEducation.AdmissionAt,
        issueAt: dbUserEducation.IssueAt);
    }
  }
}
