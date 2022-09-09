using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Models;

namespace LT.DigitalOffice.EducationService.Mappers.Models;

public class EducationInfoMapper : IEducationInfoMapper
{
  private readonly IEducationFormInfoMapper _formInfoMapper;
  private readonly IEducationTypeInfoMapper _typeInfoMapper;

  public EducationInfoMapper(
    IEducationFormInfoMapper formInfoMapper,
    IEducationTypeInfoMapper typeInfoMapper)
  {
    _formInfoMapper = formInfoMapper;
    _typeInfoMapper = typeInfoMapper;
  }

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
        EducationForm = _formInfoMapper.Map(dbUserEducation),
        EducationType = _typeInfoMapper.Map(dbUserEducation),
        Completeness = dbUserEducation.Completeness,
        AdmissionAt = dbUserEducation.AdmissionAt,
        IssueAt = dbUserEducation.IssueAt
      };
  }
}
