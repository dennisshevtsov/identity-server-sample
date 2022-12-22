// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Test.Integration.Infrastructure
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

  using IdentityServerSample.ApplicationCore.Entities;

  [TestClass]
  public sealed class DbContextTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private IDisposable _disposable;

    private DbContext _dbContext;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                    .Build();

      var scope = new ServiceCollection().AddDatabase(configuration)
                                         .BuildServiceProvider()
                                         .CreateScope();

      _disposable = scope;

      _dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
      _dbContext.Database.EnsureCreated();
    }

    [TestCleanup]
    public void Cleanup()
    {
      _dbContext?.Database?.EnsureDeleted();
      _disposable?.Dispose();
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Create_Scope()
    {
      var scopeName = Guid.NewGuid().ToString();
      var creatingScopeDesciption = Guid.NewGuid().ToString();
      var creatingScopeDisplayName = Guid.NewGuid().ToString();

      var creatingScopeEntity = new ScopeEntity
      {
        Name = scopeName,
        Description = creatingScopeDesciption,
        DisplayName = creatingScopeDisplayName,
      };

      var creatingScopeEntityEntry = _dbContext.Add(creatingScopeEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var createdScopeEntity =
        await _dbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == scopeName)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(createdScopeEntity);

      Assert.AreEqual(scopeName, createdScopeEntity!.Name);
      Assert.AreEqual(creatingScopeDesciption, createdScopeEntity!.Description);
      Assert.AreEqual(creatingScopeDisplayName, createdScopeEntity!.DisplayName);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Update_Scope()
    {
      var scopeName = Guid.NewGuid().ToString();
      var creatingScopeDesciption = Guid.NewGuid().ToString();
      var creatingScopeDisplayName = Guid.NewGuid().ToString();

      var creatingScopeEntity = new ScopeEntity
      {
        Name = scopeName,
        Description = creatingScopeDesciption,
        DisplayName = creatingScopeDisplayName,
      };

      var creatingScopeEntityEntry = _dbContext.Add(creatingScopeEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var updatingScopeDesciption = Guid.NewGuid().ToString();
      var updatingScopeDisplayName = Guid.NewGuid().ToString();

      var updatingScopeEntity = new ScopeEntity
      {
        Name = scopeName,
        Description = updatingScopeDesciption,
        DisplayName = updatingScopeDisplayName,
      };

      var updatingScopeEntityEntry = _dbContext.Attach(updatingScopeEntity);

      updatingScopeEntityEntry.State = EntityState.Modified;

      await _dbContext.SaveChangesAsync(_cancellationToken);

      updatingScopeEntityEntry.State = EntityState.Detached;

      var updatedScopeEntity =
        await _dbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == scopeName)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(updatedScopeEntity);

      Assert.AreEqual(scopeName, updatedScopeEntity!.Name);
      Assert.AreEqual(updatingScopeDesciption, updatedScopeEntity!.Description);
      Assert.AreEqual(updatingScopeDisplayName, updatedScopeEntity!.DisplayName);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Delete_Scope()
    {
      var scopeName = Guid.NewGuid().ToString();

      var creatingScopeEntity = new ScopeEntity
      {
        Name = scopeName,
        Description = Guid.NewGuid().ToString(),
        DisplayName = Guid.NewGuid().ToString(),
      };

      var creatingScopeEntityEntry = _dbContext.Add(creatingScopeEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var createdScopeEntity =
        await _dbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == scopeName)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(createdScopeEntity);

      _dbContext.Entry(createdScopeEntity!).State = EntityState.Deleted;

      await _dbContext.SaveChangesAsync(_cancellationToken);

      var deletedScopeEntity =
        await _dbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == scopeName)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNull(deletedScopeEntity);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Create_Audience()
    {
      var audienceName = Guid.NewGuid().ToString();
      var creatingAudienceDesciption = Guid.NewGuid().ToString();
      var creatingAudienceDisplayName = Guid.NewGuid().ToString();

      var creatingAudienceEntity = new AudienceEntity
      {
        Name = audienceName,
        Description = creatingAudienceDesciption,
        DisplayName = creatingAudienceDisplayName,
      };

      var creatingAudienceEntityEntry = _dbContext.Add(creatingAudienceEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      creatingAudienceEntityEntry.State = EntityState.Detached;

      var createdAudienceEntity =
        await _dbContext.Set<AudienceEntity>()
                        .Where(entity => entity.Name == audienceName)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(createdAudienceEntity);

      Assert.AreEqual(audienceName, createdAudienceEntity!.Name);
      Assert.AreEqual(creatingAudienceDesciption, createdAudienceEntity!.Description);
      Assert.AreEqual(creatingAudienceDisplayName, createdAudienceEntity!.DisplayName);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Update_Audience()
    {
      var audienceName = Guid.NewGuid().ToString();
      var creatingAudienceDesciption = Guid.NewGuid().ToString();
      var creatingAudienceDisplayName = Guid.NewGuid().ToString();

      var creatingAudienceEntity = new AudienceEntity
      {
        Name = audienceName,
        Description = creatingAudienceDesciption,
        DisplayName = creatingAudienceDisplayName,
      };

      var creatingAudienceEntityEntry = _dbContext.Add(creatingAudienceEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      creatingAudienceEntityEntry.State = EntityState.Detached;

      var updatingAudienceDesciption = Guid.NewGuid().ToString();
      var updatingAudienceDisplayName = Guid.NewGuid().ToString();

      var updatingAudienceEntity = new AudienceEntity
      {
        Name = audienceName,
        Description = updatingAudienceDesciption,
        DisplayName = updatingAudienceDisplayName,
      };

      var updatingScopeEntityEntry = _dbContext.Attach(updatingAudienceEntity);

      updatingScopeEntityEntry.State = EntityState.Modified;

      await _dbContext.SaveChangesAsync(_cancellationToken);

      updatingScopeEntityEntry.State = EntityState.Detached;

      var updatedAudienceEntity =
        await _dbContext.Set<AudienceEntity>()
                        .Where(entity => entity.Name == audienceName)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(updatedAudienceEntity);

      Assert.AreEqual(audienceName, updatedAudienceEntity!.Name);
      Assert.AreEqual(updatingAudienceDesciption, updatedAudienceEntity!.Description);
      Assert.AreEqual(updatingAudienceDisplayName, updatedAudienceEntity!.DisplayName);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Delete_Audience()
    {
      var audienceName = Guid.NewGuid().ToString();

      var creatingAudienceEntity = new AudienceEntity
      {
        Name = audienceName,
        Description = Guid.NewGuid().ToString(),
        DisplayName = Guid.NewGuid().ToString(),
      };

      var creatingAudienceEntityEntry = _dbContext.Add(creatingAudienceEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      creatingAudienceEntityEntry.State = EntityState.Detached;

      var createdAudienceEntity =
        await _dbContext.Set<AudienceEntity>()
                        .Where(entity => entity.Name == audienceName)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(createdAudienceEntity);

      _dbContext.Entry(createdAudienceEntity!).State = EntityState.Deleted;

      await _dbContext.SaveChangesAsync(_cancellationToken);

      var deletedAudienceEntity =
        await _dbContext.Set<AudienceEntity>()
                        .Where(entity => entity.Name == audienceName)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNull(deletedAudienceEntity);
    }
  }
}
