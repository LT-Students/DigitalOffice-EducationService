using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;
using LT.DigitalOffice.EducationService.Models.Dto.Enums;

namespace LT.DigitalOffice.EducationService.Mappers.Models
{
  public class PatchDbUserEducationMapper : IPatchDbUserEducationMapper
  {
    public JsonPatchDocument<DbUserEducation> Map(JsonPatchDocument<EditEducationRequest> request)
    {
      if (request == null)
      {
        return null;
      }

      JsonPatchDocument<DbUserEducation> dbUserEducation = new();

      foreach (var item in request.Operations)
      {
        if (item.path.ToUpper().EndsWith(nameof(EditEducationRequest.FormEducation).ToUpper()))
        {
          if (Enum.TryParse(item.value.ToString(), out FormEducation education))
          {
            dbUserEducation.Operations.Add(new Operation<DbUserEducation>(
                item.op, $"/{nameof(EditEducationRequest.FormEducation)}", item.from, (int)education));
            continue;
          }
        }

        dbUserEducation.Operations.Add(new Operation<DbUserEducation>(item.op, item.path, item.from, item.value));
      }

      return dbUserEducation;
    }
  }
}
