using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Data.Interfaces
{
  [AutoInject]
  public interface IImageRepository
  {
    Task<List<Guid>> CreateAsync(List<DbEducationImage> images);

    Task<bool> RemoveAsync(List<Guid> imagesIds);
  }
}
