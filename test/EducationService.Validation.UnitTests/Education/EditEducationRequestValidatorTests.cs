﻿using FluentValidation.TestHelper;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.EducationService.Validation.Education;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.EducationService.Validation.UnitTests.Education
{
    class EditEducationRequestValidatorTests
    {
       /* private EditEducationRequestValidator _validator;
        private JsonPatchDocument<EditEducationRequest> _request;

        [OneTimeSetUp]
        public void SetUp()
        {
            _validator = new EditEducationRequestValidator();
        }

        [Test]
        public void ShouldNotThrowAnyException()
        {
            _request = new JsonPatchDocument<EditEducationRequest>(
                new List<Operation<EditEducationRequest>>
                {
                        new Operation<EditEducationRequest>(
                            "replace",
                            $"/{nameof(EditEducationRequest.UniversityName)}",
                            "",
                            "New University name"),
                        new Operation<EditEducationRequest>(
                            "replace",
                            $"/{nameof(EditEducationRequest.QualificationName)}",
                            "",
                            "New Qualification name"),
                        new Operation<EditEducationRequest>(
                            "replace",
                            $"/{nameof(EditEducationRequest.AdmissionAt)}",
                            "",
                            DateTime.UtcNow),
                        new Operation<EditEducationRequest>(
                            "replace",
                            $"/{nameof(EditEducationRequest.IssueAt)}",
                            "",
                            DateTime.UtcNow)
                }, new CamelCasePropertyNamesContractResolver());

            _validator.TestValidate(_request).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void ShouldThrowExceptionWhenUniversityNameIncorrect()
        {
            _request = new JsonPatchDocument<EditEducationRequest>(
                new List<Operation<EditEducationRequest>>
                {
                        new Operation<EditEducationRequest>(
                            "replace",
                            $"/{nameof(EditEducationRequest.UniversityName)}",
                            "",
                            "")
                }, new CamelCasePropertyNamesContractResolver());

            _validator.TestValidate(_request).ShouldHaveAnyValidationError();
        }

        [Test]
        public void ShouldThrowExceptionWhenQualificationNameIncorrect()
        {
            _request = new JsonPatchDocument<EditEducationRequest>(
                new List<Operation<EditEducationRequest>>
                {
                        new Operation<EditEducationRequest>(
                            "replace",
                            $"/{nameof(EditEducationRequest.QualificationName)}",
                            "",
                            "")
                }, new CamelCasePropertyNamesContractResolver());

            _validator.TestValidate(_request).ShouldHaveAnyValidationError();
        }

        [Test]
        public void ShouldThrowExceptionWhenFormEducationIncorrect()
        {
            _request = new JsonPatchDocument<EditEducationRequest>(
                new List<Operation<EditEducationRequest>>
                {
                        new Operation<EditEducationRequest>(
                            "replace",
                            $"/formeducation",
                            "",
                            "fulltime")
                }, new CamelCasePropertyNamesContractResolver());

            _validator.TestValidate(_request).ShouldHaveAnyValidationError();
        }

        [Test]
        public void ShouldThrowExceptionWhenIsActiveIncorrect()
        {
            _request = new JsonPatchDocument<EditEducationRequest>(
                new List<Operation<EditEducationRequest>>
                {
                        new Operation<EditEducationRequest>(
                            "replace",
                            $"/isactive",
                            "",
                            "not bool")
                }, new CamelCasePropertyNamesContractResolver());

            _validator.TestValidate(_request).ShouldHaveAnyValidationError();
        }

        [Test]
        public void ShouldThrowExceptionWhenAdmissionAtIncorrect()
        {
            _request = new JsonPatchDocument<EditEducationRequest>(
                new List<Operation<EditEducationRequest>>
                {
                        new Operation<EditEducationRequest>(
                            "replace",
                            $"/admissionat",
                            "",
                            "not time")
                }, new CamelCasePropertyNamesContractResolver());

            _validator.TestValidate(_request).ShouldHaveAnyValidationError();
        }

        [Test]
        public void ShouldThrowExceptionWhenIssueAtIncorrect()
        {
            _request = new JsonPatchDocument<EditEducationRequest>(
                new List<Operation<EditEducationRequest>>
                {
                        new Operation<EditEducationRequest>(
                            "replace",
                            $"/issueat",
                            "",
                            "not time")
                }, new CamelCasePropertyNamesContractResolver());

            _validator.TestValidate(_request).ShouldHaveAnyValidationError();
        }*/
    }
}
