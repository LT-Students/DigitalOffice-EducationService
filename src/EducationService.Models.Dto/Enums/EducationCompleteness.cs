using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace LT.DigitalOffice.EducationService.Models.Dto.Enums
{
  [JsonConverter(typeof(StringEnumConverter))]
  public enum EducationCompleteness
  {
    Completed,
    Uncompleted
  }
}
