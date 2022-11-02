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
  public sealed class UserControllerTest
  {
#pragma warning disable CS8618
    private Mock<IIdentity> _identityMock;
    private Mock<ClaimsPrincipal> _userMock;
    private Mock<HttpContext> _httpContextMock;

    private UserController _controller;
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

      _controller = new UserController
      {
        ControllerContext = controllerContext,
      };
    }

    [TestMethod]
    public void Get_Should_Return_Current_User()
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

      var objectResult = actionResult as OkObjectResult;

      Assert.IsNotNull(objectResult);
      Assert.IsNotNull(objectResult.Value);

      var model = objectResult.Value as UserController.UserDto;

      Assert.IsNotNull(model);
      Assert.AreEqual(userName, model.Name);

      Assert.IsNotNull(model.Claims);
      Assert.AreEqual(2, model.Claims.Length);
      Assert.AreEqual(claims[0], model.Claims[0]);
      Assert.AreEqual(claims[1], model.Claims[1]);

      _identityMock.VerifyGet(identity => identity.Name);
      _identityMock.VerifyNoOtherCalls();

      _userMock.VerifyGet(user => user.Identity);
      _userMock.VerifyGet(user => user.Claims);
      _userMock.VerifyNoOtherCalls();

      _httpContextMock.VerifyGet(context => context.User);
      _httpContextMock.VerifyNoOtherCalls();
    }
  }
}
