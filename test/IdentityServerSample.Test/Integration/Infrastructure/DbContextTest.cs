﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Test
{
  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.ApplicationCore.Entities;

  [TestClass]
  public sealed class DbContextTest : DbIntegrationTestBase
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

      var creatingScopeEntityEntry = DbContext.Add(creatingScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var createdScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == scopeName)
                        .FirstOrDefaultAsync(CancellationToken);

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

      var creatingScopeEntityEntry = DbContext.Add(creatingScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var updatingScopeDisplayName = Guid.NewGuid().ToString();
      var updatingScopeDescription = Guid.NewGuid().ToString();

      var updatingScopeEntity = new ScopeEntity
      {
        Name = scopeName,
        DisplayName = updatingScopeDisplayName,
        Description = updatingScopeDescription,
      };

      var updatingScopeEntityEntry = DbContext.Attach(updatingScopeEntity);

      updatingScopeEntityEntry.State = EntityState.Modified;

      await DbContext.SaveChangesAsync(CancellationToken);

      updatingScopeEntityEntry.State = EntityState.Detached;

      var updatedScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == scopeName)
                        .FirstOrDefaultAsync(CancellationToken);

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

      var creatingScopeEntityEntry = DbContext.Add(creatingScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var createdScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == scopeName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdScopeEntity);

      DbContext.Entry(createdScopeEntity!).State = EntityState.Deleted;

      await DbContext.SaveChangesAsync(CancellationToken);

      var deletedScopeEntity =
        await DbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == scopeName)
                        .FirstOrDefaultAsync(CancellationToken);

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

      var creatingClientEntityEntry = DbContext.Add(creatingAudienceEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingClientEntityEntry.State = EntityState.Detached;

      var createdClientEntity =
        await DbContext.Set<ClientEntity>()
                        .Where(entity => entity.Name == clientName)
                        .FirstOrDefaultAsync(CancellationToken);

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

      var creatingClientEntityEntry = DbContext.Add(creatingAudienceEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

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

      var updatingClientEntityEntry = DbContext.Attach(updatingClientEntity);

      updatingClientEntityEntry.State = EntityState.Modified;

      await DbContext.SaveChangesAsync(CancellationToken);

      updatingClientEntityEntry.State = EntityState.Detached;

      var updatedClientEntity =
        await DbContext.Set<ClientEntity>()
                        .Where(entity => entity.Name == clientName)
                        .FirstOrDefaultAsync(CancellationToken);

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

      var creatingClientEntityEntry = DbContext.Add(creatingClientEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingClientEntityEntry.State = EntityState.Detached;

      var createdClientEntity =
        await DbContext.Set<ClientEntity>()
                        .Where(entity => entity.Name == clientName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdClientEntity);

      DbContext.Entry(createdClientEntity!).State = EntityState.Deleted;

      await DbContext.SaveChangesAsync(CancellationToken);

      var deletedClientEntity =
        await DbContext.Set<ClientEntity>()
                        .Where(entity => entity.Name == clientName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNull(deletedClientEntity);
    }
  }
}
