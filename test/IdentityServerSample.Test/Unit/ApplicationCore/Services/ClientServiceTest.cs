// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services.Test
{
  [TestClass]
  public sealed class ClientServiceTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IMapper> _mapperMock;
    private Mock<IClientRepository> _clientRepositoryMock;

    private ClientService _clientService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _mapperMock = new Mock<IMapper>();
      _clientRepositoryMock = new Mock<IClientRepository>();

      _clientService = new ClientService(
        _mapperMock.Object, _clientRepositoryMock.Object);
    }

    [TestMethod]
    public async Task AddClientAsync_Should_Return_Add_Client_From_Dto()
    {
      var controlClientEntity = new ClientEntity();

      _mapperMock.Setup(mapper => mapper.Map<ClientEntity>(It.IsAny<AddClientRequestDto>()))
                 .Returns(controlClientEntity)
                 .Verifiable();

      _clientRepositoryMock.Setup(repository => repository.AddClientAsync(It.IsAny<ClientEntity>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask)
                           .Verifiable();

      var addClientRequestDto = new AddClientRequestDto();

      await _clientService.AddClientAsync(addClientRequestDto, _cancellationToken);

      _clientRepositoryMock.Verify(repository => repository.AddClientAsync(controlClientEntity, _cancellationToken));
      _clientRepositoryMock.VerifyNoOtherCalls();

      _mapperMock.Verify(mapper => mapper.Map<ClientEntity>(addClientRequestDto));
      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddClientAsync_Should_Return_Add_Client()
    {
      _clientRepositoryMock.Setup(repository => repository.AddClientAsync(It.IsAny<ClientEntity>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask)
                           .Verifiable();

      var clientEntity = new ClientEntity();

      await _clientService.AddClientAsync(clientEntity, _cancellationToken);

      _clientRepositoryMock.Verify(repository => repository.AddClientAsync(clientEntity, _cancellationToken));
      _clientRepositoryMock.VerifyNoOtherCalls();

      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetClientAsync_Should_Return_Client_For_Dto()
    {
      var controlClientEntity = new ClientEntity();

      _clientRepositoryMock.Setup(repository => repository.GetClientAsync(It.IsAny<GetClientRequestDto>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(controlClientEntity)
                           .Verifiable();

      var getClientRequestDto = new GetClientRequestDto
      {
        ClientName = Guid.NewGuid().ToString(),
      };

      var actualClientEntity =
        await _clientService.GetClientAsync(getClientRequestDto, _cancellationToken);

      Assert.IsNotNull(controlClientEntity);
      Assert.AreEqual(controlClientEntity, actualClientEntity);

      _clientRepositoryMock.Verify(repository => repository.GetClientAsync(getClientRequestDto, _cancellationToken));
      _clientRepositoryMock.VerifyNoOtherCalls();

      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetClientAsync_Should_Return_Client_For_Client_Name()
    {
      var controlClientEntity = new ClientEntity();

      _clientRepositoryMock.Setup(repository => repository.GetClientAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(controlClientEntity)
                           .Verifiable();

      var clientName = Guid.NewGuid().ToString();

      var actualClientEntity =
        await _clientService.GetClientAsync(clientName, _cancellationToken);

      Assert.IsNotNull(controlClientEntity);
      Assert.AreEqual(controlClientEntity, actualClientEntity);

      _clientRepositoryMock.Verify(repository => repository.GetClientAsync(clientName, _cancellationToken));
      _clientRepositoryMock.VerifyNoOtherCalls();

      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetClientsAsync_Should_Return_Mapped_Dtos()
    {
      var clientEntityCollection = new ClientEntity[0];

      _clientRepositoryMock.Setup(repository => repository.GetClientsAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(clientEntityCollection)
                           .Verifiable();

      var getClientsResponseDto = new GetClientsResponseDto();

      _mapperMock.Setup(mapper => mapper.Map<GetClientsResponseDto>(It.IsAny<ClientEntity[]>()))
                 .Returns(getClientsResponseDto);

      var getClientsRequestDto = new GetClientsRequestDto();

      var actualGetClientsResponseDto = await _clientService.GetClientsAsync(
        getClientsRequestDto, _cancellationToken);

      Assert.IsNotNull(actualGetClientsResponseDto);
      Assert.AreEqual(getClientsResponseDto, actualGetClientsResponseDto);

      _clientRepositoryMock.Verify(repository => repository.GetClientsAsync(_cancellationToken));
      _clientRepositoryMock.VerifyNoOtherCalls();

      _mapperMock.Verify(mapper => mapper.Map<GetClientsResponseDto>(clientEntityCollection));
      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task CheckIfOriginIsAllowedAsync_Should_Return_True()
    {
      _clientRepositoryMock.Setup(repository => repository.GetFirstClientWithOriginAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(new ClientEntity())
                           .Verifiable();

      var origin = Guid.NewGuid().ToString();

      var flag = await _clientService.CheckIfOriginIsAllowedAsync(
        origin, _cancellationToken);

      Assert.IsTrue(flag);

      _clientRepositoryMock.Verify(repository => repository.GetFirstClientWithOriginAsync(origin, _cancellationToken));
      _clientRepositoryMock.VerifyNoOtherCalls();

      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task CheckIfOriginIsAllowedAsync_Should_Return_False()
    {
      _clientRepositoryMock.Setup(repository => repository.GetFirstClientWithOriginAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(default(ClientEntity))
                           .Verifiable();

      var origin = Guid.NewGuid().ToString();

      var flag = await _clientService.CheckIfOriginIsAllowedAsync(
        origin, _cancellationToken);

      Assert.IsFalse(flag);

      _clientRepositoryMock.Verify(repository => repository.GetFirstClientWithOriginAsync(origin, _cancellationToken));
      _clientRepositoryMock.VerifyNoOtherCalls();

      _mapperMock.VerifyNoOtherCalls();
    }
  }
}
