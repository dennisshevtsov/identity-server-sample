// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers.Test
{
  using IdentityServer4.Models;
  using IdentityServer4.Services;
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.IdentityApp.Dtos;

  [TestClass]
  public sealed class ErrorControllerTest
  {
#pragma warning disable CS8618
    private Mock<IIdentityServerInteractionService> _identityServerInteractionServiceMock;

    private ErrorController _errorController;
#pragma warning restore CS8618

    [TestInitialize]
    public void InitializeInternal()
    {
      _identityServerInteractionServiceMock = new Mock<IIdentityServerInteractionService>();

      _errorController = new ErrorController(_identityServerInteractionServiceMock.Object);
    }

    [TestMethod]
    public async Task GetError_Should_Return_Bad_Request()
    {
      _identityServerInteractionServiceMock.Setup(service => service.GetErrorContextAsync(It.IsAny<string>()))
                                           .ReturnsAsync(default(ErrorMessage))
                                           .Verifiable();

      var requestDto = new GetErrorRequestDto();

      var actionResult = await _errorController.GetError(requestDto);

      Assert.IsNotNull(actionResult);

      var badRequestResult = actionResult as BadRequestResult;

      Assert.IsNotNull(badRequestResult);
    }
  }
}
