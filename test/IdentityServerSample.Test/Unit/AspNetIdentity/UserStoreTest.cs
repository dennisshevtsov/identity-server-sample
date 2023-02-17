// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using IdentityServerSample.ApplicationCore.Extensions;
using IdentityServerSample.ApplicationCore.Identities;

namespace IdentityServerSample.IdentityApi.AspNetIdentity.Test
{
  [TestClass]
  public sealed class UserStoreTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IUserRepository> _userRepositoryMock;

    private UserStore _userStore;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _userRepositoryMock = new Mock<IUserRepository>();

      _userStore = new UserStore(_userRepositoryMock.Object);
    }

    [TestMethod]
    public async Task GetUserIdAsync_Should_Return_Id()
    {
      var controlUserId = Guid.NewGuid();
      var userEntity = new UserEntity
      {
        UserId = controlUserId,
      };

      var testUserId = await _userStore.GetUserIdAsync(userEntity, _cancellationToken);

      Assert.IsNotNull(testUserId);
      Assert.AreEqual(controlUserId.ToString(), testUserId);

      _userRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetUserNameAsync_Should_Return_Email()
    {
      var controlEmail = Guid.NewGuid().ToString();
      var userEntity = new UserEntity
      {
        Email = controlEmail,
      };

      var testUserName = await _userStore.GetUserNameAsync(userEntity, _cancellationToken);

      Assert.IsNotNull(testUserName);
      Assert.AreEqual(controlEmail, testUserName);

      _userRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task SetNormalizedUserNameAsync_Should_Update_Email()
    {
      var userName = Guid.NewGuid().ToString();

      var userEntity = new UserEntity
      {
        Email = Guid.NewGuid().ToString(),
      };

      await _userStore.SetNormalizedUserNameAsync(userEntity, userName, _cancellationToken);

      Assert.AreEqual(userName, userEntity.Email);

      _userRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task FindByIdAsync_Should_Return_Null()
    {
      var userId = "test";

      var userEntity = await _userStore.FindByIdAsync(userId, _cancellationToken);

      Assert.IsNull(userEntity);

      _userRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task FindByIdAsync_Should_Return_User()
    {
      var controlUserId = Guid.NewGuid();
      var controlUserEntity = new UserEntity();

      _userRepositoryMock.Setup(repository => repository.GetUserAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(controlUserEntity)
                         .Verifiable();

      var userId = controlUserId.ToString();

      var testUserEntity = await _userStore.FindByIdAsync(userId, _cancellationToken);

      Assert.IsNotNull(testUserEntity);
      Assert.AreEqual(controlUserEntity, testUserEntity);

      _userRepositoryMock.Verify(repository => repository.GetUserAsync(controlUserId.ToUserIdentity(), _cancellationToken));
      _userRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task FindByNameAsyncc_Should_Return_User()
    {
      var controlUserEntity = new UserEntity();

      _userRepositoryMock.Setup(repository => repository.GetUserAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(controlUserEntity)
                         .Verifiable();

      var userName = Guid.NewGuid().ToString();

      var testUserEntity = await _userStore.FindByNameAsync(userName, _cancellationToken);

      Assert.IsNotNull(testUserEntity);
      Assert.AreEqual(controlUserEntity, testUserEntity);

      _userRepositoryMock.Verify(repository => repository.GetUserAsync(userName, _cancellationToken));
      _userRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task SetPasswordHashAsync_Should_Update_Password_Hash()
    {
      var passwordHash = Guid.NewGuid().ToString();

      var userEntity = new UserEntity
      {
        PasswordHash = Guid.NewGuid().ToString(),
      };

      await _userStore.SetPasswordHashAsync(userEntity, passwordHash, _cancellationToken);

      Assert.AreEqual(passwordHash, userEntity.PasswordHash);

      _userRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetPasswordHashAsync_Should_Return_Password_Hash()
    {
      var controlPasswordHash = Guid.NewGuid().ToString();
      var userEntity = new UserEntity
      {
        PasswordHash = controlPasswordHash,
      };

      var testPasswordHash = await _userStore.GetPasswordHashAsync(userEntity, _cancellationToken);

      Assert.IsNotNull(testPasswordHash);
      Assert.AreEqual(controlPasswordHash, testPasswordHash);

      _userRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetRolesAsync_Should_Return_Empty_List()
    {
      var userEntity = new UserEntity();

      var testRoleList = await _userStore.GetRolesAsync(userEntity, _cancellationToken);

      Assert.IsNotNull(testRoleList);
      Assert.AreEqual(0, testRoleList.Count);

      _userRepositoryMock.VerifyNoOtherCalls();
    }
  }
}
