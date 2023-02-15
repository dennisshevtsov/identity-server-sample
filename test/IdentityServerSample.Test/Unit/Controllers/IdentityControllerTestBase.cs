﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers.Test
{
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  public abstract class IdentityControllerTestBase
  {
#pragma warning disable CS8618
    private Mock<UserManager<UserEntity>> _userManagerMock;

    private Mock<SignInManager<UserEntity>> _signInManagerMock;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      var signInManagerLoggerMock = new Mock<ILogger<SignInManager<UserEntity>>>();

      var userStoreMock = new Mock<IUserStore<UserEntity>>();
      var passwordHasherMock = new Mock<IPasswordHasher<UserEntity>>();
      var userValidatorMock = new Mock<IUserValidator<UserEntity>>();
      var passwordValidatorMock = new Mock<IPasswordValidator<UserEntity>>();
      var keyNormalizerMock = new Mock<ILookupNormalizer>();
      var errorsMock = new Mock<IdentityErrorDescriber>();
      var servicesMock = new Mock<IServiceProvider>();
      var contextAccessorMock = new Mock<IHttpContextAccessor>();
      var claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<UserEntity>>();
      var optionsAccessorMock = new Mock<IOptions<IdentityOptions>>();
      var userManagerLoggerMock = new Mock<ILogger<UserManager<UserEntity>>>();
      var schemesMock = new Mock<IAuthenticationSchemeProvider>();
      var confirmationMock = new Mock<IUserConfirmation<UserEntity>>();

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

      _signInManagerMock = new Mock<SignInManager<UserEntity>>(
        _userManagerMock.Object,
        contextAccessorMock.Object,
        claimsFactoryMock.Object,
        optionsAccessorMock.Object,
        signInManagerLoggerMock.Object,
        schemesMock.Object,
        confirmationMock.Object);

      InitializeInternal();

      _userManagerMock.Reset();
      _signInManagerMock.Reset();
    }

    protected Mock<UserManager<UserEntity>> UserManagerMock { get => _userManagerMock; }

    protected Mock<SignInManager<UserEntity>> SignInManagerMock { get => _signInManagerMock; }

    protected abstract void InitializeInternal();
  }
}
