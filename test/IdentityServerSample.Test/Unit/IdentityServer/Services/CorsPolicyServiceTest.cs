// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityServer.Services.Test
{
  using System.Threading;

  [TestClass]
  public sealed class CorsPolicyServiceTest
  {
#pragma warning disable CS8618
    private Mock<IClientService> _clientServiceMock;

    private CorsPolicyService _corsPolicyService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _clientServiceMock = new Mock<IClientService>();

      _corsPolicyService = new CorsPolicyService(_clientServiceMock.Object);
    }

    [TestMethod]
    public async Task IsOriginAllowedAsync_Should_Return_True()
    {
      _clientServiceMock.Setup(service => service.CheckIfOriginIsAllowedAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true)
                        .Verifiable();

      var origin = Guid.NewGuid().ToString();
      var isAllowed = await _corsPolicyService.IsOriginAllowedAsync(origin);

      Assert.IsTrue(isAllowed);

      _clientServiceMock.Verify(service => service.CheckIfOriginIsAllowedAsync(origin, CancellationToken.None));
      _clientServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task IsOriginAllowedAsync_Should_Return_False()
    {
      _clientServiceMock.Setup(service => service.CheckIfOriginIsAllowedAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false)
                        .Verifiable();

      var origin = Guid.NewGuid().ToString();
      var isAllowed = await _corsPolicyService.IsOriginAllowedAsync(origin);

      Assert.IsFalse(isAllowed);

      _clientServiceMock.Verify(service => service.CheckIfOriginIsAllowedAsync(origin, CancellationToken.None));
      _clientServiceMock.VerifyNoOtherCalls();
    }
  }
}
