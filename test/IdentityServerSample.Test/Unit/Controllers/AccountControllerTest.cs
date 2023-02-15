// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers.Test
{
  using IdentityServer4.Models;
  using IdentityServer4.Services;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using IdentityServerSample.IdentityApp.Dtos;

  [TestClass]
  public sealed class AccountControllerTest
  {
#pragma warning disable CS8618
    private Mock<UserManager<UserEntity>> _userManagerMock;

    private Mock<SignInManager<UserEntity>> _signInManagerMock;

    private Mock<IIdentityServerInteractionService> _identityServerInteractionServiceMock;

    private AccountController _accountController;
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

      _userManagerMock.Reset();
      _signInManagerMock.Reset();

      _identityServerInteractionServiceMock = new Mock<IIdentityServerInteractionService>();

      _accountController = new AccountController(
        _signInManagerMock.Object, _identityServerInteractionServiceMock.Object);
    }

    [TestMethod]
    public async Task SingInAccount_Should_Return_Bad_Request_If_Dto_Is_Not_Valid()
    {
      _accountController.ControllerContext.ModelState.AddModelError("test", "test");

      var requestDto = new SingInAccountRequestDto();

      var actionResult = await _accountController.SingInAccount(requestDto);

      Assert.IsNotNull(actionResult);

      var badRequestResult = actionResult as Microsoft.AspNetCore.Mvc.BadRequestResult;

      Assert.IsNotNull(badRequestResult);
    }

    [TestMethod]
    public async Task SingInAccount_Should_Return_Bad_Request_If_Credentials_Are_Not_Valid()
    {
      _signInManagerMock.Setup(manager => manager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                        .ReturnsAsync(SignInResult.Failed)
                        .Verifiable();

      var requestDto = new SingInAccountRequestDto();

      var actionResult = await _accountController.SingInAccount(requestDto);

      Assert.IsNotNull(actionResult);

      var badRequestResult = actionResult as Microsoft.AspNetCore.Mvc.BadRequestResult;

      Assert.IsNotNull(badRequestResult);
      Assert.IsTrue(_accountController.ControllerContext.ModelState.Any(
        error => error.Value!.Errors[0].ErrorMessage == AccountController.InvalidCredentialsErrorMessage));
    }

    [TestMethod]
    public async Task SingOutAccount_Should_Return_Redirect_Result()
    {
      _signInManagerMock.Setup(manager => manager.SignOutAsync())
                        .Returns(Task.CompletedTask)
                        .Verifiable();

      var logoutRequest = new LogoutRequest(null, null)
      {
        PostLogoutRedirectUri = Guid.NewGuid().ToString(),
      };

      _identityServerInteractionServiceMock.Setup(service => service.GetLogoutContextAsync(It.IsAny<string>()))
                                           .ReturnsAsync(logoutRequest)
                                           .Verifiable();

      var requestDto = new SignOutAccountRequestDto();

      var actionResult = await _accountController.SingOutAccount(requestDto);

      Assert.IsNotNull(actionResult);

      var redirectResult = actionResult as Microsoft.AspNetCore.Mvc.RedirectResult;

      Assert.IsNotNull(redirectResult);
      Assert.AreEqual(logoutRequest.PostLogoutRedirectUri, redirectResult.Url);
    }
  }
}
