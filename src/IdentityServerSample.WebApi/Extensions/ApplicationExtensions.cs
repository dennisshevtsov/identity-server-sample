// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to configure an application.</summary>
  public static class ApplicationExtensions
  {
    /// <summary>Initializes a database.</summary>
    /// <param name="app">An object that defines a class that provides the mechanisms to configure an application's request pipeline.</param>
    public static void InitializeDatabase(this IApplicationBuilder app)
    {
      using (var scope = app.ApplicationServices.CreateScope())
      {
        ApplicationExtensions.InitializeDatabase(
          scope.ServiceProvider.GetRequiredService<DbContext>());
      }
    }

    private static void InitializeDatabase(DbContext dbContext)
    {
      dbContext.Database.EnsureCreated();

      var clients = dbContext.Set<ClientEntity>();

      if (clients.FirstOrDefault(entity => entity.Name == "identity-server-sample-api-client-id-1") == null)
      {
        var clientEntity = new ClientEntity
        {
          Name = "identity-server-sample-api-client-id-1",
          DisplayName = "Identity Sample API Client ID for Code Flow",
          Description = "Default client",
          Scopes = new List<LiteralEmbeddedEntity>
          {
            "openid",
            "profile",
            "identity-server-sample-api-scope",
          },
          RedirectUris = new List<LiteralEmbeddedEntity>
          {
            "http://localhost:4202/signin-callback",
            "http://localhost:4202/silent-callback",
          },
          PostRedirectUris = new List<LiteralEmbeddedEntity>
          {
            "http://localhost:4202",
          },
        };

        clients.Add(clientEntity);
      }

      var scopes = dbContext.Set<ScopeEntity>();

      if (scopes.FirstOrDefault(entity => entity.Name == "identity-server-sample-api-scope") == null)
      {
        var scopeEntity = new ScopeEntity
        {
          Name = "identity-server-sample-api-scope",
          DisplayName = "Identity Sample API Scope",
          Description = "Default scope"
        };

        scopes.Add(scopeEntity);
      }

      dbContext.SaveChanges();
    }
  }
}
