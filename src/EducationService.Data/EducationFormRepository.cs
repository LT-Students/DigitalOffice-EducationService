using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Data.Provider;
using LT.DigitalOffice.EducationService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data
{
  public class EducationFormRepository : IEducationFormRepository
  {
    private readonly IDataProvider _provider;

    public EducationFormRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<Guid> CreateAsync(DbEducationForm form)
    {
      _provider.EducationForms.Add(form);
      await _provider.SaveAsync();

      return form.Id;
    }

    public async Task<bool> DoesEducationFormAlreadyExistAsync(string formName)
    {
      return await _provider.EducationForms.AnyAsync(s => s.Name.ToLower() == formName.ToLower());
    }
  }
}
