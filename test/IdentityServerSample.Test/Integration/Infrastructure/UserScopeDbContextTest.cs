// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Test
{
  using Microsoft.EntityFrameworkCore;

  [TestClass]
  public sealed class UserScopeDbContextTest : DbIntegrationTestBase
  {
    [TestMethod]
    public async Task SaveChangesAsync_Should_Create_Scope()
    {
      var creatingUserScopeEntity = UserScopeDbContextTest.GenerateTestUserScope();

      var creatingUserScopeEntityEntry = DbContext.Add(creatingUserScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingUserScopeEntityEntry.State = EntityState.Detached;

      var createdUserScopeEntity =
        await DbContext.Set<UserScopeEntity>()
                       .WithPartitionKey(creatingUserScopeEntity.UserId.ToString())
                       .Where(entity => entity.ScopeName == creatingUserScopeEntity.ScopeName)
                       .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdUserScopeEntity);

      Assert.AreEqual(creatingUserScopeEntity.ScopeName, createdUserScopeEntity.ScopeName);
      Assert.AreEqual(creatingUserScopeEntity.UserId, createdUserScopeEntity.UserId);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Delete_Scope()
    {
      var creatingUserScopeEntity = UserScopeDbContextTest.GenerateTestUserScope();

      var creatingUserScopeEntityEntry = DbContext.Add(creatingUserScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingUserScopeEntityEntry.State = EntityState.Detached;

      var deletingUserScopeEntity = new UserScopeEntity
      {
        ScopeName = creatingUserScopeEntity.ScopeName,
        UserId= creatingUserScopeEntity.UserId,
      };

      var deletingUserScopeEntityEntry = DbContext.Remove(deletingUserScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      deletingUserScopeEntityEntry.State = EntityState.Detached;

      var deletedUserScopeEntity =
        await DbContext.Set<UserScopeEntity>()
                       .WithPartitionKey(creatingUserScopeEntity.UserId.ToString())
                       .Where(entity => entity.ScopeName == creatingUserScopeEntity.ScopeName)
                       .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNull(deletedUserScopeEntity);
    }

    private static UserScopeEntity GenerateTestUserScope() => new UserScopeEntity
    {
      ScopeName = Guid.NewGuid().ToString(),
      UserId = Guid.NewGuid(),
    };
  }
}
