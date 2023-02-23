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

      var audiences = dbContext.Set<AudienceEntity>();

      if (audiences.FirstOrDefault(entity => entity.AudienceName == "identity-server-sample-api") == null)
      {
        var audienceEntity = new AudienceEntity
        {
          AudienceName = "identity-server-sample-api",
          DisplayName = "Identity Server Sample API",
          Scopes = new List<string>
          {
            "identity-server-sample-api-scope"
          },
        };

        audiences.Add(audienceEntity);
      }

      var clients = dbContext.Set<ClientEntity>();

      if (clients.FirstOrDefault(entity => entity.ClientName == "identity-server-sample-api-client-id-1") == null)
      {
        var clientEntity = new ClientEntity
        {
          ClientName = "identity-server-sample-api-client-id-1",
          DisplayName = "Identity Sample API Client ID for Code Flow",
          Description = "Default client",
          Scopes = new List<string>
          {
            "openid",
            "profile",
            "identity-server-sample-api-scope",
          },
          RedirectUris = new List<string>
          {
            "http://localhost:4202/signin-callback",
            "http://localhost:4202/silent-callback",
          },
          PostRedirectUris = new List<string>
          {
            "http://localhost:4202",
          },
        };

        clients.Add(clientEntity);
      }

      var scopes = dbContext.Set<ScopeEntity>();

      if (scopes.FirstOrDefault(entity => entity.ScopeName == "identity-server-sample-api-scope") == null)
      {
        var scopeEntity = new ScopeEntity
        {
          ScopeName = "identity-server-sample-api-scope",
          DisplayName = "Identity Sample API Scope",
          Description = "Default scope"
        };

        scopes.Add(scopeEntity);
      }

      dbContext.SaveChanges();
    }
  }
}
