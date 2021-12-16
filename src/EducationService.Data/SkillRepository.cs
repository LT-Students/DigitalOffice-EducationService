using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Data.Provider;
using LT.DigitalOffice.EducationService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data
{
  public class SkillRepository : ISkillRepository
  {
    private readonly IDataProvider _provider;

    public SkillRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<Guid?> CreateAsync(DbSkill skill)
    {
      if (skill is null)
      {
        return null;
      }

      _provider.Skills.Add(skill);
      await _provider.SaveAsync();

      return skill.Id;
    }

    public async Task<bool> DoesSkillAlreadyExistAsync(string skillName)
    {
      return await _provider.Skills.AnyAsync(s => s.Name.ToLower() == skillName.ToLower());
    }
  }
}
