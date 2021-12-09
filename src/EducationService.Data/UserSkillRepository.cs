using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Data.Provider;
using LT.DigitalOffice.EducationService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data
{
  public class UserSkillRepository : IUserSkillRepository
  {
    private readonly IDataProvider _provider;

    public UserSkillRepository(
      IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<List<DbUserSkill>> FindAsync(Guid userId)
    {
      return (
        await _provider.UsersSkills
        .Include(us => us.Skill)
        .Where(us => us.UserId == userId)
        .ToListAsync());
    }
  }
}
