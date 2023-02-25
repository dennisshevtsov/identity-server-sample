// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services.Test
{
  using IdentityServerSample.ApplicationCore.Identities;

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
    public async Task GetScopesAsync_Should_Return_All_Custom_Scopes()
    {
      var controlScopeEntityCollection = new List<ScopeEntity>();

      _scopeRepositoryMock.Setup(repository => repository.GetScopesAsync(It.IsAny<IEnumerable<IScopeIdentity>>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(controlScopeEntityCollection)
                          .Verifiable();

      var actualScopeEntityCollection = await _scopeService.GetScopesAsync(_cancellationToken);

      Assert.IsNotNull(actualScopeEntityCollection);
      Assert.AreEqual(controlScopeEntityCollection, actualScopeEntityCollection);

      _scopeRepositoryMock.Verify(repository => repository.GetScopesAsync(null, false, _cancellationToken));
      _scopeRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetScopesAsync_Should_Return_Custom_Scopes_By_Names()
    {
      var controlScopeEntityCollection = new List<ScopeEntity>();

      _scopeRepositoryMock.Setup(repository => repository.GetScopesAsync(It.IsAny<IEnumerable<IScopeIdentity>>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(controlScopeEntityCollection)
                          .Verifiable();

      var scopeNameCollection = new[]
      {
        Guid.NewGuid().ToString(),
      };

      var actualScopeEntityCollection =
        await _scopeService.GetScopesAsync(scopeNameCollection, _cancellationToken);

      Assert.IsNotNull(actualScopeEntityCollection);
      Assert.AreEqual(controlScopeEntityCollection, actualScopeEntityCollection);

      _scopeRepositoryMock.Verify(repository => repository.GetScopesAsync(scopeNameCollection.ToScopeIdentities(), false, _cancellationToken));
      _scopeRepositoryMock.VerifyNoOtherCalls();
    }
  }
}
