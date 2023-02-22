// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityServer.Stores.Test
{
  using System.Threading;

  using IdentityServer4.Models;

  [TestClass]
  public sealed class ClientStoreTest
  {
#pragma warning disable CS8618
    private Mock<IMapper> _mapperMock;
    private Mock<IClientRepository> _clientRepositoryMock;

    private ClientStore _clientStore;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _mapperMock = new Mock<IMapper>();
      _clientRepositoryMock = new Mock<IClientRepository>();

      _clientStore = new ClientStore(
        _mapperMock.Object, _clientRepositoryMock.Object);
    }

    [TestMethod]
    public async Task FindClientByIdAsync_Should_Return_Null()
    {
      _clientRepositoryMock.Setup(repository => repository.GetClientAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(default(ClientEntity))
                           .Verifiable();

      var clientId = Guid.NewGuid().ToString();
      var testClientEntity = await _clientStore.FindClientByIdAsync(clientId);

      Assert.IsNull(testClientEntity);

      _clientRepositoryMock.Verify(repository => repository.GetClientAsync(clientId, CancellationToken.None));
      _clientRepositoryMock.VerifyNoOtherCalls();

      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task FindClientByIdAsync_Should_Return_Client()
    {
      var controlClientEntity = new ClientEntity();

      _clientRepositoryMock.Setup(repository => repository.GetClientAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(controlClientEntity)
                           .Verifiable();

      var controlClient = new Client();

      _mapperMock.Setup(mapper => mapper.Map<Client>(It.IsAny<ClientEntity>()))
                 .Returns(controlClient)
                 .Verifiable();

      var clientId = Guid.NewGuid().ToString();
      var testClientEntity = await _clientStore.FindClientByIdAsync(clientId);

      Assert.IsNotNull(testClientEntity);
      Assert.AreEqual(controlClient, testClientEntity);

      _clientRepositoryMock.Verify(repository => repository.GetClientAsync(clientId, CancellationToken.None));
      _clientRepositoryMock.VerifyNoOtherCalls();

      _mapperMock.Verify(mapper => mapper.Map<Client>(controlClientEntity));
      _mapperMock.VerifyNoOtherCalls();
    }
  }
}
