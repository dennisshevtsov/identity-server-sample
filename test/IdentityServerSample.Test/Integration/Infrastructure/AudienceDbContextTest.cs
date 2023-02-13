// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Test
{
  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.ApplicationCore.Entities;

  [TestClass]
  public sealed class AudienceDbContextTest : DbIntegrationTestBase
  {
    [TestMethod]
    public async Task SaveChangesAsync_Should_Create_Audience()
    {
      var audienceName = Guid.NewGuid().ToString();
      var creatingAudienceDisplayName = Guid.NewGuid().ToString();
      var creatingAudienceDescription = Guid.NewGuid().ToString();

      var creatingAudienceEntity = new AudienceEntity
      {
        Name = audienceName,
        DisplayName = creatingAudienceDisplayName,
        Description = creatingAudienceDescription,
      };

      var creatingAudienceEntityEntry = DbContext.Add(creatingAudienceEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingAudienceEntityEntry.State = EntityState.Detached;

      var createdAudienceEntity =
        await DbContext.Set<AudienceEntity>()
                        .Where(entity => entity.Name == audienceName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdAudienceEntity);

      Assert.AreEqual(audienceName, createdAudienceEntity!.Name);
      Assert.AreEqual(creatingAudienceDisplayName, createdAudienceEntity!.DisplayName);
      Assert.AreEqual(creatingAudienceDescription, createdAudienceEntity!.Description);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Update_Audience()
    {
      var audienceName = Guid.NewGuid().ToString();
      var creatingAudienceDisplayName = Guid.NewGuid().ToString();
      var creatingAudienceDesciption = Guid.NewGuid().ToString();

      var creatingAudienceEntity = new AudienceEntity
      {
        Name = audienceName,
        DisplayName = creatingAudienceDisplayName,
        Description = creatingAudienceDesciption,
      };

      var creatingAudienceEntityEntry = DbContext.Add(creatingAudienceEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingAudienceEntityEntry.State = EntityState.Detached;

      var updatingAudienceDisplayName = Guid.NewGuid().ToString();
      var updatingAudienceDescription = Guid.NewGuid().ToString();

      var updatingAudienceEntity = new AudienceEntity
      {
        Name = audienceName,
        DisplayName = updatingAudienceDisplayName,
        Description = updatingAudienceDescription,
      };

      var updatingScopeEntityEntry = DbContext.Attach(updatingAudienceEntity);

      updatingScopeEntityEntry.State = EntityState.Modified;

      await DbContext.SaveChangesAsync(CancellationToken);

      updatingScopeEntityEntry.State = EntityState.Detached;

      var updatedAudienceEntity =
        await DbContext.Set<AudienceEntity>()
                        .Where(entity => entity.Name == audienceName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(updatedAudienceEntity);

      Assert.AreEqual(audienceName, updatedAudienceEntity!.Name);
      Assert.AreEqual(updatingAudienceDisplayName, updatedAudienceEntity!.DisplayName);
      Assert.AreEqual(updatingAudienceDescription, updatedAudienceEntity!.Description);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Delete_Audience()
    {
      var audienceName = Guid.NewGuid().ToString();

      var creatingAudienceEntity = new AudienceEntity
      {
        Name = audienceName,
        DisplayName = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var creatingAudienceEntityEntry = DbContext.Add(creatingAudienceEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingAudienceEntityEntry.State = EntityState.Detached;

      var createdAudienceEntity =
        await DbContext.Set<AudienceEntity>()
                        .Where(entity => entity.Name == audienceName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdAudienceEntity);

      DbContext.Entry(createdAudienceEntity!).State = EntityState.Deleted;

      await DbContext.SaveChangesAsync(CancellationToken);

      var deletedAudienceEntity =
        await DbContext.Set<AudienceEntity>()
                        .Where(entity => entity.Name == audienceName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNull(deletedAudienceEntity);
    }
  }
}
