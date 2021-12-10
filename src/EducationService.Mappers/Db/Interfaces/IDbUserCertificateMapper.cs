using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.EducationService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbUserCertificateMapper
  {
    DbUserCertificate Map(CreateCertificateRequest request, List<Guid> filesIds);
  }
}
