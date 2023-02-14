// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories.Test
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Repositories;
  using IdentityServerSample.Infrastructure.Test;

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
    public async Task GetScopesAsync_Should_Return_All_Scopes()
    {
      var controlScopeEntityCollection = await CreateNewScopesAsync(10);

      var testScopeEntityCollection =
        await _scopeRepository.GetScopesAsync(CancellationToken);

      Assert.AreEqual(controlScopeEntityCollection.Length, testScopeEntityCollection.Count);

      for (int i = 0; i < controlScopeEntityCollection.Length; i++)
      {
        AreEqual(controlScopeEntityCollection[i], testScopeEntityCollection[i]);
      }

      AreDetached(testScopeEntityCollection);
    }

    [TestMethod]
    public async Task GetScopesAsync_Should_Return_Scopes_With_Defined_Names()
    {
      var allScopeEntityCollection = await CreateNewScopesAsync(10);
      var controlScopeEntityCollection =
        allScopeEntityCollection.Where((entity, index) => index % 2 == 0)
                                .ToArray();

      var scopeNameCollection =
        controlScopeEntityCollection.Select(entity => entity.Name!)
                                    .ToArray();

      var testScopeEntityCollection =
        await _scopeRepository.GetScopesAsync(scopeNameCollection, CancellationToken);

      Assert.AreEqual(controlScopeEntityCollection.Length, testScopeEntityCollection.Count);

      for (int i = 0; i < controlScopeEntityCollection.Length; i++)
      {
        AreEqual(controlScopeEntityCollection[i], testScopeEntityCollection[i]);
      }

      AreDetached(testScopeEntityCollection);
    }

    private async Task<ScopeEntity> CreateNewScopeAsync()
    {
      var scopeEntity = new ScopeEntity
      {
        Name = Guid.NewGuid().ToString(),
        DisplayName = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var scopeEntityEntry = DbContext.Add(scopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      scopeEntityEntry.State = EntityState.Detached;

      return scopeEntity;
    }

    private async Task<ScopeEntity[]> CreateNewScopesAsync(int audiences)
    {
      var scopeEntityCollection = new List<ScopeEntity>();

      for (int i = 0; i < audiences; i++)
      {
        scopeEntityCollection.Add(await CreateNewScopeAsync());
      }

      return scopeEntityCollection.OrderBy(entity => entity.Name)
                                  .ToArray();
    }

    private void AreEqual(ScopeEntity control, ScopeEntity test)
    {
      Assert.AreEqual(control.Name, test.Name);
      Assert.AreEqual(control.DisplayName, test.DisplayName);
      Assert.AreEqual(control.Description, test.Description);
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
