﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
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

      var scope = new ServiceCollection().SetUpDatabase(configuration)
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
      var creatingScopeDisplayName = Guid.NewGuid().ToString();
      var creatingScopeDescription = Guid.NewGuid().ToString();

      var creatingScopeEntity = new ScopeEntity
      {
        Name = scopeName,
        DisplayName = creatingScopeDisplayName,
        Description = creatingScopeDescription,
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

      var creatingScopeEntityEntry = _dbContext.Add(creatingScopeEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var updatingScopeDisplayName = Guid.NewGuid().ToString();
      var updatingScopeDescription = Guid.NewGuid().ToString();

      var updatingScopeEntity = new ScopeEntity
      {
        Name = scopeName,
        DisplayName = updatingScopeDisplayName,
        Description = updatingScopeDescription,
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
      var creatingAudienceDisplayName = Guid.NewGuid().ToString();
      var creatingAudienceDescription = Guid.NewGuid().ToString();

      var creatingAudienceEntity = new AudienceEntity
      {
        Name = audienceName,
        DisplayName = creatingAudienceDisplayName,
        Description = creatingAudienceDescription,
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

      var creatingAudienceEntityEntry = _dbContext.Add(creatingAudienceEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      creatingAudienceEntityEntry.State = EntityState.Detached;

      var updatingAudienceDisplayName = Guid.NewGuid().ToString();
      var updatingAudienceDescription = Guid.NewGuid().ToString();

      var updatingAudienceEntity = new AudienceEntity
      {
        Name = audienceName,
        DisplayName = updatingAudienceDisplayName,
        Description = updatingAudienceDescription,
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

    [TestMethod]
    public async Task SaveChangesAsync_Should_Create_Client()
    {
      var clientName = Guid.NewGuid().ToString();
      var creatingCleintDisplayName = Guid.NewGuid().ToString();
      var creatingClientDesciption = Guid.NewGuid().ToString();
      var creatingScopeName = Guid.NewGuid().ToString();
      var creatingRedirectUri = Guid.NewGuid().ToString();
      var creatingPostRedirectUri = Guid.NewGuid().ToString();

      var creatingAudienceEntity = new ClientEntity
      {
        Name = clientName,
        DisplayName = creatingCleintDisplayName,
        Description = creatingClientDesciption,
        Scopes = new[] { creatingScopeName },
        RedirectUris = new[] { creatingRedirectUri },
        PostRedirectUris = new[] { creatingPostRedirectUri },
      };

      var creatingClientEntityEntry = _dbContext.Add(creatingAudienceEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      creatingClientEntityEntry.State = EntityState.Detached;

      var createdClientEntity =
        await _dbContext.Set<ClientEntity>()
                        .Where(entity => entity.Name == clientName)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(createdClientEntity);

      Assert.AreEqual(clientName, createdClientEntity!.Name);
      Assert.AreEqual(creatingCleintDisplayName, createdClientEntity!.DisplayName);
      Assert.AreEqual(creatingClientDesciption, createdClientEntity!.Description);

      Assert.IsNotNull(createdClientEntity!.Scopes);
      Assert.AreEqual(1, createdClientEntity!.Scopes!.Count());
      Assert.AreEqual(creatingScopeName, createdClientEntity!.Scopes!.First());

      Assert.IsNotNull(createdClientEntity!.RedirectUris);
      Assert.AreEqual(1, createdClientEntity!.RedirectUris!.Count());
      Assert.AreEqual(creatingRedirectUri, createdClientEntity!.RedirectUris!.First());

      Assert.IsNotNull(createdClientEntity!.PostRedirectUris);
      Assert.AreEqual(1, createdClientEntity!.PostRedirectUris!.Count());
      Assert.AreEqual(creatingPostRedirectUri, createdClientEntity!.PostRedirectUris!.First());
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Update_Client()
    {
      var clientName = Guid.NewGuid().ToString();
      var scopeName0 = Guid.NewGuid().ToString();
      var redirectUri0 = Guid.NewGuid().ToString();
      var postRedirectUri0 = Guid.NewGuid().ToString();

      var creatingAudienceEntity = new ClientEntity
      {
        Name = clientName,
        DisplayName = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Scopes = new[] { scopeName0 },
        RedirectUris = new[] { redirectUri0 },
        PostRedirectUris = new[] { postRedirectUri0 },
      };

      var creatingClientEntityEntry = _dbContext.Add(creatingAudienceEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      creatingClientEntityEntry.State = EntityState.Detached;

      var updatingCleintDisplayName = Guid.NewGuid().ToString();
      var updatingClientDesciption = Guid.NewGuid().ToString();
      var scopeName1 = Guid.NewGuid().ToString();
      var redirectUri1 = Guid.NewGuid().ToString();
      var postRedirectUri1 = Guid.NewGuid().ToString();

      var updatingClientEntity = new ClientEntity
      {
        Name = clientName,
        DisplayName = updatingCleintDisplayName,
        Description = updatingClientDesciption,
        Scopes = new[] { scopeName0, scopeName1 },
        PostRedirectUris = new[] { postRedirectUri0, postRedirectUri1 },
        RedirectUris = new[] { redirectUri0, redirectUri1 },
      };

      var updatingClientEntityEntry = _dbContext.Attach(updatingClientEntity);

      updatingClientEntityEntry.State = EntityState.Modified;

      await _dbContext.SaveChangesAsync(_cancellationToken);

      updatingClientEntityEntry.State = EntityState.Detached;

      var updatedClientEntity =
        await _dbContext.Set<ClientEntity>()
                        .Where(entity => entity.Name == clientName)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(updatedClientEntity);
      Assert.AreEqual(clientName, updatedClientEntity!.Name);
      Assert.AreEqual(updatingCleintDisplayName, updatedClientEntity!.DisplayName);
      Assert.AreEqual(updatingClientDesciption, updatedClientEntity!.Description);

      Assert.IsNotNull(updatedClientEntity!.Scopes);

      var updatedScopes = updatedClientEntity!.Scopes!.ToArray();

      Assert.AreEqual(2, updatedScopes.Length);
      Assert.AreEqual(scopeName0, updatedScopes[0]);
      Assert.AreEqual(scopeName1, updatedScopes[1]);

      Assert.IsNotNull(updatedClientEntity!.Scopes);

      var updatedRedirectUris = updatedClientEntity!.RedirectUris!.ToArray();

      Assert.AreEqual(2, updatedRedirectUris.Length);
      Assert.AreEqual(redirectUri0, updatedRedirectUris[0]);
      Assert.AreEqual(redirectUri1, updatedRedirectUris[1]);

      Assert.IsNotNull(updatedClientEntity!.Scopes);

      var updatedPostRedirectUris = updatedClientEntity!.PostRedirectUris!.ToArray();

      Assert.AreEqual(2, updatedPostRedirectUris.Length);
      Assert.AreEqual(postRedirectUri0, updatedPostRedirectUris[0]);
      Assert.AreEqual(postRedirectUri1, updatedPostRedirectUris[1]);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Delete_Client()
    {
      var clientName = Guid.NewGuid().ToString();

      var creatingClientEntity = new ClientEntity
      {
        Name = clientName,
        DisplayName = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Scopes = new string[] { Guid.NewGuid().ToString() },
        RedirectUris = new string[] { Guid.NewGuid().ToString() },
        PostRedirectUris = new string[] { Guid.NewGuid().ToString() },
      };

      var creatingClientEntityEntry = _dbContext.Add(creatingClientEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      creatingClientEntityEntry.State = EntityState.Detached;

      var createdClientEntity =
        await _dbContext.Set<ClientEntity>()
                        .Where(entity => entity.Name == clientName)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(createdClientEntity);

      _dbContext.Entry(createdClientEntity!).State = EntityState.Deleted;

      await _dbContext.SaveChangesAsync(_cancellationToken);

      var deletedClientEntity =
        await _dbContext.Set<ClientEntity>()
                        .Where(entity => entity.Name == clientName)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNull(deletedClientEntity);
    }
  }
}
