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

    public async Task<List<Guid>> CreateAsync(List<DbCertificateImage> images)
    {
      if (images == null)
      {
        return null;
      }

      _provider.CertificateImages.AddRange(images);
      await _provider.SaveAsync();

      return images.Select(x => x.ImageId).ToList();
    }

    public async Task<bool> RemoveAsync(List<Guid> imagesIds)
    {
      if (imagesIds == null)
      {
        return false;
      }

      IEnumerable<DbCertificateImage> images = _provider.CertificateImages
        .Where(x => imagesIds.Contains(x.ImageId));

      _provider.CertificateImages.RemoveRange(images);
      await _provider.SaveAsync();

      return true;
    }
  }
}
