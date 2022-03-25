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
      _provider.EducationTypes.Add(type);
      await _provider.SaveAsync();

      return type.Id;
    }

    public async Task<bool> DoesEducationTypeAlreadyExistAsync(string typeName)
    {
      return await _provider.EducationForms.AnyAsync(s => s.Name.ToLower() == typeName.ToLower());
    }
  }
}
