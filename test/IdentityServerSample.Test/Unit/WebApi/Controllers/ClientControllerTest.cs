// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Controllers.Test
{
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.ApplicationCore.Identities;

  [TestClass]
  public sealed class ClientControllerTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IClientService> _clientServiceMock;
    private Mock<IMapper> _mapperMock;

    private ClientController _clientController;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _clientServiceMock = new Mock<IClientService>();
      _mapperMock = new Mock<IMapper>();

      _clientController = new ClientController(_clientServiceMock.Object, _mapperMock.Object);
    }

    [TestMethod]
    public async Task GetClients_Should_Return_Collection_Of_Clients()
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

      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetClient_Should_Return_Client()
    {
      var controlClientEntity = new ClientEntity();

      _clientServiceMock.Setup(service => service.GetClientAsync(It.IsAny<IClientIdentity>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(controlClientEntity)
                        .Verifiable();

      var getClientResponseDto = new GetClientResponseDto();

      _mapperMock.Setup(mapper => mapper.Map<GetClientResponseDto>(It.IsAny<ClientEntity>()))
                 .Returns(getClientResponseDto);

      var getClientRequestDto = new GetClientRequestDto();
      var actionResult = await _clientController.GetClient(getClientRequestDto, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var okObjectResult = actionResult as OkObjectResult;

      Assert.IsNotNull(okObjectResult);
      Assert.AreEqual(getClientResponseDto, okObjectResult!.Value);

      _clientServiceMock.Verify(service => service.GetClientAsync(getClientRequestDto, _cancellationToken), Times.Once());
      _clientServiceMock.VerifyNoOtherCalls();

      _mapperMock.Verify(mapper => mapper.Map<GetClientResponseDto>(controlClientEntity), Times.Once());
      _mapperMock.VerifyNoOtherCalls();
    }
  }
}
