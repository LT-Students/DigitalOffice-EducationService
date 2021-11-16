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
    DbSet<DbUserEducation> UserEducations { get; set; }
    DbSet<DbUserCertificate> UserCertificates { get; set; }
    DbSet<DbCertificateImage> CertificateImages { get; set; }
  }
}
