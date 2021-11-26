using FluentValidation;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using LT.DigitalOffice.EducationService.Validation.Certificates.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Models.Broker.Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Validation.Certificates
{
  public class CreateCertificateRequestValidator : AbstractValidator<CreateCertificateRequest>, ICreateCertificateRequestValidator
  {
    private readonly IRequestClient<ICheckUsersExistence> _rcCheckUsersExistence;
    private readonly ILogger<CreateCertificateRequestValidator> _logger;

    public CreateCertificateRequestValidator(
      IRequestClient<ICheckUsersExistence> rcCheckUsersExistence,
      ILogger<CreateCertificateRequestValidator> logger)
    {
      _rcCheckUsersExistence = rcCheckUsersExistence;
      _logger = logger;

      RuleFor(x => x.UserId)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("Wrong user id value.")
        .MustAsync(async (pu, cancellation) => await CheckValidityUserId(pu))
        .WithMessage("User does not exist.");

      RuleFor(x => x.Name)
        .NotEmpty()
        .MaximumLength(100)
        .WithMessage("Name of Certificate is too long.");

      RuleFor(x => x.SchoolName)
        .NotEmpty()
        .MaximumLength(100)
        .WithMessage("Name of school is too long.");

      RuleFor(x => x.EducationType)
        .IsInEnum()
        .WithMessage("Wrong education type value.");
    }

    private async Task<bool> CheckValidityUserId(Guid userId)
    {
      string logMessage = "Cannot check existing user with this id {userId}";

      try
      {
        Response<IOperationResult<ICheckUsersExistence>> response =
          await _rcCheckUsersExistence.GetResponse<IOperationResult<ICheckUsersExistence>>(
            ICheckUsersExistence.CreateObj(new List<Guid> { userId }));

        if (response.Message.IsSuccess)
        {
          return response.Message.Body.UserIds.Any();
        }

        _logger.LogWarning($"Can not find with this Id: {userId}: {Environment.NewLine}{string.Join('\n', response.Message.Errors)}");
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, logMessage);
      }

      return false;
    }
  }
}
