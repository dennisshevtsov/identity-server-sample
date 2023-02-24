// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using IdentityServerSample.ApplicationCore.Identities;
using System.Threading;

namespace IdentityServerSample.ApplicationCore.Services.Test
{
  [TestClass]
  public sealed class AudienceScopeServiceTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IAudienceScopeRepository> _audienceScopeRepositoryMock;

    private AudienceScopeService _audienceScopeService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _audienceScopeRepositoryMock = new Mock<IAudienceScopeRepository>();

      _audienceScopeService = new AudienceScopeService(_audienceScopeRepositoryMock.Object);
    }

    [TestMethod]
    public async Task GetAudienceScopesAsync_Should_Return_All_Audience_Scopes()
    {
      var audienceName0 = Guid.NewGuid().ToString();
      var audienceName1 = Guid.NewGuid().ToString();

      var controlAudienceScopeEntityCollection = new List<AudienceScopeEntity>
      {
        new AudienceScopeEntity
        {
          AudienceName = audienceName0,
          ScopeName = Guid.NewGuid().ToString(),
        },
        new AudienceScopeEntity
        {
          AudienceName = audienceName1,
          ScopeName = Guid.NewGuid().ToString(),
        },
        new AudienceScopeEntity
        {
          AudienceName = audienceName0,
          ScopeName = Guid.NewGuid().ToString(),
        },
      };

      _audienceScopeRepositoryMock.Setup(repository => repository.GetAudienceScopesAsync(It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(controlAudienceScopeEntityCollection)
                                  .Verifiable();

      var audienceScopeDictionary =
        await _audienceScopeService.GetAudienceScopesAsync(_cancellationToken);

      Assert.IsNotNull(audienceScopeDictionary);
      Assert.AreEqual(2, audienceScopeDictionary.Count);

      Assert.IsTrue(audienceScopeDictionary.ContainsKey(audienceName0));
      Assert.AreEqual(2, audienceScopeDictionary[audienceName0].Count);
      Assert.IsTrue(audienceScopeDictionary[audienceName0].Any(name => name == controlAudienceScopeEntityCollection[0].ScopeName));
      Assert.IsTrue(audienceScopeDictionary[audienceName0].Any(name => name == controlAudienceScopeEntityCollection[2].ScopeName));

      Assert.IsTrue(audienceScopeDictionary.ContainsKey(audienceName1));
      Assert.AreEqual(1, audienceScopeDictionary[audienceName1].Count);
      Assert.IsTrue(audienceScopeDictionary[audienceName1].Any(name => name == controlAudienceScopeEntityCollection[1].ScopeName));

      _audienceScopeRepositoryMock.Verify(repository => repository.GetAudienceScopesAsync(_cancellationToken));
      _audienceScopeRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetAudienceScopesAsync_Should_Return_Audience_Scopes_For_Scopes()
    {
      var audienceName0 = Guid.NewGuid().ToString();
      var audienceName1 = Guid.NewGuid().ToString();

      var controlAudienceScopeEntityCollection = new List<AudienceScopeEntity>
      {
        new AudienceScopeEntity
        {
          AudienceName = audienceName0,
          ScopeName = Guid.NewGuid().ToString(),
        },
        new AudienceScopeEntity
        {
          AudienceName = audienceName1,
          ScopeName = Guid.NewGuid().ToString(),
        },
        new AudienceScopeEntity
        {
          AudienceName = audienceName0,
          ScopeName = Guid.NewGuid().ToString(),
        },
      };

      _audienceScopeRepositoryMock.Setup(repository => repository.GetAudienceScopesAsync(It.IsAny<IEnumerable<IScopeIdentity>>(), It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(controlAudienceScopeEntityCollection)
                                  .Verifiable();

      var scopeIdentities = new List<IScopeIdentity>();

      var audienceScopeDictionary =
        await _audienceScopeService.GetAudienceScopesAsync(scopeIdentities, _cancellationToken);

      Assert.IsNotNull(audienceScopeDictionary);
      Assert.AreEqual(2, audienceScopeDictionary.Count);

      Assert.IsTrue(audienceScopeDictionary.ContainsKey(audienceName0));
      Assert.AreEqual(2, audienceScopeDictionary[audienceName0].Count);
      Assert.IsTrue(audienceScopeDictionary[audienceName0].Any(name => name == controlAudienceScopeEntityCollection[0].ScopeName));
      Assert.IsTrue(audienceScopeDictionary[audienceName0].Any(name => name == controlAudienceScopeEntityCollection[2].ScopeName));

      Assert.IsTrue(audienceScopeDictionary.ContainsKey(audienceName1));
      Assert.AreEqual(1, audienceScopeDictionary[audienceName1].Count);
      Assert.IsTrue(audienceScopeDictionary[audienceName1].Any(name => name == controlAudienceScopeEntityCollection[1].ScopeName));

      _audienceScopeRepositoryMock.Verify(repository => repository.GetAudienceScopesAsync(scopeIdentities, _cancellationToken));
      _audienceScopeRepositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetAudienceScopesAsync_Should_Return_Audience_Scopes_For_Audiences()
    {
      var audienceName0 = Guid.NewGuid().ToString();
      var audienceName1 = Guid.NewGuid().ToString();

      var controlAudienceScopeEntityCollection = new List<AudienceScopeEntity>
      {
        new AudienceScopeEntity
        {
          AudienceName = audienceName0,
          ScopeName = Guid.NewGuid().ToString(),
        },
        new AudienceScopeEntity
        {
          AudienceName = audienceName1,
          ScopeName = Guid.NewGuid().ToString(),
        },
        new AudienceScopeEntity
        {
          AudienceName = audienceName0,
          ScopeName = Guid.NewGuid().ToString(),
        },
      };

      _audienceScopeRepositoryMock.Setup(repository => repository.GetAudienceScopesAsync(It.IsAny<IEnumerable<IAudienceIdentity>>(), It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(controlAudienceScopeEntityCollection)
                                  .Verifiable();

      var audienceIdentities = new List<IAudienceIdentity>();

      var audienceScopeDictionary =
        await _audienceScopeService.GetAudienceScopesAsync(audienceIdentities, _cancellationToken);

      Assert.IsNotNull(audienceScopeDictionary);
      Assert.AreEqual(2, audienceScopeDictionary.Count);

      Assert.IsTrue(audienceScopeDictionary.ContainsKey(audienceName0));
      Assert.AreEqual(2, audienceScopeDictionary[audienceName0].Count);
      Assert.IsTrue(audienceScopeDictionary[audienceName0].Any(name => name == controlAudienceScopeEntityCollection[0].ScopeName));
      Assert.IsTrue(audienceScopeDictionary[audienceName0].Any(name => name == controlAudienceScopeEntityCollection[2].ScopeName));

      Assert.IsTrue(audienceScopeDictionary.ContainsKey(audienceName1));
      Assert.AreEqual(1, audienceScopeDictionary[audienceName1].Count);
      Assert.IsTrue(audienceScopeDictionary[audienceName1].Any(name => name == controlAudienceScopeEntityCollection[1].ScopeName));

      _audienceScopeRepositoryMock.Verify(repository => repository.GetAudienceScopesAsync(audienceIdentities, _cancellationToken));
      _audienceScopeRepositoryMock.VerifyNoOtherCalls();
    }
  }
}
