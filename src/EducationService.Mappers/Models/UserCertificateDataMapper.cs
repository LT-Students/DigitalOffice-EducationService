using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using LT.DigitalOffice.Models.Broker.Models.Education;
using System.Linq;

namespace LT.DigitalOffice.EducationService.Mappers.Models
{
  public class UserCertificateDataMapper : IUserCertificateDataMapper
  {
    public CertificateData Map(DbUserCertificate dbUserCertificate)
    {
      if (dbUserCertificate is null)
      {
        return null;
      }

      return new CertificateData(
        id: dbUserCertificate.Id,
        educationType: ((EducationType)dbUserCertificate.EducationType).ToString(),
        name: dbUserCertificate.Name,
        schoolName: dbUserCertificate.SchoolName,
        receivedAt: dbUserCertificate.ReceivedAt,
        imageId: dbUserCertificate.Images?.FirstOrDefault().ImageId);
    }
  }
}
