// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers.Test
{
  using IdentityServer4.Services;

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
  }
}
