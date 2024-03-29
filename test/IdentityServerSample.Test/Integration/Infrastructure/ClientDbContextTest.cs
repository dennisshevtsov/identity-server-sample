﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Test
{
  using Microsoft.EntityFrameworkCore;

  [TestClass]
  public sealed class ClientDbContextTest : DbIntegrationTestBase
  {
    [TestMethod]
    public async Task SaveChangesAsync_Should_Create_Client()
    {
      var clientName = Guid.NewGuid().ToString();
      var creatingCleintDisplayName = Guid.NewGuid().ToString();
      var creatingClientDesciption = Guid.NewGuid().ToString();
      var creatingScopeName = Guid.NewGuid().ToString();
      var creatingRedirectUri = Guid.NewGuid().ToString();
      var creatingPostRedirectUri = Guid.NewGuid().ToString();
      var creatingCorsOriginUri = Guid.NewGuid().ToString();

      var creatingAudienceEntity = new ClientEntity
      {
        ClientName = clientName,
        DisplayName = creatingCleintDisplayName,
        Description = creatingClientDesciption,
        Scopes = new[] { creatingScopeName },
        RedirectUris = new[] { creatingRedirectUri },
        PostRedirectUris = new[] { creatingPostRedirectUri },
        CorsOrigins = new[] { creatingCorsOriginUri },
      };

      var creatingClientEntityEntry = DbContext.Add(creatingAudienceEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingClientEntityEntry.State = EntityState.Detached;

      var createdClientEntity =
        await DbContext.Set<ClientEntity>()
                        .Where(entity => entity.ClientName == clientName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdClientEntity);

      Assert.AreEqual(clientName, createdClientEntity!.ClientName);
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

      Assert.IsNotNull(createdClientEntity!.CorsOrigins);
      Assert.AreEqual(1, createdClientEntity!.CorsOrigins!.Count());
      Assert.AreEqual(creatingCorsOriginUri, createdClientEntity!.CorsOrigins!.First());
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Update_Client()
    {
      var clientName = Guid.NewGuid().ToString();
      var scopeName0 = Guid.NewGuid().ToString();
      var redirectUri0 = Guid.NewGuid().ToString();
      var postRedirectUri0 = Guid.NewGuid().ToString();
      var corsOriginUri0 = Guid.NewGuid().ToString();

      var creatingAudienceEntity = new ClientEntity
      {
        ClientName = clientName,
        DisplayName = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Scopes = new[] { scopeName0 },
        RedirectUris = new[] { redirectUri0 },
        PostRedirectUris = new[] { postRedirectUri0 },
        CorsOrigins = new[] { corsOriginUri0 },
      };

      var creatingClientEntityEntry = DbContext.Add(creatingAudienceEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      creatingClientEntityEntry.State = EntityState.Detached;

      var updatingCleintDisplayName = Guid.NewGuid().ToString();
      var updatingClientDesciption = Guid.NewGuid().ToString();
      var scopeName1 = Guid.NewGuid().ToString();
      var redirectUri1 = Guid.NewGuid().ToString();
      var postRedirectUri1 = Guid.NewGuid().ToString();
      var corsOriginUri1 = Guid.NewGuid().ToString();

      var updatingClientEntity = new ClientEntity
      {
        ClientName = clientName,
        DisplayName = updatingCleintDisplayName,
        Description = updatingClientDesciption,
        Scopes = new[] { scopeName0, scopeName1 },
        PostRedirectUris = new[] { postRedirectUri0, postRedirectUri1 },
        RedirectUris = new[] { redirectUri0, redirectUri1 },
        CorsOrigins = new[] { corsOriginUri0, corsOriginUri1 },
      };

      var updatingClientEntityEntry = DbContext.Attach(updatingClientEntity);

      updatingClientEntityEntry.State = EntityState.Modified;

      await DbContext.SaveChangesAsync(CancellationToken);

      updatingClientEntityEntry.State = EntityState.Detached;

      var updatedClientEntity =
        await DbContext.Set<ClientEntity>()
                        .Where(entity => entity.ClientName == clientName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(updatedClientEntity);
      Assert.AreEqual(clientName, updatedClientEntity!.ClientName);
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

      var updatedCorsOrigins = updatedClientEntity!.CorsOrigins!.ToArray();

      Assert.AreEqual(2, updatedCorsOrigins.Length);
      Assert.AreEqual(corsOriginUri0, updatedCorsOrigins[0]);
      Assert.AreEqual(corsOriginUri1, updatedCorsOrigins[1]);
    }

    [TestMethod]
    public async Task SaveChangesAsync_Should_Delete_Client()
    {
      var clientName = Guid.NewGuid().ToString();

      var creatingClientEntity = new ClientEntity
      {
        ClientName = clientName,
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
                        .Where(entity => entity.ClientName == clientName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(createdClientEntity);

      DbContext.Entry(createdClientEntity!).State = EntityState.Deleted;

      await DbContext.SaveChangesAsync(CancellationToken);

      var deletedClientEntity =
        await DbContext.Set<ClientEntity>()
                        .Where(entity => entity.ClientName == clientName)
                        .FirstOrDefaultAsync(CancellationToken);

      Assert.IsNull(deletedClientEntity);
    }
  }
}
