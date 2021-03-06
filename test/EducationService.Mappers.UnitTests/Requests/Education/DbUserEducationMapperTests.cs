using LT.DigitalOffice.UnitTestKernel;
using LT.DigitalOffice.EducationService.Mappers.Db;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Enums;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using NUnit.Framework;
using System;

namespace LT.DigitalOffice.EducationService.Mappers.Db.UnitTests
{
    /*class DbUserEducationMapperTests
    {
        private DbUserEducationMapper _mapper;

        private DbUserEducation _dbEducation;
        private CreateEducationRequest _education;

        [SetUp]
        public void SetUp()
        {
            //_mapper = new DbUserEducationMapper();

            _education = new CreateEducationRequest
            {
                UniversityName = "UniversityName",
                QualificationName = "QualificationName",
                FormEducation = FormEducation.Distance,
                AdmissionAt = DateTime.UtcNow,
                UserId = Guid.NewGuid()
            };

            _dbEducation = new DbUserEducation
            {
                Id = Guid.NewGuid(),
                UniversityName = "UniversityName",
                QualificationName = "QualificationName",
                FormEducation = 1,
                AdmissionAt = _education.AdmissionAt,
                UserId = _education.UserId,
                IsActive = true
            };
        }

        [Test]
        public void ShouldMapSuccesful()
        {
            var result = _mapper.Map(_education);

            _dbEducation.Id = result.Id;

            SerializerAssert.AreEqual(_dbEducation, result);
        }

        [Test]
        public void ShouldThrowExceptionWhenRequestIdNull()
        {
            Assert.Throws<ArgumentNullException>(() => _mapper.Map(null));
        }
    }*/
}
