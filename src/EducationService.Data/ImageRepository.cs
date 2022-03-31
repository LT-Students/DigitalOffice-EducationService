using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using LT.DigitalOffice.EducationService.Data.Provider;
using LT.DigitalOffice.EducationService.Models.Db;

namespace LT.DigitalOffice.EducationService.Data
{
  public class ImageRepository : IImageRepository
  {
    private readonly IDataProvider _provider;

    public ImageRepository(
      IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<List<Guid>> CreateAsync(List<DbEducationImage> images)
    {
      if (images is null)
      {
        return null;
      }

      _provider.EducationsImages.AddRange(images);
      await _provider.SaveAsync();

      return images.Select(x => x.ImageId).ToList();
    }

    public async Task<bool> RemoveAsync(List<Guid> imagesIds)
    {
      if (imagesIds is null)
      {
        return false;
      }

      IEnumerable<DbEducationImage> images = _provider.EducationsImages
        .Where(x => imagesIds.Contains(x.ImageId));

      _provider.EducationsImages.RemoveRange(images);
      await _provider.SaveAsync();

      return true;
    }
  }
}
