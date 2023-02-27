﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.Initialization.Test
{
  using Microsoft.AspNetCore.Identity;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  [TestClass]
  public sealed class DatabaseInitializerTest
  {
#pragma warning disable CS8618
    private Mock<IConfiguration> _configurationMock;
    private Mock<UserManager<UserEntity>> _userManagerMock;
    private Mock<IScopeService> _scopeServiceMock;

    private DatabaseInitializer _databaseInitializer;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      var userStoreMock = new Mock<IUserStore<UserEntity>>();
      var passwordHasherMock = new Mock<IPasswordHasher<UserEntity>>();
      var userValidatorMock = new Mock<IUserValidator<UserEntity>>();
      var passwordValidatorMock = new Mock<IPasswordValidator<UserEntity>>();
      var keyNormalizerMock = new Mock<ILookupNormalizer>();
      var errorsMock = new Mock<IdentityErrorDescriber>();
      var servicesMock = new Mock<IServiceProvider>();
      var optionsAccessorMock = new Mock<IOptions<IdentityOptions>>();
      var userManagerLoggerMock = new Mock<ILogger<UserManager<UserEntity>>>();

      _userManagerMock = new Mock<UserManager<UserEntity>>(
        userStoreMock.Object,
        optionsAccessorMock.Object,
        passwordHasherMock.Object,
        new[] { userValidatorMock.Object }.AsEnumerable(),
        new[] { passwordValidatorMock.Object }.AsEnumerable(),
        keyNormalizerMock.Object,
        errorsMock.Object,
        servicesMock.Object,
        userManagerLoggerMock.Object);

      _databaseInitializer = new DatabaseInitializer(
        _configurationMock.Object,
        _userManagerMock.Object,
        _scopeServiceMock.Object);

      _userManagerMock.Reset();
    }
  }
}
