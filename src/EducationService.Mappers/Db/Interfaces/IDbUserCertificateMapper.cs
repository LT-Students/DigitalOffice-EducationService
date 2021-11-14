using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using System;

namespace LT.DigitalOffice.EducationService.Mappers.Db.Interfaces
{
    [AutoInject]
    public interface IDbUserCertificateMapper
    {
        DbUserCertificate Map(CreateCertificateRequest request, Guid imageId);
    }
}
