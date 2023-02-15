// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers.Test
{
  using IdentityServer4.Services;
  using IdentityServerSample.IdentityApp.Dtos;
  using Microsoft.AspNetCore.Mvc;

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
    public async Task SingInAccount_Should_Return_Bad_Request()
    {
      _accountController.ControllerContext.ModelState.AddModelError("test", "test");

      var requestDto = new SingInAccountRequestDto();

      var actionResult = await _accountController.SingInAccount(requestDto);

      Assert.IsNotNull(actionResult);

      var badRequestResult = actionResult as BadRequestResult;

      Assert.IsNotNull(badRequestResult);
    }
  }
}
