using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Database;
using LT.DigitalOffice.Kernel.Enums;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.EducationService.Data.Provider
{
  [AutoInject(InjectType.Scoped)]
  public interface IDataProvider : IBaseDataProvider
  {
    DbSet<DbUserEducation> UsersEducations { get; set; }
    DbSet<DbUserCertificate> UsersCertificates { get; set; }
    DbSet<DbCertificateImage> CertificatesImages { get; set; }
  }
}
