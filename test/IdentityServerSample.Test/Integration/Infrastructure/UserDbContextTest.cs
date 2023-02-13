// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Test
{
  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.ApplicationCore.Entities;

  [TestClass]
  public sealed class UserDbContextTest : DbIntegrationTestBase
  {
    [TestMethod]
    public async Task SaveChangesAsync_Should_Create_User()
    {
      var creatingUserName = Guid.NewGuid().ToString();
      var creatingUserEmail = Guid.NewGuid().ToString();
      var creatingUserPasswordHash = Guid.NewGuid().ToString();

      var creatingUserEntity = new UserEntity
      {
        Name = creatingUserName,
        Email = creatingUserEmail,
        PasswordHash = creatingUserPasswordHash,
      };

      var creatingUserEntityEntry = DbContext.Add(creatingUserEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingUserEntityEntry.State = EntityState.Detached;

      var createdUserEntity =
        await DbContext.Set<UserEntity>()
                       .WithPartitionKey(creatingUserEntity.UserId.ToString())
                       .Where(entity => entity.UserId == creatingUserEntity.UserId)
                       .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdUserEntity);

      Assert.AreEqual(creatingUserName, createdUserEntity!.Name);
      Assert.AreEqual(creatingUserEmail, createdUserEntity!.Email);
      Assert.AreEqual(creatingUserPasswordHash, createdUserEntity!.PasswordHash);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Update_User()
    {
      var creatingUserName = Guid.NewGuid().ToString();
      var creatingUserEmail = Guid.NewGuid().ToString();
      var creatingUserPasswordHash = Guid.NewGuid().ToString();

      var creatingUserEntity = new UserEntity
      {
        Name = creatingUserName,
        Email = creatingUserEmail,
        PasswordHash = creatingUserPasswordHash,
      };

      var creatingUserEntityEntry = DbContext.Add(creatingUserEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingUserEntityEntry.State = EntityState.Detached;

      var updatingUserName = Guid.NewGuid().ToString();
      var updatingUserEmail = Guid.NewGuid().ToString();
      var updatingUserPasswordHash = Guid.NewGuid().ToString();

      var updatingUserEntity = new UserEntity
      {
        UserId = creatingUserEntity.UserId,
        Name = creatingUserName,
        Email = updatingUserEmail,
        PasswordHash = updatingUserPasswordHash,
      };

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

      Assert.AreEqual(creatingUserName, updatedUserEntity!.Name);
      Assert.AreEqual(updatingUserEmail, updatedUserEntity!.Email);
      Assert.AreEqual(updatingUserPasswordHash, updatedUserEntity!.PasswordHash);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Delete_User()
    {
      var creatingUserEntity = new UserEntity
      {
        Name = Guid.NewGuid().ToString(),
        Email = Guid.NewGuid().ToString(),
        PasswordHash = Guid.NewGuid().ToString(),
      };

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
  }
}
