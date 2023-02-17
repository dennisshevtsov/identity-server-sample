// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

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
    }

    [TestMethod]
    public async Task FindByIdAsync_Should_Return_Null()
    {
      var userId = "test";

      var userEntity = await _userStore.FindByIdAsync(userId, _cancellationToken);

      Assert.IsNull(userEntity);
    }
  }
}
