using LT.DigitalOffice.EducationService.Mappers.Patch.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;

namespace LT.DigitalOffice.EducationService.Patch.Models
{
  public class PatchDbUserCertificateMapper : IPatchDbUserCertificateMapper
  {
    public JsonPatchDocument<DbUserCertificate> Map(JsonPatchDocument<EditCertificateRequest> request)
    {
      if (request is null)
      {
        return null;
      }

      JsonPatchDocument<DbUserCertificate> result = new JsonPatchDocument<DbUserCertificate>();

      foreach (Operation<EditCertificateRequest> item in request.Operations)
      {
        if (item.path.EndsWith(nameof(EditCertificateRequest.EducationType), StringComparison.OrdinalIgnoreCase))
        {
          result.Operations.Add(new Operation<DbUserCertificate>(item.op, item.path, item.from, (int)Enum.Parse(typeof(EducationType), item.value.ToString())));
          continue;
        }
        result.Operations.Add(new Operation<DbUserCertificate>(item.op, item.path, item.from, item.value));
      }

      return result;
    }
  }
}
