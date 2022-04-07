using LT.DigitalOffice.EducationService.Mappers.Patch.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;

namespace LT.DigitalOffice.EducationService.Patch.Models
{
  public class PatchDbUserEducationMapper : IPatchDbUserEducationMapper
  {
    public JsonPatchDocument<DbUserEducation> Map(JsonPatchDocument<EditEducationRequest> request)
    {

      if (request is null)
      {
        return null;
      }

      JsonPatchDocument<DbUserEducation> dbUserEducation = new();

      foreach (Operation<EditEducationRequest> item in request.Operations)
      {
        if (item.path.EndsWith(nameof(EditEducationRequest.Completeness), StringComparison.OrdinalIgnoreCase))
        {
          dbUserEducation.Operations.Add(new Operation<DbUserEducation>(
            item.op, item.path, item.from, (int)Enum.Parse(typeof(EducationCompleteness), item.value.ToString())));

            continue;
        }
        dbUserEducation.Operations.Add(new Operation<DbUserEducation>(item.op, item.path, item.from, item.value));
      }
      return dbUserEducation;          
    }
  }
}
