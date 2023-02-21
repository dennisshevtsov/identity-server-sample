﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using IdentityServerSample.ApplicationCore.Identities;

namespace IdentityServerSample.ApplicationCore.Services.Test
{
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
    public async Task GetScopesAsync_Should_Return_Null()
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
  }
}
