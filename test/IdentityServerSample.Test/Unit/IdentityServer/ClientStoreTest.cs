// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.AspNetIdentity.Test
{
  using IdentityServerSample.IdentityApi.IdenittyServer;

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
  }
}
