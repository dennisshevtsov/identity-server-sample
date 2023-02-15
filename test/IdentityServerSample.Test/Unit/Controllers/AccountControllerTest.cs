// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers.Test
{
  using IdentityServer4.Services;
  using Microsoft.AspNetCore.Identity;

  using IdentityServerSample.IdentityApp.Dtos;
  using IdentityServer4.Models;

  [TestClass]
  public sealed class AccountControllerTest : IdentityControllerTestBase
  {
#pragma warning disable CS8618
    private Mock<IIdentityServerInteractionService> _identityServerInteractionServiceMock;

    private AccountController _accountController;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _identityServerInteractionServiceMock = new Mock<IIdentityServerInteractionService>();

      _accountController = new AccountController(
        SignInManagerMock.Object, _identityServerInteractionServiceMock.Object);
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
      SignInManagerMock.Setup(manager => manager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
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
      SignInManagerMock.Setup(manager => manager.SignOutAsync())
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
