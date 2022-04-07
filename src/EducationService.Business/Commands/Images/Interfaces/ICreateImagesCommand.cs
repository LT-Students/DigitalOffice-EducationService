using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.Image.Interfaces
{
  [AutoInject]
  public interface ICreateImagesCommand
  {
    Task<OperationResultResponse<List<Guid>>> ExecuteAsync(CreateImagesRequest request);
  }
}
