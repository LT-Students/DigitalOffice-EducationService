using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.EducationService.Mappers.Models.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.EducationService.Mappers.Models
{
  public class CreateImageDataMapper : ICreateImageDataMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateImageDataMapper(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public List<CreateImageData> Map(List<ImageContent> request)
    {
      if (request is null)
      {
        return null;
      }

      return request.Select(x => new CreateImageData(
        x.Name,
        x.Content,
        x.Extension,
        _httpContextAccessor.HttpContext.GetUserId())).ToList();
    }

    public List<CreateImageData> Map(string name, string content, string extension)
    {
      return (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(extension))
        ? null
        : new() { new CreateImageData(name, content, extension, _httpContextAccessor.HttpContext.GetUserId()) };
    }
  }
}
