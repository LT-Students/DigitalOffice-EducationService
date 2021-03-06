using LT.DigitalOffice.EducationService.Business.Commands.Education.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using LT.DigitalOffice.EducationService.Models.Dto.Requests.Education;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.UnitTestKernel;
using LT.DigitalOffice.EducationService.Data.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.EducationService.Business.UnitTests.EducationsCommandTests
{
  class EditEducationCommandTests
  {
    private IEditEducationCommand _command;
    private AutoMocker _mocker;

    private JsonPatchDocument<EditEducationRequest> _request;
    private JsonPatchDocument<DbUserEducation> _dbRequest;

    private Guid _userId;
    private Guid _educationId;
    private DbUserEducation _dbUserEducation;

    /*[SetUp]
    public void SetUp()
    {
        _mocker = new AutoMocker();
        _command = _mocker.CreateInstance<EditEducationCommand>();

        _userId = Guid.NewGuid();
        _educationId = Guid.NewGuid();

        _dbUserEducation = new DbUserEducation
        {
            Id = _educationId,
            UserId = _userId,
            UniversityName = "UniversityName",
            QualificationName = "QualificationName",
            AdmissionAt = DateTime.UtcNow,
            IssueAt = DateTime.UtcNow,
            FormEducation = 1
        };

        #region requests initialization

        var time = DateTime.UtcNow;

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
                        time),
                    new Operation<EditEducationRequest>(
                        "replace",
                        $"/{nameof(EditEducationRequest.IssueAt)}",
                        "",
                        time),
                    new Operation<EditEducationRequest>(
                        "replace",
                        $"/{nameof(EditEducationRequest.FormEducation)}",
                        "",
                        0)
                }, new CamelCasePropertyNamesContractResolver()
            );

        _dbRequest = new JsonPatchDocument<DbUserEducation>(
            new List<Operation<DbUserEducation>>
                {
                    new Operation<DbUserEducation>(
                        "replace",
                        $"/{nameof(DbUserEducation.UniversityName)}",
                        "",
                        "New University name"),
                    new Operation<DbUserEducation>(
                        "replace",
                        $"/{nameof(DbUserEducation.QualificationName)}",
                        "",
                        "New Qualification name"),
                    new Operation<DbUserEducation>(
                        "replace",
                        $"/{nameof(DbUserEducation.AdmissionAt)}",
                        "",
                        time),
                    new Operation<DbUserEducation>(
                        "replace",
                        $"/{nameof(DbUserEducation.IssueAt)}",
                        "",
                        time),
                    new Operation<DbUserEducation>(
                        "replace",
                        $"/{nameof(DbUserEducation.FormEducation)}",
                        "",
                        0)
                }, new CamelCasePropertyNamesContractResolver());

        #endregion

        IDictionary<object, object> _items = new Dictionary<object, object>();
        _items.Add("UserId", _dbUser.Id);

        _mocker
            .Setup<IHttpContextAccessor, IDictionary<object, object>>(x => x.HttpContext.Items)
            .Returns(_items);

        _mocker
            .Setup<IPatchDbUserEducationMapper, JsonPatchDocument<DbUserEducation>>(x => x.Map(_request))
            .Returns(_dbRequest);

        _mocker
            .Setup<IEditEducationRequestValidator, bool>(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
            .Returns(true);

        _mocker
            .Setup<IEducationRepository, Task<bool>>(x => x.EditAsync(It.IsAny<DbUserEducation>(), It.IsAny<JsonPatchDocument<DbUserEducation>>()))
            .Returns(Task.FromResult(true));

        _mocker
            .Setup<IEducationRepository, DbUserEducation>(x => x.Get(_educationId))
            .Returns(_dbUserEducation);
    }*/

    //[Test]
    //public void ShouldThrowForbiddenExceptionWhenUserHasNotRight()
    //{
    //    _mocker
    //        .Setup<IUserRepository, DbUser>(x => x.Get(_dbUser.Id))
    //        .Returns(new DbUser { IsAdmin = false });

    //    _mocker
    //        .Setup<IAccessValidator, bool>(x => x.HasRights(Rights.AddEditRemoveUsers))
    //        .Returns(false);

    //    _dbUserEducation.UserId = Guid.NewGuid();

    //    Assert.Throws<ForbiddenException>(() => _command.Execute(_educationId, _request));
    //    _mocker.Verify<IEducationRepository, Task<bool>>(x => x.Edit(It.IsAny<DbUserEducation>(), It.IsAny<JsonPatchDocument<DbUserEducation>>()),
    //        Times.Never);
    //    _mocker.Verify<IEducationRepository, DbUserEducation>(x => x.Get(_educationId),
    //        Times.Once);
    //    _mocker.Verify<IUserRepository, DbUser>(x => x.Get(_dbUser.Id),
    //        Times.Once);
    //}

    //[Test]
    //public void ShouldThrowValidationExceptionWhenValidationInFailed()
    //{
    //    _mocker
    //        .Setup<IEditEducationRequestValidator, bool>(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
    //        .Returns(false);

    //    Assert.Throws<ValidationException>(() => _command.Execute(_educationId, _request));
    //    _mocker.Verify<IEducationRepository, Task<bool>>(x => x.Edit(It.IsAny<DbUserEducation>(), It.IsAny<JsonPatchDocument<DbUserEducation>>()),
    //        Times.Never);
    //    _mocker.Verify<IEducationRepository, DbUserEducation>(x => x.Get(_educationId),
    //        Times.Once);
    //    _mocker.Verify<IUserRepository, DbUser>(x => x.Get(_dbUser.Id),
    //        Times.Once);
    //}

    //[Test]
    //public void ShouldThrowExceptionWhenRepositoryThrow()
    //{
    //    _mocker
    //        .Setup<IUserRepository>(x => x.Get(_dbUser.Id))
    //        .Throws(new Exception());

    //    Assert.Throws<Exception>(async () => await _command.Execute(_educationId, _request));
    //    _mocker.Verify<IEducationRepository, Task<bool>>(x => x.Edit(_dbUserEducation, _dbRequest),
    //        Times.Never);
    //    _mocker.Verify<IEducationRepository, DbUserEducation>(x => x.Get(It.IsAny<Guid>()),
    //        Times.Never);
    //    _mocker.Verify<IUserRepository, DbUser>(x => x.Get(_dbUser.Id),
    //        Times.Once);
    //}

    /*[Test]
    public async Task ShouldEditEducationSuccesfull()
    {
      var expectedResponse = new OperationResultResponse<bool>
      {
        Status = OperationResultStatusType.FullSuccess,
        Body = true
      };

      SerializerAssert.AreEqual(expectedResponse, await _command.ExecuteAsync(_educationId, _request));
      _mocker.Verify<IEducationRepository, Task<bool>>(x => x.EditAsync(_dbUserEducation, _dbRequest),
          Times.Once);
      _mocker.Verify<IEducationRepository, DbUserEducation>(x => x.Get(_educationId),
          Times.Once);
    }*/
  }
}
