using FluentValidation.TestHelper;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;
using LT.DigitalOffice.UserService.Validation.Certificates;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace LT.DigitalOffice.EducationService.Validation.UnitTests.Certificate
{
  public class EditCertificateRequestValidatorTests
  {
    private EditCertificateRequestValidator _validator;

    private Guid _existUser = Guid.NewGuid();

    [SetUp]
    public void SetUp()
    {
      _validator = new();
    }

    [Test]
    public void ShouldValidateWhenRequestIsCorrect()
    {
      var editCertificateRequest = new JsonPatchDocument<EditCertificateRequest>(
      new List<Operation<EditCertificateRequest>>
      {
        new Operation<EditCertificateRequest>
        {
            op = "replace",
            path = $"/{nameof(EditCertificateRequest.Name)}",
            value = "NewName"
        },
        new Operation<EditCertificateRequest>
        {
            op = "replace",
            path = $"/{nameof(EditCertificateRequest.SchoolName)}",
            value = "NewSchoolName"
        },
        new Operation<EditCertificateRequest>
        {
            op = "replace",
            path = $"/{nameof(EditCertificateRequest.ReceivedAt)}",
            value = DateTime.UtcNow
        },
        new Operation<EditCertificateRequest>
        {
            op = "replace",
            path = $"/{nameof(EditCertificateRequest.Image)}",
            value = JsonSerializer.Serialize(new AddImageRequest
            {
                Name = "Test",
                Content = Properties.Resources.Base64String,
                Extension = ".jpg"
            })
        },
        new Operation<EditCertificateRequest>
        {
            op = "replace",
            path = $"/{nameof(EditCertificateRequest.IsActive)}",
            value = false
        }
      },
      new CamelCasePropertyNamesContractResolver());

      _validator.TestValidate(editCertificateRequest).ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void ShouldthrowExceptionWhenNameIsIncorrect()
    {
      var editCertificateRequest = new JsonPatchDocument<EditCertificateRequest>(
          new List<Operation<EditCertificateRequest>>
          {
                    new Operation<EditCertificateRequest>
                    {
                        op = "replace",
                        path = $"/{nameof(EditCertificateRequest.Name)}",
                        value = ""
                    }
          },
          new CamelCasePropertyNamesContractResolver());

      _validator.TestValidate(editCertificateRequest).ShouldHaveAnyValidationError();
    }

    [Test]
    public void ShouldthrowExceptionWhenSchoolNameIsIncorrect()
    {
      var editCertificateRequest = new JsonPatchDocument<EditCertificateRequest>(
          new List<Operation<EditCertificateRequest>>
          {
                    new Operation<EditCertificateRequest>
                    {
                        op = "replace",
                        path = $"/{nameof(EditCertificateRequest.SchoolName)}",
                        value = ""
                    },
          },
          new CamelCasePropertyNamesContractResolver());

      _validator.TestValidate(editCertificateRequest).ShouldHaveAnyValidationError();
    }

    [Test]
    public void ShouldthrowExceptionWhenImageIsIncorrect()
    {
      var editCertificateRequest = new JsonPatchDocument<EditCertificateRequest>(
          new List<Operation<EditCertificateRequest>>
          {
                    new Operation<EditCertificateRequest>
                    {
                        op = "replace",
                        path = $"/{nameof(EditCertificateRequest.Image)}",
                        value = "wrong obj"
                    },
          },
          new CamelCasePropertyNamesContractResolver());

      _validator.TestValidate(editCertificateRequest).ShouldHaveAnyValidationError();
    }

    [Test]
    public void ShouldthrowExceptionWhenReceivedAtIsIncorrect()
    {
      var editCertificateRequest = new JsonPatchDocument<EditCertificateRequest>(
          new List<Operation<EditCertificateRequest>>
          {
                    new Operation<EditCertificateRequest>
                    {
                        op = "replace",
                        path = $"/{nameof(EditCertificateRequest.ReceivedAt)}",
                        value = "wrong obj"
                    },
          },
          new CamelCasePropertyNamesContractResolver());

      _validator.TestValidate(editCertificateRequest).ShouldHaveAnyValidationError();
    }

    [Test]
    public void ShouldthrowExceptionWhenIsActiveIsIncorrect()
    {
      var editCertificateRequest = new JsonPatchDocument<EditCertificateRequest>(
          new List<Operation<EditCertificateRequest>>
          {
                    new Operation<EditCertificateRequest>
                    {
                        op = "replace",
                        path = $"/{nameof(EditCertificateRequest.IsActive)}",
                        value = "wrong obj"
                    },
          },
          new CamelCasePropertyNamesContractResolver());

      _validator.TestValidate(editCertificateRequest).ShouldHaveAnyValidationError();
    }
  }
}
