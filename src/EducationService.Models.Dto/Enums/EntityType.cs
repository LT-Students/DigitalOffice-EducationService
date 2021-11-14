using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LT.DigitalOffice.EducationService.Models.Dto.Enums
{
  [JsonConverter(typeof(StringEnumConverter))]
  public enum EntityType
  {
    User,
    Certificate,
    Education
  }
}
