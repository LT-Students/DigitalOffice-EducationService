using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Models.Broker.Models.Education;

namespace LT.DigitalOffice.EducationService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IUserCertificateDataMapper
  {
    CertificateData Map(DbUserCertificate dbUserCertificate);
  }
}
