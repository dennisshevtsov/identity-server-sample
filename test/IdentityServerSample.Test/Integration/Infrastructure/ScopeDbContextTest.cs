// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Test
{
  using Microsoft.EntityFrameworkCore;

  [TestClass]
  public sealed class ScopeDbContextTest : DbIntegrationTestBase
  {
    [TestMethod]
    public async Task SaveChangesAsync_Should_Create_Scope()
    {
      var scopeName = Guid.NewGuid().ToString();
      var creatingScopeEntity = ScopeDbContextTest.GenerateTestScope(scopeName);

      var creatingScopeEntityEntry = DbContext.Add(creatingScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var createdScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.ScopeName == scopeName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdScopeEntity);

      Assert.AreEqual(scopeName, createdScopeEntity.ScopeName);
      Assert.AreEqual(creatingScopeEntity.DisplayName, createdScopeEntity.DisplayName);
      Assert.AreEqual(creatingScopeEntity.Description, createdScopeEntity.Description);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Update_Scope()
    {
      var scopeName = Guid.NewGuid().ToString();
      var creatingScopeEntity = ScopeDbContextTest.GenerateTestScope(scopeName);

      var creatingScopeEntityEntry = DbContext.Add(creatingScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var updatingScopeEntity = ScopeDbContextTest.GenerateTestScope(scopeName);

      var updatingScopeEntityEntry = DbContext.Attach(updatingScopeEntity);

      updatingScopeEntityEntry.State = EntityState.Modified;

      await DbContext.SaveChangesAsync(CancellationToken);

      updatingScopeEntityEntry.State = EntityState.Detached;

      var updatedScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.ScopeName == creatingScopeEntity.ScopeName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(updatedScopeEntity);

      Assert.AreEqual(scopeName, updatedScopeEntity.ScopeName);
      Assert.AreEqual(updatingScopeEntity.DisplayName, updatedScopeEntity.DisplayName);
      Assert.AreEqual(updatingScopeEntity.Description, updatedScopeEntity.Description);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Delete_Scope()
    {
      var scopeName = Guid.NewGuid().ToString();
      var creatingScopeEntity = ScopeDbContextTest.GenerateTestScope(scopeName);

      var creatingScopeEntityEntry = DbContext.Add(creatingScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var createdScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.ScopeName == scopeName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdScopeEntity);

      DbContext.Entry(createdScopeEntity!).State = EntityState.Deleted;

      await DbContext.SaveChangesAsync(CancellationToken);

      var deletedScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.ScopeName == scopeName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNull(deletedScopeEntity);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Ignore_Standard()
    {
      var scopeName = Guid.NewGuid().ToString();
      var creatingScopeStandard = true;

      var creatingScopeEntity = ScopeDbContextTest.GenerateTestScope(scopeName);
      creatingScopeEntity.Standard = creatingScopeStandard;

      var creatingScopeEntityEntry = DbContext.Add(creatingScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var createdScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.ScopeName == scopeName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdScopeEntity);

      Assert.AreEqual(scopeName, createdScopeEntity!.ScopeName);
      Assert.AreEqual(creatingScopeEntity.DisplayName, createdScopeEntity.DisplayName);
      Assert.AreEqual(creatingScopeEntity.Description, createdScopeEntity.Description);
      Assert.AreNotEqual(creatingScopeStandard, createdScopeEntity.Standard);
    }

    private static ScopeEntity GenerateTestScope(string scopeName) => new ScopeEntity
    {
      ScopeName = scopeName,
      DisplayName = Guid.NewGuid().ToString(),
      Description = Guid.NewGuid().ToString(),
    };
  }
}
