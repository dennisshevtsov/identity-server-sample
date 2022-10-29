// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Api.Test.Unit
{
  using System.Security.Claims;
  using System.Security.Principal;

  using Moq;

  using IdentityServerSample.Api.Controllers;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;

  [TestClass]
  public sealed class IdentityControllerTest
  {
#pragma warning disable CS8618
    private Mock<IIdentity> _identityMock;
    private Mock<ClaimsPrincipal> _userMock;
    private Mock<HttpContext> _httpContextMock;
    private IdentityController _controller;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _identityMock = new Mock<IIdentity>();

      _userMock = new Mock<ClaimsPrincipal>();
      _userMock.SetupGet(user => user.Identity)
               .Returns(_identityMock.Object)
               .Verifiable();

      _httpContextMock = new Mock<HttpContext>();
      _httpContextMock.SetupGet(context => context.User)
                      .Returns(_userMock.Object)
                      .Verifiable();

      var controllerContext = new ControllerContext
      {
        HttpContext = _httpContextMock.Object,
      };

      _controller = new IdentityController();
      _controller.ControllerContext = controllerContext;
    }

    [TestMethod]
    public void GetTest()
    {
      var userName = Guid.NewGuid().ToString();
      _identityMock.SetupGet(identity => identity.Name)
             .Returns(userName)
             .Verifiable();

      var claims = new[]
      {
        Guid.NewGuid().ToString(),
        Guid.NewGuid().ToString(),
      };

      _userMock.SetupGet(user => user.Claims)
               .Returns(new[]
               {
                 new Claim("test", claims[0]),
                 new Claim("test", claims[1]),
               })
               .Verifiable();

      var actionResult = _controller.Get();

      Assert.IsNotNull(actionResult);
    }
  }
}
