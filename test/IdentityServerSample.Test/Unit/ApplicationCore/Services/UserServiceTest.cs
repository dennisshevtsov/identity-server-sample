﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services.Test
{
  using IdentityServerSample.ApplicationCore.Identities;

  [TestClass]
  public sealed class UserServiceTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<IUserScopeRepository> _userScopeRepositoryMock;

    private UserService _userService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _userRepositoryMock = new Mock<IUserRepository>();
      _userScopeRepositoryMock = new Mock<IUserScopeRepository>();

      _userService = new UserService(
        _userRepositoryMock.Object, _userScopeRepositoryMock.Object);
    }

    [TestMethod]
    public async Task AddUserAsync_Should_Add_User_With_Scopes()
    {
      _userRepositoryMock.Setup(repository => repository.AddUserAsync(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Verifiable();

      _userScopeRepositoryMock.Setup(repository => repository.AddUserScopesAsync(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()))
                              .Returns(Task.CompletedTask)
                              .Verifiable();

      var controlUserEntity = new UserEntity();

      await _userService.AddUserAsync(controlUserEntity, _cancellationToken);

      _userRepositoryMock.Verify(repository => repository.AddUserAsync(controlUserEntity, _cancellationToken));
      _userRepositoryMock.VerifyNoOtherCalls();

      _userScopeRepositoryMock.Verify(repository => repository.AddUserScopesAsync(controlUserEntity, _cancellationToken));
      _userScopeRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_Null_For_Unknown_User_Id()
    {
      _userRepositoryMock.Setup(repository => repository.GetUserAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(default(UserEntity))
                         .Verifiable();

      var identity = Guid.NewGuid().ToUserIdentity();

      var actualUserEntity = await _userService.GetUserAsync(identity, _cancellationToken);

      Assert.IsNull(actualUserEntity);

      _userRepositoryMock.Verify(repository => repository.GetUserAsync(identity, _cancellationToken));
      _userRepositoryMock.VerifyNoOtherCalls();

      _userScopeRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_User_Whith_Scopes_For_Correct_User_Id()
    {
      var controlUserEntity = new UserEntity();

      _userRepositoryMock.Setup(repository => repository.GetUserAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(controlUserEntity)
                         .Verifiable();

      var controlUserScopeEntityCollection = new List<UserScopeEntity>();

      _userScopeRepositoryMock.Setup(repository => repository.GetUserScopesAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(controlUserScopeEntityCollection)
                              .Verifiable();

      var identity = Guid.NewGuid().ToUserIdentity();

      var actualUserEntity = await _userService.GetUserAsync(identity, _cancellationToken);

      Assert.IsNotNull(actualUserEntity);
      Assert.AreEqual(controlUserEntity, actualUserEntity);
      Assert.AreEqual(controlUserScopeEntityCollection, actualUserEntity.Scopes);

      _userRepositoryMock.Verify(repository => repository.GetUserAsync(identity, _cancellationToken));
      _userRepositoryMock.VerifyNoOtherCalls();

      _userScopeRepositoryMock.Verify(repository => repository.GetUserScopesAsync(identity, _cancellationToken));
      _userScopeRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_Null_For_Unknown_User_Email()
    {
      _userRepositoryMock.Setup(repository => repository.GetUserAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(default(UserEntity))
                         .Verifiable();

      var userEmail = Guid.NewGuid().ToString();

      var actualUserEntity = await _userService.GetUserAsync(userEmail, _cancellationToken);

      Assert.IsNull(actualUserEntity);

      _userRepositoryMock.Verify(repository => repository.GetUserAsync(userEmail, _cancellationToken));
      _userRepositoryMock.VerifyNoOtherCalls();

      _userScopeRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_User_Whith_Scopes_For_Correct_User_Email()
    {
      var controlUserEntity = new UserEntity
      {
        UserId = Guid.NewGuid(),
      };

      _userRepositoryMock.Setup(repository => repository.GetUserAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(controlUserEntity)
                         .Verifiable();

      var controlUserScopeEntityCollection = new List<UserScopeEntity>();

      _userScopeRepositoryMock.Setup(repository => repository.GetUserScopesAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(controlUserScopeEntityCollection)
                              .Verifiable();

      var userEmail = Guid.NewGuid().ToString();

      var actualUserEntity = await _userService.GetUserAsync(userEmail, _cancellationToken);

      Assert.IsNotNull(actualUserEntity);
      Assert.AreEqual(controlUserEntity, actualUserEntity);
      Assert.AreEqual(controlUserScopeEntityCollection, actualUserEntity.Scopes);

      _userRepositoryMock.Verify(repository => repository.GetUserAsync(userEmail, _cancellationToken));
      _userRepositoryMock.VerifyNoOtherCalls();

      _userScopeRepositoryMock.Verify(repository => repository.GetUserScopesAsync(controlUserEntity, _cancellationToken));
      _userScopeRepositoryMock.VerifyNoOtherCalls();
    }
  }
}
