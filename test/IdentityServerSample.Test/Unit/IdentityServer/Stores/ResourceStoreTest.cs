// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityServer.Stores.Test
{
  using System.Linq;

  using IdentityServer4.Models;
  using Moq;

  [TestClass]
  public sealed class ResourceStoreTest
  {
#pragma warning disable CS8618
    private Mock<IMapper> _mapperMock;

    private Mock<IScopeService> _scopeServiceMock;
    private Mock<IAudienceService> _audienceServiceMock;

    private ResourceStore _resourceStore;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _mapperMock = new Mock<IMapper>();

      _scopeServiceMock = new Mock<IScopeService>();
      _audienceServiceMock = new Mock<IAudienceService>();

      _resourceStore = new ResourceStore(
        _mapperMock.Object,
        _audienceServiceMock.Object,
        _scopeServiceMock.Object);
    }

    [TestMethod]
    public async Task FindIdentityResourcesByScopeNameAsync_Should_Return_Resources()
    {
      var controlScopeEntityCollection = new List<ScopeEntity>();

      _scopeServiceMock.Setup(service => service.GetStandardScopesAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(controlScopeEntityCollection)
                       .Verifiable();

      var controlIndentityResourceCollection = new List<IdentityResource>();

      _mapperMock.Setup(mapper => mapper.Map<IEnumerable<IdentityResource>>(It.IsAny<List<ScopeEntity>>()))
                 .Returns(controlIndentityResourceCollection)
                 .Verifiable();

      var scopeNames = new[]
      {
        Guid.NewGuid().ToString(),
      };

      var actualIdentityResourceCollection =
        await _resourceStore.FindIdentityResourcesByScopeNameAsync(scopeNames);

      Assert.IsNotNull(actualIdentityResourceCollection);
      Assert.AreEqual(controlIndentityResourceCollection, actualIdentityResourceCollection);

      _mapperMock.Verify(mapper => mapper.Map<IEnumerable<IdentityResource>>(controlScopeEntityCollection));
      _mapperMock.VerifyNoOtherCalls();

      _scopeServiceMock.Verify(service => service.GetStandardScopesAsync(CancellationToken.None));
      _scopeServiceMock.VerifyNoOtherCalls();

      _audienceServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task FindApiScopesByNameAsync_Should_Return_Scopes()
    {
      var controlScopeEntityCollection = new List<ScopeEntity>();

      _scopeServiceMock.Setup(repository => repository.GetScopesAsync(It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
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

      _scopeServiceMock.Verify(repository => repository.GetScopesAsync(scopeNames, CancellationToken.None));
      _scopeServiceMock.VerifyNoOtherCalls();

      _audienceServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task FindApiResourcesByScopeNameAsync_Should_Return_Resources()
    {
      var controlAudienceEntityCollection = new List<AudienceEntity>();

      _audienceServiceMock.Setup(repository => repository.GetAudiencesByScopesAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(controlAudienceEntityCollection)
                          .Verifiable();

      var controlResourceCollection = new List<ApiResource>();

      _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ApiResource>>(It.IsAny<List<AudienceEntity>>()))
                 .Returns(controlResourceCollection)
                 .Verifiable();

      var scopeNames = new string[0];

      var testResourceCollection =
        await _resourceStore.FindApiResourcesByScopeNameAsync(scopeNames);

      Assert.IsNotNull(testResourceCollection);
      Assert.AreEqual(controlResourceCollection, testResourceCollection);

      _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ApiResource>>(controlAudienceEntityCollection));
      _mapperMock.VerifyNoOtherCalls();

      _scopeServiceMock.VerifyNoOtherCalls();

      _audienceServiceMock.Verify(repository => repository.GetAudiencesByScopesAsync(scopeNames, CancellationToken.None));
      _audienceServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task FindApiResourcesByNameAsync_Should_Return_Resources()
    {
      var controlAudienceEntityCollection = new List<AudienceEntity>();

      _audienceServiceMock.Setup(repository => repository.GetAudiencesByNamesAsync(It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(controlAudienceEntityCollection)
                          .Verifiable();

      var controlResourceCollection = new ApiResource[0];

      _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ApiResource>>(It.IsAny<List<AudienceEntity>>()))
                 .Returns(controlResourceCollection)
                 .Verifiable();

      var scopeNames = new string[0];

      var testResourceCollection =
        await _resourceStore.FindApiResourcesByNameAsync(scopeNames);

      Assert.IsNotNull(testResourceCollection);
      Assert.AreEqual(controlResourceCollection, testResourceCollection);

      _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ApiResource>>(controlAudienceEntityCollection));
      _mapperMock.VerifyNoOtherCalls();

      _scopeServiceMock.VerifyNoOtherCalls();

      _audienceServiceMock.Verify(repository => repository.GetAudiencesByNamesAsync(scopeNames, CancellationToken.None));
      _audienceServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetAllResourcesAsync_Should_Return_Resources()
    {
      var controlStandardScopeEntityCollection = new List<ScopeEntity>();

      _scopeServiceMock.Setup(service => service.GetStandardScopesAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(controlStandardScopeEntityCollection)
                       .Verifiable();

      var controlIdentityResources = new List<IdentityResource>
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
      };

      _mapperMock.Setup(mapper => mapper.Map<IEnumerable<IdentityResource>>(It.IsAny<List<ScopeEntity>>()))
                 .Returns(controlIdentityResources)
                 .Verifiable();

      var controlScopeEntityCollection = new List<ScopeEntity>();

      _scopeServiceMock.Setup(repository => repository.GetScopesAsync(It.IsAny<CancellationToken>()))
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

      var controlAudienceEntityCollection = new List<AudienceEntity>();

      _audienceServiceMock.Setup(repository => repository.GetAudiencesAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(controlAudienceEntityCollection)
                          .Verifiable();

      var controlResourceCollection = new[]
      {
        new ApiResource { Name = Guid.NewGuid().ToString(), },
        new ApiResource { Name = Guid.NewGuid().ToString(), },
      };

      _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ApiResource>>(It.IsAny<List<AudienceEntity>>()))
                 .Returns(controlResourceCollection)
                 .Verifiable();

      var scopeNames = new string[0];

      var testResources = await _resourceStore.GetAllResourcesAsync();

      Assert.IsNotNull(testResources);

      AreEqual(controlScopeCollection, testResources.ApiScopes);
      AreEqual(controlResourceCollection, testResources.ApiResources);
      AreEqual(controlIdentityResources, testResources.IdentityResources);

      _mapperMock.Verify(mapper => mapper.Map<IEnumerable<IdentityResource>>(controlStandardScopeEntityCollection));
      _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ApiScope>>(controlAudienceEntityCollection));
      _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ApiResource>>(controlAudienceEntityCollection));
      _mapperMock.VerifyNoOtherCalls();

      _scopeServiceMock.Verify(repository => repository.GetStandardScopesAsync(CancellationToken.None));
      _scopeServiceMock.Verify(repository => repository.GetScopesAsync(CancellationToken.None));
      _scopeServiceMock.VerifyNoOtherCalls();

      _audienceServiceMock.Verify(repository => repository.GetAudiencesAsync(CancellationToken.None));
      _audienceServiceMock.VerifyNoOtherCalls();
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

    private void AreEqual(List<IdentityResource> controlIdentityResources, ICollection<IdentityResource> testIdentityResources)
    {
      Assert.IsNotNull(testIdentityResources);

      Assert.AreEqual(controlIdentityResources.Count, testIdentityResources.Count);

      var identityResourceSet =
        testIdentityResources.Select(resource => resource.Name)
                             .ToHashSet();

      for (int i = 0; i < controlIdentityResources.Count; i++)
      {
        Assert.IsTrue(identityResourceSet.Contains(controlIdentityResources[i].Name));
      }
    }
  }
}
