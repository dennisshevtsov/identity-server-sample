// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Test
{
  using Microsoft.EntityFrameworkCore;

  [TestClass]
  public sealed class AudienceScopeDbContextTest : DbIntegrationTestBase
  {
    [TestMethod]
    public async Task SaveChangesAsync_Should_Create_Audience_Scope()
    {
      var audienceName = Guid.NewGuid().ToString();
      var creatingAudienceScopeEntity = AudienceScopeDbContextTest.GenerateTestAudienceScope(audienceName);

      var creatingAudienceScopeEntityEntry = DbContext.Add(creatingAudienceScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingAudienceScopeEntityEntry.State = EntityState.Detached;

      var createdAudienceScopeEntity =
        await DbContext.Set<AudienceScopeEntity>()
                       .WithPartitionKey(audienceName)
                       .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdAudienceScopeEntity);
      Assert.AreEqual(creatingAudienceScopeEntity.ScopeName, createdAudienceScopeEntity.ScopeName);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Delete_Audience_Scope()
    {
      var audienceName = Guid.NewGuid().ToString();
      var creatingAudienceScopeEntity = AudienceScopeDbContextTest.GenerateTestAudienceScope(audienceName);

      var creatingAudienceScopeEntityEntry = DbContext.Add(creatingAudienceScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingAudienceScopeEntityEntry.State = EntityState.Detached;

      var createdAudienceScopeEntity =
        await DbContext.Set<AudienceScopeEntity>()
                       .WithPartitionKey(audienceName)
                       .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdAudienceScopeEntity);

      DbContext.Entry(createdAudienceScopeEntity!).State = EntityState.Deleted;

      await DbContext.SaveChangesAsync(CancellationToken);

      var deletedAudienceScopeEntity =
        await DbContext.Set<AudienceScopeEntity>()
                       .WithPartitionKey(audienceName)
                       .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNull(deletedAudienceScopeEntity);
    }

    private static AudienceScopeEntity GenerateTestAudienceScope(string audienceName) => new AudienceScopeEntity
    {
      AudienceName = audienceName,
      ScopeName = Guid.NewGuid().ToString(),
    };
  }
}
