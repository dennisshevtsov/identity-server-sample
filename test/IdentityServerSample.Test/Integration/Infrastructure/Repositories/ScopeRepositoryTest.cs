// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories.Test
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;

  using IdentityServerSample.Infrastructure.Test;
  using IdentityServerSample.ApplicationCore.Identities;

  [TestClass]
  public sealed class ScopeRepositoryTest : DbIntegrationTestBase
  {
#pragma warning disable CS8618
    private IScopeRepository _scopeRepository;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _scopeRepository = ServiceProvider.GetRequiredService<IScopeRepository>();
    }

    [TestMethod]
    public async Task GetScopeAsync_Should_Return_Null()
    {
      await CreateNewScopesAsync(10, false);

      var scopeIdentity = Guid.NewGuid().ToString().ToScopeIdentity();

      var testScopeEntity =
        await _scopeRepository.GetScopeAsync(scopeIdentity, CancellationToken);

      Assert.IsNull(testScopeEntity);
    }

    [TestMethod]
    public async Task GetScopeAsync_Should_Return_Scope()
    {
      var allScopeEntityCollection = await CreateNewScopesAsync(10, false);
      var controlScopeEntity = allScopeEntityCollection[2];

      var scopeIdentity = controlScopeEntity.ScopeName!.ToScopeIdentity();

      var testScopeEntity =
        await _scopeRepository.GetScopeAsync(scopeIdentity, CancellationToken);

      Assert.IsNotNull(testScopeEntity);

      ScopeRepositoryTest.AreEqual(controlScopeEntity, testScopeEntity);
      IsDetached(testScopeEntity);
    }

    [TestMethod]
    public async Task GetScopesAsync_Should_Return_All_Scopes()
    {
      await CreateNewScopesAsync(10, true);

      var controlScopeEntityCollection = await CreateNewScopesAsync(10, false);

      var testScopeEntityCollection =
        await _scopeRepository.GetScopesAsync(null, false, CancellationToken);

      ScopeRepositoryTest.AreEqual(controlScopeEntityCollection, testScopeEntityCollection);
      AreDetached(testScopeEntityCollection);
    }

    [TestMethod]
    public async Task GetScopesAsync_Should_Return_Scopes_With_Defined_Names()
    {
      await CreateNewScopesAsync(10, true);

      var allScopeEntityCollection = await CreateNewScopesAsync(10, false);
      var controlScopeEntityCollection =
        allScopeEntityCollection.Where((entity, index) => index % 2 == 0)
                                .ToList();

      var scopeIdentityCollection =
        controlScopeEntityCollection.Select(entity => entity.ScopeName!)
                                    .ToScopeIdentities();

      var testScopeEntityCollection =
        await _scopeRepository.GetScopesAsync(scopeIdentityCollection, false, CancellationToken);

      ScopeRepositoryTest.AreEqual(controlScopeEntityCollection, testScopeEntityCollection);
      AreDetached(testScopeEntityCollection);
    }

    [TestMethod]
    public async Task GetScopesAsync_Should_Return_All_Standard_Scopes()
    {
      await CreateNewScopesAsync(10, false);

      var controlScopeEntityCollection = await CreateNewScopesAsync(10, true);

      var testScopeEntityCollection =
        await _scopeRepository.GetScopesAsync(null, true, CancellationToken);

      ScopeRepositoryTest.AreEqual(controlScopeEntityCollection, testScopeEntityCollection);

      AreDetached(testScopeEntityCollection);
    }

    [TestMethod]
    public async Task GetScopesAsync_Should_Return_Standard_Scopes_With_Defined_Names()
    {
      await CreateNewScopesAsync(10, false);

      var allScopeEntityCollection = await CreateNewScopesAsync(10, true);
      var controlScopeEntityCollection =
        allScopeEntityCollection.Where((entity, index) => index % 2 == 0)
                                .ToList();

      var scopeIdentityCollection =
        controlScopeEntityCollection.Select(entity => entity.ScopeName!)
                                    .ToScopeIdentities();

      var testScopeEntityCollection =
        await _scopeRepository.GetScopesAsync(scopeIdentityCollection, true, CancellationToken);

      ScopeRepositoryTest.AreEqual(controlScopeEntityCollection, testScopeEntityCollection);
      AreDetached(testScopeEntityCollection);
    }

    private async Task<ScopeEntity> CreateNewScopeAsync(bool standard)
    {
      var scopeEntity = new ScopeEntity
      {
        ScopeName = Guid.NewGuid().ToString(),
        DisplayName = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Standard = standard,
      };

      var scopeEntityEntry = DbContext.Add(scopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      scopeEntityEntry.State = EntityState.Detached;

      return scopeEntity;
    }

    private async Task<List<ScopeEntity>> CreateNewScopesAsync(int scopes, bool standard)
    {
      var scopeEntityCollection = new List<ScopeEntity>();

      for (int i = 0; i < scopes; i++)
      {
        scopeEntityCollection.Add(await CreateNewScopeAsync(standard));
      }

      return scopeEntityCollection.OrderBy(entity => entity.ScopeName)
                                  .ToList();
    }

    private static void AreEqual(ScopeEntity control, ScopeEntity test)
    {
      Assert.AreEqual(control.ScopeName, test.ScopeName);
      Assert.AreEqual(control.DisplayName, test.DisplayName);
      Assert.AreEqual(control.Description, test.Description);
      Assert.AreEqual(control.Standard, test.Standard);
    }

    private static void AreEqual(List<ScopeEntity> control, List<ScopeEntity> test)
    {
      Assert.AreEqual(control.Count, test.Count);

      for (int i = 0; i < control.Count; i++)
      {
        ScopeRepositoryTest.AreEqual(control[i], test[i]);
      }
    }

    private void IsDetached(ScopeEntity scopeEntity)
      => Assert.AreEqual(EntityState.Detached, DbContext.Entry(scopeEntity).State);

    private void AreDetached(List<ScopeEntity> scopeEntityCollection)
    {
      foreach (var scopeEntity in scopeEntityCollection)
      {
        IsDetached(scopeEntity);
      }
    }
  }
}
