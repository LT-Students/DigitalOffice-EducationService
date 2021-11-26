using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;
using System;

namespace LT.DigitalOffice.EducationService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IPatchDbUserCertificateMapper
  {
    JsonPatchDocument<DbUserCertificate> Map(JsonPatchDocument<EditCertificateRequest> request);
  }
}
