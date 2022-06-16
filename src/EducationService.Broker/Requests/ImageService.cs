using LT.DigitalOffice.EducationService.Broker.Requests.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Models.Image;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Broker.Requests
{
  public class ImageService : IImageService
  {
    private readonly IRequestClient<ICreateImagesRequest> _rcImage;
    private readonly ILogger<ImageService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ImageService(
      IRequestClient<ICreateImagesRequest> rcImage,
      ILogger<ImageService> logger,
      IHttpContextAccessor httpContextAccessor)
    {
      _rcImage = rcImage;
      _logger = logger;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<Guid>> CreateImagesAsync(List<ImageContent> images, List<string> errors)
    {
      if (images is null || !images.Any())
      {
        return null;
      }

      return images is null || !images.Any()
        ? null
        : (await _rcImage.ProcessRequest<ICreateImagesRequest, ICreateImagesResponse>(
          ICreateImagesRequest.CreateObj(
            images.Select(i => new CreateImageData(name: i.Name, content: i.Content, extension: i.Extension)).ToList(),
            ImageSource.User,
            _httpContextAccessor.HttpContext.GetUserId()),
          errors,
          _logger))
        .ImagesIds;
    }
  }
}
