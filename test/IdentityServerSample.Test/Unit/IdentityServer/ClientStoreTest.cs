// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.IdenittyServer.Test
{
  using System.Threading;

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
      var testClientEntityd = await _clientStore.FindClientByIdAsync(clientId);

      Assert.IsNull(testClientEntityd);

      _clientRepositoryMock.Verify(repository => repository.GetClientAsync(clientId, CancellationToken.None));
      _clientRepositoryMock.VerifyNoOtherCalls();

      _mapperMock.VerifyNoOtherCalls();
    }
  }
}
