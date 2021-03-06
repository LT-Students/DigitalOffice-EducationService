using LT.DigitalOffice.EducationService.Business.Commands.Education.Interfaces;
using LT.DigitalOffice.EducationService.Models.Db;
using Moq.AutoMock;
using System;

namespace LT.DigitalOffice.EducationService.Business.UnitTests.EducationsCommandTests
{
  class RemoveEducationCommandTests
  {
    private IRemoveEducationCommand _command;
    private AutoMocker _mocker;

    private Guid _userId;
    private Guid _educationId;
    private DbUserEducation _dbUserEducation;

    /*[SetUp]
    public void SetUp()
    {
      _mocker = new AutoMocker();
      _command = _mocker.CreateInstance<RemoveEducationCommand>();

      _userId = Guid.NewGuid();
      _educationId = Guid.NewGuid();

      _dbUserEducation = new DbUserEducation
      {
        Id = _educationId,
        IsActive = true
      };

      _mocker
        .Setup<IAccessValidator, Task<bool>>(x => x.IsAdminAsync(null))
        .Returns(Task.FromResult(true));

      IDictionary<object, object> _items = new Dictionary<object, object>();
      _items.Add("UserId", _dbUser.Id);

      _mocker
        .Setup<IHttpContextAccessor, IDictionary<object, object>>(x => x.HttpContext.Items)
        .Returns(_items);

      _mocker
        .Setup<IEducationRepository, Task<bool>>(x => x.RemoveAsync(_dbUserEducation))
        .Returns(Task.FromResult(true));

      _mocker
        .Setup<IEducationRepository, DbUserEducation>(x => x.Get(_educationId))
        .Returns(_dbUserEducation);
    }*/

    //[Test]
    //public void ShouldThrowForbiddenExceptionWhenUserHasNotRight()
    //{
    //    var userId = Guid.NewGuid();

    //    IDictionary<object, object> _items = new Dictionary<object, object>();
    //    _items.Add("UserId", userId);

    //    _mocker
    //        .Setup<IHttpContextAccessor, IDictionary<object, object>>(x => x.HttpContext.Items)
    //        .Returns(_items);

    //    _mocker
    //        .Setup<IUserRepository, DbUser>(x => x.Get(userId))
    //        .Returns(new DbUser { IsAdmin = false });

    //    _mocker
    //        .Setup<IAccessValidator, bool>(x => x.HasRights(Rights.AddEditRemoveUsers))
    //        .Returns(false);

    //    Assert.Throws<ForbiddenException>(() => _command.Execute(_educationId));
    //    _mocker.Verify<IEducationRepository, Task<bool>>(x => x.Remove(It.IsAny<DbUserEducation>()), Times.Never);
    //    _mocker.Verify<IUserRepository>(x => x.Get(userId), Times.Once);
    //    _mocker.Verify<IEducationRepository>(x => x.Get(_educationId), Times.Once);
    //}

    //[Test]
    //public void ShouldThrowExceptionWhenRepositoryThrow()
    //{
    //    _mocker
    //        .Setup<IUserRepository>(x => x.Get(It.IsAny<Guid>()))
    //        .Throws(new Exception());

    //    Assert.Throws<Exception>(() => _command.Execute(_educationId));
    //    _mocker.Verify<IEducationRepository, Task<bool>>(x => x.Remove(It.IsAny<DbUserEducation>()), Times.Never);
    //    _mocker.Verify<IUserRepository>(x => x.Get(_dbUser.Id), Times.Once);
    //    _mocker.Verify<IEducationRepository>(x => x.Get(_educationId), Times.Never);
    //}

    /*[Test]
    public async Task ShouldRemoveEducationSuccesfull()
    {
      var expectedResponse = new OperationResultResponse<bool>
      {
        Status = OperationResultStatusType.FullSuccess,
        Body = true
      };

      SerializerAssert.AreEqual(expectedResponse, await _command.ExecuteAsync(_educationId));
      _mocker.Verify<IEducationRepository, Task<bool>>(x => x.RemoveAsync(_dbUserEducation), Times.Once);
      _mocker.Verify<IEducationRepository>(x => x.Get(_educationId), Times.Once);
    }*/
  }
}
