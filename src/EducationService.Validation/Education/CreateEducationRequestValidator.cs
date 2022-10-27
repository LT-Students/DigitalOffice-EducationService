using FluentValidation;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.EducationService.Validation.Education.Interfaces;
using LT.DigitalOffice.EducationService.Validation.Education.Resources;
using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Models.Broker.Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Validation.Education
{
  public class CreateEducationRequestValidator : AbstractValidator<CreateEducationRequest>, ICreateEducationRequestValidator
  {
    private readonly IRequestClient<ICheckUsersExistence> _rcCheckUsersExistence;
    private readonly ILogger<CreateEducationRequestValidator> _logger;

    public CreateEducationRequestValidator(
      IRequestClient<ICheckUsersExistence> rcCheckUsersExistence,
      ILogger<CreateEducationRequestValidator> logger)
    {
      _rcCheckUsersExistence = rcCheckUsersExistence;
      _logger = logger;
      
      Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");

      RuleFor(x => x.UserId)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage($"{nameof(CreateEducationRequest.UserId)} {EducationValidatorResource.IsEmpty}")
        .MustAsync(async (pu, cancellation) => await CheckValidityUserId(pu, new List<string>()))
        .WithMessage($"{EducationValidatorResource.UserDoesNotExist}");

      RuleFor(education => education.UniversityName)
        .NotEmpty().WithMessage($"{nameof(CreateEducationRequest.UniversityName)} {EducationValidatorResource.IsEmpty}");

      RuleFor(education => education.QualificationName)
        .NotEmpty().WithMessage($"{nameof(CreateEducationRequest.QualificationName)} {EducationValidatorResource.IsEmpty}");

      RuleFor(education => education.Completeness)
        .IsInEnum().WithMessage($"{nameof(CreateEducationRequest.Completeness)} {EducationValidatorResource.IsNotInEnum}");
    }

    private async Task<bool> CheckValidityUserId(Guid userId, List<string> errors)
    {
      ICheckUsersExistence check = await RequestHandler.ProcessRequest<ICheckUsersExistence, ICheckUsersExistence>(
          _rcCheckUsersExistence,
          ICheckUsersExistence.CreateObj(new List<Guid> { userId }),
          errors,
          _logger);

      if(check != null)
      {
        return check.UserIds.Any();
      }

      return false;
    }
  }
}
