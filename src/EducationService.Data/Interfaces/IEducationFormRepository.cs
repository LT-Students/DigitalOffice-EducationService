using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data.Interfaces
{
  [AutoInject]
  public interface IEducationFormRepository
  {
    Task<Guid> CreateAsync(DbEducationForm form);

    Task<bool> DoesEducationFormAlreadyExistAsync(string name);
  }
}
