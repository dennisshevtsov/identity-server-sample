// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Test
{
  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.ApplicationCore.Entities;

  [TestClass]
  public sealed class ScopeDbContextTest : DbIntegrationTestBase
  {
    [TestMethod]
    public async Task SaveChangesAsync_Should_Create_Scope()
    {
      var scopeName = Guid.NewGuid().ToString();
      var creatingScopeDisplayName = Guid.NewGuid().ToString();
      var creatingScopeDescription = Guid.NewGuid().ToString();

      var creatingScopeEntity = new ScopeEntity
      {
        Name = scopeName,
        DisplayName = creatingScopeDisplayName,
        Description = creatingScopeDescription,
      };

      var creatingScopeEntityEntry = DbContext.Add(creatingScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var createdScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == scopeName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdScopeEntity);

      Assert.AreEqual(scopeName, createdScopeEntity!.Name);
      Assert.AreEqual(creatingScopeDisplayName, createdScopeEntity!.DisplayName);
      Assert.AreEqual(creatingScopeDescription, createdScopeEntity!.Description);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Update_Scope()
    {
      var scopeName = Guid.NewGuid().ToString();
      var creatingScopeDisplayName = Guid.NewGuid().ToString();
      var creatingScopeDescription = Guid.NewGuid().ToString();

      var creatingScopeEntity = new ScopeEntity
      {
        Name = scopeName,
        DisplayName = creatingScopeDisplayName,
        Description = creatingScopeDescription,
      };

      var creatingScopeEntityEntry = DbContext.Add(creatingScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var updatingScopeDisplayName = Guid.NewGuid().ToString();
      var updatingScopeDescription = Guid.NewGuid().ToString();

      var updatingScopeEntity = new ScopeEntity
      {
        Name = scopeName,
        DisplayName = updatingScopeDisplayName,
        Description = updatingScopeDescription,
      };

      var updatingScopeEntityEntry = DbContext.Attach(updatingScopeEntity);

      updatingScopeEntityEntry.State = EntityState.Modified;

      await DbContext.SaveChangesAsync(CancellationToken);

      updatingScopeEntityEntry.State = EntityState.Detached;

      var updatedScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == scopeName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(updatedScopeEntity);

      Assert.AreEqual(scopeName, updatedScopeEntity!.Name);
      Assert.AreEqual(updatingScopeDisplayName, updatedScopeEntity!.DisplayName);
      Assert.AreEqual(updatingScopeDescription, updatedScopeEntity!.Description);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Delete_Scope()
    {
      var scopeName = Guid.NewGuid().ToString();

      var creatingScopeEntity = new ScopeEntity
      {
        Name = scopeName,
        DisplayName = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var creatingScopeEntityEntry = DbContext.Add(creatingScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var createdScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == scopeName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdScopeEntity);

      DbContext.Entry(createdScopeEntity!).State = EntityState.Deleted;

      await DbContext.SaveChangesAsync(CancellationToken);

      var deletedScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == scopeName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNull(deletedScopeEntity);
    }
  }
}
