using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.EducationService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbUserEducationMapper
  {
    DbUserEducation Map(CreateEducationRequest request, List<Guid> filesIds);
  }
}
