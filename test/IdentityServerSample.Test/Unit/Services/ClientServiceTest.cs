// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Test.Unit.Services
{
  using AutoMapper;
  using Moq;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Repositories;
  using IdentityServerSample.ApplicationCore.Services;

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
    public async Task GetAudiencesAsync_Should_Return_Mapped_Dtos()
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
  }
}
