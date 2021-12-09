using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Models.Broker.Models.Skill;

namespace LT.DigitalOffice.EducationService.Mappers.Models
{
  public class UserSkillDataMapper : IUserSkillDataMapper
  {
    public UserSkillData Map(DbUserSkill dbUserSkill)
    {
      if (dbUserSkill is null)
      {
        return null;
      }

      return new UserSkillData(
        id: dbUserSkill.Id,
        name: dbUserSkill.Skill.Name
        );
    }
  }
}
