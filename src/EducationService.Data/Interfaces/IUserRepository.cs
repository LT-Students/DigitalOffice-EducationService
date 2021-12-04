using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data.Interfaces
{
  [AutoInject]
  public interface IUserRepository
  {
    Task<(List<DbUserCertificate> userCertificates, List<DbUserEducation> userEducations)>
      GetAsync(Guid userId);
  }
}
