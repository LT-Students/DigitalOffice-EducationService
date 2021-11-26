using FluentValidation;
using FluentValidation.Validators;
using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using LT.DigitalOffice.EducationService.Validation.Certificates.Interfaces;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;
using System.Collections.Generic;
using LT.DigitalOffice.Kernel.Validators;

namespace LT.DigitalOffice.UserService.Validation.Certificates
{
  public class EditCertificateRequestValidator : BaseEditRequestValidator<EditCertificateRequest>, IEditCertificateRequestValidator
  {
    private void HandleInternalPropertyValidation(Operation<EditCertificateRequest> requestedOperation, CustomContext context)
    {
      Context = context;
      RequestedOperation = requestedOperation;

      #region paths

      AddСorrectPaths(
        new List<string>
        {
          nameof(EditCertificateRequest.Name),
          nameof(EditCertificateRequest.ReceivedAt),
          nameof(EditCertificateRequest.SchoolName),
          nameof(EditCertificateRequest.EducationType),
          nameof(EditCertificateRequest.IsActive)
        });

      AddСorrectOperations(nameof(EditCertificateRequest.Name), new List<OperationType> { OperationType.Replace });
      AddСorrectOperations(nameof(EditCertificateRequest.ReceivedAt), new List<OperationType> { OperationType.Replace });
      AddСorrectOperations(nameof(EditCertificateRequest.SchoolName), new List<OperationType> { OperationType.Replace });
      AddСorrectOperations(nameof(EditCertificateRequest.EducationType), new List<OperationType> { OperationType.Replace });
      AddСorrectOperations(nameof(EditCertificateRequest.IsActive), new List<OperationType> { OperationType.Replace });

      #endregion

      #region conditions

      AddFailureForPropertyIf(
        nameof(EditCertificateRequest.Name),
        o => o == OperationType.Replace,
        new Dictionary<Func<Operation<EditCertificateRequest>, bool>, string>
        {
          { x => !string.IsNullOrEmpty(x.value?.ToString()), "Name is too short."},
          { x => x.value.ToString().Length < 100, "Name is too long."}
        });

      AddFailureForPropertyIf(
        nameof(EditCertificateRequest.SchoolName),
        o => o == OperationType.Replace,
        new Dictionary<Func<Operation<EditCertificateRequest>, bool>, string>
        {
          { x => !string.IsNullOrEmpty(x.value?.ToString()), "School name is too short."},
          { x => x.value.ToString().Length < 100, "School name is too long."}
        });

      AddFailureForPropertyIf(
        nameof(EditCertificateRequest.EducationType),
        o => o == OperationType.Replace,
        new Dictionary<Func<Operation<EditCertificateRequest>, bool>, string>
        {
          { x => Enum.TryParse(typeof(EducationType), x.value?.ToString(), out _), "Incorrect format EducationType"}
        });

      AddFailureForPropertyIf(
        nameof(EditCertificateRequest.ReceivedAt),
        o => o == OperationType.Replace,
        new Dictionary<Func<Operation<EditCertificateRequest>, bool>, string>
        {
          { x => DateTime.TryParse(x.value?.ToString(), out _), "Incorrect format ReceivedAt"}
        });

      AddFailureForPropertyIf(
        nameof(EditCertificateRequest.IsActive),
        o => o == OperationType.Replace,
        new Dictionary<Func<Operation<EditCertificateRequest>, bool>, string>
        {
         { x => bool.TryParse(x.value?.ToString(), out _), "Incorrect format IsActive"}
        });

      #endregion
    }

    public EditCertificateRequestValidator()
    {
      RuleForEach(x => x.Operations)
        .Custom(HandleInternalPropertyValidation);
    }
  }
}
