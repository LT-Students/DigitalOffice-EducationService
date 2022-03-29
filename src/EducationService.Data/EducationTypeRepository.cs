using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Data.Provider;
using LT.DigitalOffice.EducationService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data
{
  public class EducationTypeRepository : IEducationTypeRepository
  {
    private readonly IDataProvider _provider;

    public EducationTypeRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<Guid> CreateAsync(DbEducationType type)
    {
      _provider.EducationsTypes.Add(type);
      await _provider.SaveAsync();

      return type.Id;
    }

    public async Task<bool> DoesEducationTypeAlreadyExistAsync(string name)
    {
      return await _provider.EducationsTypes.AnyAsync(t => t.Name.Equals(name));
    }
  }
}
