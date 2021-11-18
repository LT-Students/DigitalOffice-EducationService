using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.EducationService.Business.Commands.Image.Interfaces;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class ImageController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<List<Guid>>> CreateAsync(
      [FromServices] ICreateImageCommand command,
      [FromBody] CreateImagesRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpPost("remove")]
    public async Task<OperationResultResponse<bool>> RemoveAsync(
      [FromServices] IRemoveImagesCommand command,
      [FromBody] RemoveImagesRequest request)
    {
      return await command.ExecuteAsync(request);
    }
  }
}
