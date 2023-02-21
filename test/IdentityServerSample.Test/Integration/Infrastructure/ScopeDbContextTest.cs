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
      var creatingScopeEntity = ScopeDbContextTest.GenerateTestScope();

      var creatingScopeEntityEntry = DbContext.Add(creatingScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var createdScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == creatingScopeEntity.Name)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdScopeEntity);

      Assert.AreEqual(creatingScopeEntity.Name, createdScopeEntity.Name);
      Assert.AreEqual(creatingScopeEntity.DisplayName, createdScopeEntity.DisplayName);
      Assert.AreEqual(creatingScopeEntity.Description, createdScopeEntity.Description);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Update_Scope()
    {
      var creatingScopeEntity = ScopeDbContextTest.GenerateTestScope();

      var creatingScopeEntityEntry = DbContext.Add(creatingScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var updatingScopeDisplayName = Guid.NewGuid().ToString();
      var updatingScopeDescription = Guid.NewGuid().ToString();

      var updatingScopeEntity = new ScopeEntity
      {
        Name = creatingScopeEntity.Name,
        DisplayName = updatingScopeDisplayName,
        Description = updatingScopeDescription,
      };

      var updatingScopeEntityEntry = DbContext.Attach(updatingScopeEntity);

      updatingScopeEntityEntry.State = EntityState.Modified;

      await DbContext.SaveChangesAsync(CancellationToken);

      updatingScopeEntityEntry.State = EntityState.Detached;

      var updatedScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == creatingScopeEntity.Name)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(updatedScopeEntity);

      Assert.AreEqual(creatingScopeEntity.Name, updatedScopeEntity.Name);
      Assert.AreEqual(updatingScopeDisplayName, updatedScopeEntity.DisplayName);
      Assert.AreEqual(updatingScopeDescription, updatedScopeEntity.Description);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Delete_Scope()
    {
      var creatingScopeEntity = ScopeDbContextTest.GenerateTestScope();

      var creatingScopeEntityEntry = DbContext.Add(creatingScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var createdScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == creatingScopeEntity.Name)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdScopeEntity);

      DbContext.Entry(createdScopeEntity!).State = EntityState.Deleted;

      await DbContext.SaveChangesAsync(CancellationToken);

      var deletedScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == creatingScopeEntity.Name)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNull(deletedScopeEntity);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Ignore_Standard()
    {
      var creatingScopeStandard = true;

      var creatingScopeEntity = ScopeDbContextTest.GenerateTestScope();
      creatingScopeEntity.Standard = creatingScopeStandard;

      var creatingScopeEntityEntry = DbContext.Add(creatingScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var createdScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == creatingScopeEntity.Name)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdScopeEntity);

      Assert.AreEqual(creatingScopeEntity.Name, createdScopeEntity!.Name);
      Assert.AreEqual(creatingScopeEntity.DisplayName, createdScopeEntity.DisplayName);
      Assert.AreEqual(creatingScopeEntity.Description, createdScopeEntity.Description);
      Assert.AreNotEqual(creatingScopeStandard, createdScopeEntity.Standard);
    }

    private static ScopeEntity GenerateTestScope() => new ScopeEntity
    {
      Name = Guid.NewGuid().ToString(),
      DisplayName = Guid.NewGuid().ToString(),
      Description = Guid.NewGuid().ToString(),
    };
  }
}
