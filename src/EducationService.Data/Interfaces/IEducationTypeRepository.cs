using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data.Interfaces
{
  [AutoInject]
  public interface IEducationTypeRepository
  {
    Task<Guid> CreateAsync(DbEducationType type);

    Task<bool> DoesEducationTypeAlreadyExistAsync(string name);
  }
}
