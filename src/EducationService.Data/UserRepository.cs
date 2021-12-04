using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Data.Provider;
using LT.DigitalOffice.EducationService.Models.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly IDataProvider _provider;

    public UserRepository(
      IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<(List<DbUserCertificate> userCertificates, List<DbUserEducation> userEducations)> 
      GetAsync(Guid userId)
    {
      return (
        await _provider.UsersCertificates
          .Include(uc => uc.Images)
          .Where(uc => uc.UserId == userId)
          .ToListAsync(),
        await _provider.UsersEducations
          .Where(uc => uc.UserId == userId)
          .ToListAsync());
    }
  }
}
