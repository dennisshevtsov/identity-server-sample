// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services.Test
{
  [TestClass]
  public sealed class ScopeServiceTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IScopeRepository> _scopeRepositoryMock;

    private ScopeService _scopeService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _scopeRepositoryMock = new Mock<IScopeRepository>();

      _scopeService = new ScopeService(_scopeRepositoryMock.Object);
    }

    [TestMethod]
    public async Task GetScopesAsync_Should_Collection_Thank_Contains_Standard_Scopes()
    {
      var scopeEntityCollection = new List<ScopeEntity>();

      _scopeRepositoryMock.Setup(repository => repository.GetScopesAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(scopeEntityCollection)
                          .Verifiable();

      var actualScopeEntityCollection = await _scopeService.GetScopesAsync(_cancellationToken);

      Assert.IsNotNull(actualScopeEntityCollection);
      Assert.AreEqual(5, actualScopeEntityCollection.Count);

      Assert.IsTrue(actualScopeEntityCollection.Any(entity => entity.Name == "openid"));
      Assert.IsTrue(actualScopeEntityCollection.Any(entity => entity.Name == "profile"));
      Assert.IsTrue(actualScopeEntityCollection.Any(entity => entity.Name == "email"));
      Assert.IsTrue(actualScopeEntityCollection.Any(entity => entity.Name == "address"));
      Assert.IsTrue(actualScopeEntityCollection.Any(entity => entity.Name == "phone"));

      _scopeRepositoryMock.Verify(repository => repository.GetScopesAsync(_cancellationToken));
      _scopeRepositoryMock.VerifyNoOtherCalls();
    }
  }
}
