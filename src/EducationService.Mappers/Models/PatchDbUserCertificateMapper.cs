using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;

namespace LT.DigitalOffice.EducationService.Mappers.Models
{
  public class PatchDbUserCertificateMapper : IPatchDbUserCertificateMapper
  {
    public JsonPatchDocument<DbUserCertificate> Map(JsonPatchDocument<EditCertificateRequest> request, Guid? imageId)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var result = new JsonPatchDocument<DbUserCertificate>();

      foreach (var item in request.Operations)
      {
        if (item.path.EndsWith(nameof(EditCertificateRequest.EducationType), StringComparison.OrdinalIgnoreCase))
        {
          result.Operations.Add(new Operation<DbUserCertificate>(item.op, item.path, item.from, (int)Enum.Parse(typeof(EducationType), item.value.ToString())));
          continue;
        }
        if (item.path.EndsWith(nameof(EditCertificateRequest.Image), StringComparison.OrdinalIgnoreCase) && imageId.HasValue)
        {
          result.Operations.Add(new Operation<DbUserCertificate>(item.op, $"/{nameof(DbUserCertificate.ImageId)}", item.from, imageId.Value));
          continue;
        }
        result.Operations.Add(new Operation<DbUserCertificate>(item.op, item.path, item.from, item.value));
      }

      return result;
    }
  }
}
