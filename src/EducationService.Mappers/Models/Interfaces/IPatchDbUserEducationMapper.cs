using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.User.Education;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.EducationService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IPatchDbUserEducationMapper
  {
    JsonPatchDocument<DbUserEducation> Map(JsonPatchDocument<EditEducationRequest> request);
  }
}
