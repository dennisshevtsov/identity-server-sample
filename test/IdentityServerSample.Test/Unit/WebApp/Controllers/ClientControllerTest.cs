// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApp.Controllers.Test
{
  using Microsoft.AspNetCore.Mvc;

  [TestClass]
  public sealed class ClientControllerTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IClientService> _clientServiceMock;

    private ClientController _clientController;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _clientServiceMock = new Mock<IClientService>();

      _clientController = new ClientController(_clientServiceMock.Object);
    }

    [TestMethod]
    public async Task GetClients_Should_Collection_Of_Audiences()
    {
      var getClientsResponseDto = new GetClientsResponseDto();

      _clientServiceMock.Setup(service => service.GetClientsAsync(It.IsAny<GetClientsRequestDto>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(getClientsResponseDto)
                        .Verifiable();

      var getClientsRequestDto = new GetClientsRequestDto();
      var actionResult = await _clientController.GetClients(getClientsRequestDto, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var okObjectResult = actionResult as OkObjectResult;

      Assert.IsNotNull(okObjectResult);
      Assert.AreEqual(getClientsResponseDto, okObjectResult!.Value);

      _clientServiceMock.Verify(service => service.GetClientsAsync(getClientsRequestDto, _cancellationToken), Times.Once());
      _clientServiceMock.VerifyNoOtherCalls();
    }
  }
}
