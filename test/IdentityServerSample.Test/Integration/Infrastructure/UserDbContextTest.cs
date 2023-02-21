// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Test
{
  using Microsoft.EntityFrameworkCore;

  [TestClass]
  public sealed class UserDbContextTest : DbIntegrationTestBase
  {
    [TestMethod]
    public async Task SaveChangesAsync_Should_Create_User()
    {
      var creatingUserEntity = UserDbContextTest.GenerateTestUser();

      var creatingUserEntityEntry = DbContext.Add(creatingUserEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingUserEntityEntry.State = EntityState.Detached;

      var createdUserEntity =
        await DbContext.Set<UserEntity>()
                       .WithPartitionKey(creatingUserEntity.UserId.ToString())
                       .Where(entity => entity.UserId == creatingUserEntity.UserId)
                       .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdUserEntity);

      Assert.AreEqual(creatingUserEntity.Name, createdUserEntity!.Name);
      Assert.AreEqual(creatingUserEntity.Email, createdUserEntity!.Email);
      Assert.AreEqual(creatingUserEntity.PasswordHash, createdUserEntity!.PasswordHash);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Update_User()
    {
      var creatingUserEntity = UserDbContextTest.GenerateTestUser();

      var creatingUserEntityEntry = DbContext.Add(creatingUserEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingUserEntityEntry.State = EntityState.Detached;

      var updatingUserEntity = UserDbContextTest.GenerateTestUser();
      updatingUserEntity.UserId = creatingUserEntity.UserId;

      var updatingScopeEntityEntry = DbContext.Attach(updatingUserEntity);

      updatingScopeEntityEntry.State = EntityState.Modified;

      await DbContext.SaveChangesAsync(CancellationToken);

      updatingScopeEntityEntry.State = EntityState.Detached;

      var updatedUserEntity =
        await DbContext.Set<UserEntity>()
                       .WithPartitionKey(creatingUserEntity.UserId.ToString())
                       .Where(entity => entity.UserId == creatingUserEntity.UserId)
                       .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(updatedUserEntity);

      Assert.AreEqual(updatingUserEntity.Name, updatedUserEntity!.Name);
      Assert.AreEqual(updatingUserEntity.Email, updatedUserEntity!.Email);
      Assert.AreEqual(updatingUserEntity.PasswordHash, updatedUserEntity!.PasswordHash);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Delete_User()
    {
      var creatingUserEntity = UserDbContextTest.GenerateTestUser();

      var creatingUserEntityEntry = DbContext.Add(creatingUserEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingUserEntityEntry.State = EntityState.Detached;

      var createdUserEntity =
        await DbContext.Set<UserEntity>()
                       .WithPartitionKey(creatingUserEntity.UserId.ToString())
                       .Where(entity => entity.UserId == creatingUserEntity.UserId)
                       .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdUserEntity);

      DbContext.Entry(createdUserEntity!).State = EntityState.Deleted;

      await DbContext.SaveChangesAsync(CancellationToken);

      var deletedUserEntity =
        await DbContext.Set<UserEntity>()
                       .WithPartitionKey(creatingUserEntity.UserId.ToString())
                       .Where(entity => entity.UserId == creatingUserEntity.UserId)
                       .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNull(deletedUserEntity);
    }

    private static UserEntity GenerateTestUser() => new UserEntity
    {
      Name = Guid.NewGuid().ToString(),
      Email = Guid.NewGuid().ToString(),
      PasswordHash = Guid.NewGuid().ToString(),
    };
  }
}
