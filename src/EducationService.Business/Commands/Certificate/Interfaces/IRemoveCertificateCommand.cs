﻿using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.Commands.Certificate.Interfaces
{
  [AutoInject]
  public interface IRemoveCertificateCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid certificateId);
  }
}