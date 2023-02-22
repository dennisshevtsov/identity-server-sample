// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.AspNetIdentity.Test
{
  using IdentityModel;

  using IdentityServerSample.ApplicationCore.Identities;

  [TestClass]
  public sealed class UserStoreTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IUserService> _userServiceMock;

    private UserStore _userStore;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _userServiceMock = new Mock<IUserService>();

      _userStore = new UserStore(_userServiceMock.Object);
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

      _userServiceMock.VerifyNoOtherCalls();
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

      _userServiceMock.VerifyNoOtherCalls();
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

      _userServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task FindByIdAsync_Should_Return_Null()
    {
      var userId = "test";

      var userEntity = await _userStore.FindByIdAsync(userId, _cancellationToken);

      Assert.IsNull(userEntity);

      _userServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task FindByIdAsync_Should_Return_User()
    {
      var controlUserId = Guid.NewGuid();
      var controlUserEntity = new UserEntity();

      _userServiceMock.Setup(repository => repository.GetUserAsync(It.IsAny<IUserIdentity>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(controlUserEntity)
                         .Verifiable();

      var userId = controlUserId.ToString();

      var testUserEntity = await _userStore.FindByIdAsync(userId, _cancellationToken);

      Assert.IsNotNull(testUserEntity);
      Assert.AreEqual(controlUserEntity, testUserEntity);

      _userServiceMock.Verify(repository => repository.GetUserAsync(controlUserId.ToUserIdentity(), _cancellationToken));
      _userServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task FindByNameAsyncc_Should_Return_User()
    {
      var controlUserEntity = new UserEntity();

      _userServiceMock.Setup(repository => repository.GetUserAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(controlUserEntity)
                         .Verifiable();

      var userName = Guid.NewGuid().ToString();

      var testUserEntity = await _userStore.FindByNameAsync(userName, _cancellationToken);

      Assert.IsNotNull(testUserEntity);
      Assert.AreEqual(controlUserEntity, testUserEntity);

      _userServiceMock.Verify(repository => repository.GetUserAsync(userName, _cancellationToken));
      _userServiceMock.VerifyNoOtherCalls();
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

      _userServiceMock.VerifyNoOtherCalls();
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

      _userServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetRolesAsync_Should_Return_Empty_List()
    {
      var userEntity = new UserEntity();

      var testUserRoleCollection =
        await _userStore.GetRolesAsync(userEntity, _cancellationToken);

      Assert.IsNotNull(testUserRoleCollection);
      Assert.AreEqual(0, testUserRoleCollection.Count);

      _userServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetEmailAsync_Should_Return_Email()
    {
      var controlEmail = Guid.NewGuid().ToString();
      var userEntity = new UserEntity
      {
        Email = controlEmail,
      };

      var testUserName = await _userStore.GetEmailAsync(userEntity, _cancellationToken);

      Assert.IsNotNull(testUserName);
      Assert.AreEqual(controlEmail, testUserName);

      _userServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetClaimsAsync_Should_Return_Claim_List()
    {
      var controlUserName = Guid.NewGuid().ToString();
      var controlUserScopeName = Guid.NewGuid().ToString();

      var userEntity = new UserEntity
      {
        Name = controlUserName,
        Scopes = new List<UserScopeEntity>
        {
          new UserScopeEntity
          {
            Name = controlUserScopeName,
          },
        },
      };

      var claimCollection = await _userStore.GetClaimsAsync(userEntity, _cancellationToken);

      Assert.IsNotNull(claimCollection);

      Assert.AreEqual(3, claimCollection.Count);

      Assert.AreEqual(JwtClaimTypes.PreferredUserName, claimCollection[0].Type);
      Assert.AreEqual(controlUserName, claimCollection[0].Value);

      Assert.AreEqual(JwtClaimTypes.EmailVerified, claimCollection[1].Type);
      Assert.AreEqual("true", claimCollection[1].Value);

      Assert.AreEqual(JwtClaimTypes.Scope, claimCollection[2].Type);
      Assert.AreEqual(controlUserScopeName, claimCollection[2].Value);

      _userServiceMock.VerifyNoOtherCalls();
    }
  }
}
