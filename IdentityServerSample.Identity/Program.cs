// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using IdentityServer4.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
                .AddInMemoryApiScopes(new[]
                {
                  new ApiScope("identity-server-sample-api-scope", "Identity Sample API Scope"),
                })
                .AddInMemoryClients(new[]
                {
                  new Client
                  {
                    ClientId = "identity-server-sample-api-client-id",
                    ClientName = "Identity Sample API Client ID",
                    ClientSecrets =
                    {
                      new Secret("identity-server-sample-api-client-secret".Sha256()),
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                      "identity-server-sample-api-scope",
                    },
                  },
                })
                .AddInMemoryApiResources(new[]
                {
                  new ApiResource
                  {
                    Name = "identity-server-sample-api",
                    Scopes =
                    {
                      "identity-server-sample-api-scope",
                    },
                    
                  },
                })
                .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseIdentityServer();
app.Run();
