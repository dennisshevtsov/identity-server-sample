// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityServer.Test
{
  using System.Linq;

  using IdentityServer4;
  using IdentityServer4.Models;
  using Moq;

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

    [TestMethod]
    public async Task FindApiScopesByNameAsync_Should_Return_Scopes()
    {
      var controlScopeEntityCollection = new List<ScopeEntity>();

      _scopeRepositoryMock.Setup(repository => repository.GetScopesAsync(It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(controlScopeEntityCollection)
                          .Verifiable();

      var controlScopeCollection = new List<ApiScope>();

      _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ApiScope>>(It.IsAny<List<ScopeEntity>>()))
                 .Returns(controlScopeCollection)
                 .Verifiable();

      var scopeNames = new string[0];

      var testScopeCollection =
        await _resourceStore.FindApiScopesByNameAsync(scopeNames);

      Assert.IsNotNull(testScopeCollection);
      Assert.AreEqual(controlScopeCollection, testScopeCollection);

      _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ApiScope>>(controlScopeEntityCollection));
      _mapperMock.VerifyNoOtherCalls();

      _scopeRepositoryMock.Verify(repository => repository.GetScopesAsync(scopeNames, CancellationToken.None));
      _scopeRepositoryMock.VerifyNoOtherCalls();

      _audienceRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task FindApiResourcesByScopeNameAsync_Should_Return_Resources()
    {
      var controlAudienceEntityCollection = new AudienceEntity[0];

      _audienceRepositoryMock.Setup(repository => repository.GetAudiencesByScopesAsync(It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(controlAudienceEntityCollection)
                             .Verifiable();

      var controlResourceCollection = new ApiResource[0];

      _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ApiResource>>(It.IsAny<AudienceEntity[]>()))
                 .Returns(controlResourceCollection)
                 .Verifiable();

      var scopeNames = new string[0];

      var testResourceCollection =
        await _resourceStore.FindApiResourcesByScopeNameAsync(scopeNames);

      Assert.IsNotNull(testResourceCollection);
      Assert.AreEqual(controlResourceCollection, testResourceCollection);

      _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ApiResource>>(controlAudienceEntityCollection));
      _mapperMock.VerifyNoOtherCalls();

      _audienceRepositoryMock.Verify(repository => repository.GetAudiencesByScopesAsync(scopeNames, CancellationToken.None));
      _audienceRepositoryMock.VerifyNoOtherCalls();

      _scopeRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task FindApiResourcesByNameAsync_Should_Return_Resources()
    {
      var controlAudienceEntityCollection = new AudienceEntity[0];

      _audienceRepositoryMock.Setup(repository => repository.GetAudiencesByNamesAsync(It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(controlAudienceEntityCollection)
                             .Verifiable();

      var controlResourceCollection = new ApiResource[0];

      _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ApiResource>>(It.IsAny<AudienceEntity[]>()))
                 .Returns(controlResourceCollection)
                 .Verifiable();

      var scopeNames = new string[0];

      var testResourceCollection =
        await _resourceStore.FindApiResourcesByNameAsync(scopeNames);

      Assert.IsNotNull(testResourceCollection);
      Assert.AreEqual(controlResourceCollection, testResourceCollection);

      _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ApiResource>>(controlAudienceEntityCollection));
      _mapperMock.VerifyNoOtherCalls();

      _audienceRepositoryMock.Verify(repository => repository.GetAudiencesByNamesAsync(scopeNames, CancellationToken.None));
      _audienceRepositoryMock.VerifyNoOtherCalls();

      _scopeRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetAllResourcesAsync_Should_Return_Resources()
    {
      var controlScopeEntityCollection = new List<ScopeEntity>();

      _scopeRepositoryMock.Setup(repository => repository.GetScopesAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(controlScopeEntityCollection)
                          .Verifiable();

      var controlScopeCollection = new List<ApiScope>
      {
        new ApiScope { Name = Guid.NewGuid().ToString(), },
        new ApiScope { Name = Guid.NewGuid().ToString(), },
      };

      _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ApiScope>>(It.IsAny<List<ScopeEntity>>()))
                 .Returns(controlScopeCollection)
                 .Verifiable();

      var controlAudienceEntityCollection = new AudienceEntity[0];

      _audienceRepositoryMock.Setup(repository => repository.GetAudiencesAsync(It.IsAny<CancellationToken>()))
                             .ReturnsAsync(controlAudienceEntityCollection)
                             .Verifiable();

      var controlResourceCollection = new[]
      {
        new ApiResource { Name = Guid.NewGuid().ToString(), },
        new ApiResource { Name = Guid.NewGuid().ToString(), },
      };

      _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ApiResource>>(It.IsAny<AudienceEntity[]>()))
                 .Returns(controlResourceCollection)
                 .Verifiable();

      var scopeNames = new string[0];

      var testResources = await _resourceStore.GetAllResourcesAsync();

      Assert.IsNotNull(testResources);

      AreEqual(controlScopeCollection, testResources.ApiScopes);
      AreEqual(controlResourceCollection, testResources.ApiResources);
      AreEqual(testResources.IdentityResources);

      _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ApiScope>>(controlAudienceEntityCollection));
      _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ApiResource>>(controlAudienceEntityCollection));
      _mapperMock.VerifyNoOtherCalls();

      _audienceRepositoryMock.Verify(repository => repository.GetAudiencesAsync(CancellationToken.None));
      _audienceRepositoryMock.VerifyNoOtherCalls();

      _scopeRepositoryMock.Verify(repository => repository.GetScopesAsync(CancellationToken.None));
      _scopeRepositoryMock.VerifyNoOtherCalls();
    }

    private static void AreEqual(
      List<ApiScope> controlScopeCollection, ICollection<ApiScope> testApiScopes)
    {
      Assert.IsNotNull(testApiScopes);

      var scopeSet = testApiScopes.Select(scope => scope.Name)
                                  .ToHashSet();

      Assert.AreEqual(controlScopeCollection.Count, scopeSet.Count);

      for (int i = 0; i < controlScopeCollection.Count; i++)
      {
        Assert.IsTrue(scopeSet.Contains(controlScopeCollection[i].Name));
      }
    }

    private void AreEqual(
      ApiResource[] controlResourceCollection, ICollection<ApiResource> testApiResources)
    {
      Assert.IsNotNull(testApiResources);

      var resourceSet = testApiResources.Select(scope => scope.Name)
                                        .ToHashSet();

      Assert.AreEqual(controlResourceCollection.Length, resourceSet.Count);

      for (int i = 0; i < controlResourceCollection.Length; i++)
      {
        Assert.IsTrue(resourceSet.Contains(controlResourceCollection[i].Name));
      }
    }

    private void AreEqual(ICollection<IdentityResource> testIdentityResources)
    {
      Assert.IsNotNull(testIdentityResources);

      var identityResourceSet =
        testIdentityResources.Select(resource => resource.Name)
                             .ToHashSet();

      Assert.AreEqual(5, identityResourceSet.Count);

      Assert.IsTrue(identityResourceSet.Contains(IdentityServerConstants.StandardScopes.OpenId));
      Assert.IsTrue(identityResourceSet.Contains(IdentityServerConstants.StandardScopes.Profile));
      Assert.IsTrue(identityResourceSet.Contains(IdentityServerConstants.StandardScopes.Email));
      Assert.IsTrue(identityResourceSet.Contains(IdentityServerConstants.StandardScopes.Phone));
      Assert.IsTrue(identityResourceSet.Contains(IdentityServerConstants.StandardScopes.Address));
    }
  }
}
