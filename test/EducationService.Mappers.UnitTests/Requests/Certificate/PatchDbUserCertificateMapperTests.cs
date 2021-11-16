using LT.DigitalOffice.UnitTestKernel;
using LT.DigitalOffice.EducationService.Mappers.Models;
using LT.DigitalOffice.EducationService.Models.Db;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Certificates;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Images;

namespace LT.DigitalOffice.EducationService.Mappers.Db.UnitTests
{
    public class PatchDbUserCertificateMapperTests
    {
        private PatchDbUserCertificateMapper _mapper;

        private Guid _imageId = Guid.NewGuid();
        private JsonPatchDocument<EditCertificateRequest> _request;
        private JsonPatchDocument<DbUserCertificate> _dbRequest;

        [SetUp]
        public void SetUp()
        {
            _mapper = new();

            var time = DateTime.UtcNow;
            var userId = Guid.NewGuid();

            _request = new JsonPatchDocument<EditCertificateRequest>(
                new List<Operation<EditCertificateRequest>> {
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
                        value = time
                    },
                    new Operation<EditCertificateRequest>
                    {
                        op = "replace",
                        path = $"/{nameof(EditCertificateRequest.Image)}",
                        value = new ImageContent()
                    },
                    new Operation<EditCertificateRequest>
                    {
                        op = "replace",
                        path = $"/{nameof(EditCertificateRequest.IsActive)}",
                        value = false
                    },
                    new Operation<EditCertificateRequest>
                    {
                        op = "replace",
                        path = $"/{nameof(EditCertificateRequest.UserId)}",
                        value = userId
                    }
                },
                new CamelCasePropertyNamesContractResolver());

            _dbRequest = new JsonPatchDocument<DbUserCertificate>(
                new List<Operation<DbUserCertificate>> {
                    new Operation<DbUserCertificate>
                    {
                        op = "replace",
                        path = $"/{nameof(DbUserCertificate.Name)}",
                        value = "NewName"
                    },
                    new Operation<DbUserCertificate>
                    {
                        op = "replace",
                        path = $"/{nameof(DbUserCertificate.SchoolName)}",
                        value = "NewSchoolName"
                    },
                    new Operation<DbUserCertificate>
                    {
                        op = "replace",
                        path = $"/{nameof(DbUserCertificate.ReceivedAt)}",
                        value = time
                    },
                    new Operation<DbUserCertificate>
                    {
                        op = "replace",
                        path = $"/{nameof(DbUserCertificate.ImageId)}",
                        value = _imageId
                    },
                    new Operation<DbUserCertificate>
                    {
                        op = "replace",
                        path = $"/{nameof(DbUserCertificate.IsActive)}",
                        value = false
                    },
                    new Operation<DbUserCertificate>
                    {
                        op = "replace",
                        path = $"/{nameof(DbUserCertificate.UserId)}",
                        value = userId
                    }
                },
                new CamelCasePropertyNamesContractResolver());
        }

        [Test]
        public void ShouldMapSuccessful()
        {
            SerializerAssert.AreEqual(_dbRequest, _mapper.Map(_request, _imageId));
        }

        [Test]
        public void ShouldThrowExceptionWhenRequestIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _mapper.Map(null, _imageId));
        }
    }
}
