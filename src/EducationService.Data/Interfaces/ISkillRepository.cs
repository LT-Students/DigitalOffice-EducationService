using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data.Interfaces
{
  [AutoInject]
  public interface ISkillRepository
  {
    Task<Guid?> CreateAsync(DbSkill skill);

    Task<bool> DoesSkillAlreadyExistAsync(string skillName);
  }
}