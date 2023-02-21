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
                       .Where(entity => entity.Name == creatingUserScopeEntity.Name)
                       .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdUserScopeEntity);

      Assert.AreEqual(creatingUserScopeEntity.Name, createdUserScopeEntity.Name);
      Assert.AreEqual(creatingUserScopeEntity.UserId, createdUserScopeEntity.UserId);
    }

    private static UserScopeEntity GenerateTestUserScope() => new UserScopeEntity
    {
      Name = Guid.NewGuid().ToString(),
      UserId = Guid.NewGuid(),
    };
  }
}
