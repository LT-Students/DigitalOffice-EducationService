using FluentValidation;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.EducationService.Validation.Education.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Models.Broker.Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

      RuleFor(x => x.UserId)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("Wrong user id value.")
        .MustAsync(async (pu, cancellation) => await CheckValidityUserId(pu, new List<string>()))
        .WithMessage("User does not exist.");

      RuleFor(education => education.UniversityName)
        .NotEmpty().WithMessage("University name must not be empty.");

      RuleFor(education => education.QualificationName)
        .NotEmpty().WithMessage("Qualification name must not be empty.");

      RuleFor(education => education.Completeness)
        .IsInEnum().WithMessage("Wrong form completeness of education.");
    }

    private async Task<bool> CheckValidityUserId(Guid userId, List<string> errors)
    {
      return await RequestHandler.ProcessRequest<ICheckUsersExistence, bool>(
          _rcCheckUsersExistence,
          ICheckUsersExistence.CreateObj(new List<Guid> { userId }),
          errors,
          _logger);
    }
  }
}
