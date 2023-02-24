// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories.Test
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;

  using IdentityServerSample.Infrastructure.Test;
  using IdentityServerSample.ApplicationCore.Identities;
  using IdentityServerSample.ApplicationCore.Repositories;

  [TestClass]
  public sealed class AudienceScopeRepositoryTest : DbIntegrationTestBase
  {
#pragma warning disable CS8618
    private IAudienceScopeRepository _audienceScopeRepository;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _audienceScopeRepository = ServiceProvider.GetRequiredService<IAudienceScopeRepository>();
    }

    [TestMethod]
    public async Task GetAudienceScopesAsync_Should_Return_Audience_Scopes_For_Audience()
    {
      await CreateNewAudienceScopesAsync(Guid.NewGuid().ToString(), 10);

      var audienceName = Guid.NewGuid().ToString();
      var controlAudienceScopeEntityCollection =
        await CreateNewAudienceScopesAsync(audienceName, 10);

      var identity = audienceName.ToAudienceIdentity();

      var testAudienceScopeEntityCollection =
        await _audienceScopeRepository.GetAudienceScopesAsync(identity, CancellationToken);

      Assert.AreEqual(controlAudienceScopeEntityCollection.Count, testAudienceScopeEntityCollection.Count);

      AudienceScopeRepositoryTest.AreEqual(
        controlAudienceScopeEntityCollection, testAudienceScopeEntityCollection);

      AreDetached(testAudienceScopeEntityCollection);
    }

    [TestMethod]
    public async Task GetAudienceScopesAsync_Should_Return_All_Audience_Scopes()
    {
      var audienceName = Guid.NewGuid().ToString();
      var controlAudienceScopeEntityCollection =
        await CreateNewAudienceScopesAsync(audienceName, 10);

      var testAudienceScopeEntityCollection =
        await _audienceScopeRepository.GetAudienceScopesAsync(CancellationToken);

      Assert.AreEqual(controlAudienceScopeEntityCollection.Count, testAudienceScopeEntityCollection.Count);

      AudienceScopeRepositoryTest.AreEqual(
        controlAudienceScopeEntityCollection, testAudienceScopeEntityCollection);

      AreDetached(testAudienceScopeEntityCollection);
    }

    [TestMethod]
    public async Task GetAudienceScopesAsync_Should_Return_Audience_Scopes_For_Scopes()
    {
      var allAudienceScopeEntityCollection =
        await CreateNewAudienceScopesAsync(Guid.NewGuid().ToString(), 10);
      var controlAudienceScopeEntityCollection =
        allAudienceScopeEntityCollection.Where((entity, index) => index % 2 == 0)
                                        .ToList();

      var scopeIdentities =
        controlAudienceScopeEntityCollection.Select(entity => entity.ScopeName!)
                                            .ToScopeIdentities();

      var testAudienceScopeEntityCollection =
        await _audienceScopeRepository.GetAudienceScopesAsync(
          scopeIdentities, CancellationToken);

      Assert.AreEqual(controlAudienceScopeEntityCollection.Count, testAudienceScopeEntityCollection.Count);

      AudienceScopeRepositoryTest.AreEqual(
        controlAudienceScopeEntityCollection, testAudienceScopeEntityCollection);

      AreDetached(testAudienceScopeEntityCollection);
    }

    [TestMethod]
    public async Task GetAudienceScopesAsync_Should_Return_Audience_Scopes_For_Audiencies()
    {
      var audienceNames = new[]
      {
        Guid.NewGuid().ToString(),
        Guid.NewGuid().ToString(),
      };

      var controlAudienceScopeEntityCollection = new List<AudienceScopeEntity>();

      controlAudienceScopeEntityCollection.AddRange(
        await CreateNewAudienceScopesAsync(audienceNames[0], 5));
      controlAudienceScopeEntityCollection.AddRange(
        await CreateNewAudienceScopesAsync(audienceNames[1], 10));

      controlAudienceScopeEntityCollection =
        controlAudienceScopeEntityCollection.OrderBy(entity => entity.ScopeName)
                                            .ToList();

      await CreateNewAudienceScopesAsync(Guid.NewGuid().ToString(), 10);

      var audienciesIdentities = audienceNames.ToAudienceIdentities();

      var testAudienceScopeEntityCollection =
        await _audienceScopeRepository.GetAudienceScopesAsync(
          audienciesIdentities, CancellationToken);

      Assert.AreEqual(controlAudienceScopeEntityCollection.Count, testAudienceScopeEntityCollection.Count);

      AudienceScopeRepositoryTest.AreEqual(
        controlAudienceScopeEntityCollection, testAudienceScopeEntityCollection);

      AreDetached(testAudienceScopeEntityCollection);
    }

    private async Task<AudienceScopeEntity> CreateNewAudienceScopeAsync(string audienceName)
    {
      var audienceScopeEntity = new AudienceScopeEntity
      {
        AudienceName = audienceName,
        ScopeName = Guid.NewGuid().ToString(),
      };

      var audienceScopeEntityEntry = DbContext.Add(audienceScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      audienceScopeEntityEntry.State = EntityState.Detached;

      return audienceScopeEntity;
    }

    private async Task<List<AudienceScopeEntity>> CreateNewAudienceScopesAsync(
      string audienceName, int audiences)
    {
      var audienceScopeEntityCollection = new List<AudienceScopeEntity>();

      for (int i = 0; i < audiences; i++)
      {
        audienceScopeEntityCollection.Add(await CreateNewAudienceScopeAsync(audienceName));
      }

      return audienceScopeEntityCollection.OrderBy(entity => entity.ScopeName)
                                          .ToList();
    }

    private static void AreEqual(AudienceScopeEntity control, AudienceScopeEntity test)
    {
      Assert.AreEqual(control.AudienceName, test.AudienceName);
      Assert.AreEqual(control.ScopeName, test.ScopeName);
    }

    private static void AreEqual(
      List<AudienceScopeEntity> control, List<AudienceScopeEntity> test)
    {
      for (int i = 0; i < control.Count; i++)
      {
        AudienceScopeRepositoryTest.AreEqual(control[i], test[i]);
      }
    }

    private void IsDetached(AudienceScopeEntity audienceEntity)
      => Assert.AreEqual(EntityState.Detached, DbContext.Entry(audienceEntity).State);

    private void AreDetached(List<AudienceScopeEntity> audienceEntityCollection)
    {
      for (int i = 0; i < audienceEntityCollection.Count; i++)
      {
        IsDetached(audienceEntityCollection[i]);
      }
    }
  }
}
