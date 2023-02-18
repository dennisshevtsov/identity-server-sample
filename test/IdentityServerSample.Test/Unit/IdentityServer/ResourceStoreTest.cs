﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.IdenittyServer.Test
{
  using IdentityServer4;

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

    [TestMethod]
    public async Task FindIdentityResourcesByScopeNameAsync_Should_Return_Resources()
    {
      var scopeNames = new[]
      {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
      };

      var identityResourceCollection =
        await _resourceStore.FindIdentityResourcesByScopeNameAsync(scopeNames);

      Assert.IsNotNull(identityResourceCollection);

      var identityResourceArray = identityResourceCollection.ToArray();

      Assert.AreEqual(scopeNames.Length, identityResourceArray.Length);

      for (int i = 0; i < identityResourceArray.Length; i++)
      {
        Assert.IsTrue(scopeNames.Contains(identityResourceArray[i].Name));
      }

      _mapperMock.VerifyNoOtherCalls();

      _audienceRepositoryMock.VerifyNoOtherCalls();
      _scopeRepositoryMock.VerifyNoOtherCalls();
    }
  }
}
