using FluentValidation;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
//using LT.DigitalOffice.EducationService.Validation.Certificates;
using LT.DigitalOffice.EducationService.Validation.Education.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
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
 //   private readonly ILogger<CreateCertificateRequestValidator> _logger;

    public CreateEducationRequestValidator(
      IRequestClient<ICheckUsersExistence> rcCheckUsersExistence
     /* ILogger<CreateCertificateRequestValidator> logger*/)
    {
      _rcCheckUsersExistence = rcCheckUsersExistence;
    //  _logger = logger;

      RuleFor(x => x.UserId)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("Wrong user id value.")
        .MustAsync(async (pu, cancellation) => await CheckValidityUserId(pu))
        .WithMessage("User does not exist.");

      RuleFor(education => education.UniversityName)
        .NotEmpty().WithMessage("University name must not be empty.")
        .MaximumLength(100).WithMessage("University name is too long");

      RuleFor(education => education.QualificationName)
        .NotEmpty().WithMessage("Qualification name must not be empty.")
        .MaximumLength(100).WithMessage("Qualification name is too long");

      //RuleFor(education => education.FormEducation)
      //  .IsInEnum().WithMessage("Wrong form education.");
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

      //  _logger.LogWarning($"Can not find with this Id: {userId}: {Environment.NewLine}{string.Join('\n', response.Message.Errors)}");
      }
      catch (Exception exc)
      {
       // _logger.LogError(exc, logMessage);
      }

      return false;
    }
  }
}
