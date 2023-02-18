// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.IdenittyServer.Test
{
  [TestClass]
  public sealed class ResourceStoreTest
  {
#pragma warning disable CS8618
    private Mock<IMapper> _mapperMock;

    private Mock<IAudienceRepository> _audienceRepositoryMock;
    private Mock<IScopeRepository> _scopeRepositoryMock;

    private ResourceStore _resourceStore;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _mapperMock = new Mock<IMapper>();

      _audienceRepositoryMock = new Mock<IAudienceRepository>();
      _scopeRepositoryMock = new Mock<IScopeRepository>();

      _resourceStore = new ResourceStore(
        _mapperMock.Object,
        _scopeRepositoryMock.Object,
        _audienceRepositoryMock.Object);
    }
  }
}
