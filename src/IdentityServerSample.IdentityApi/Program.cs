// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using IdentityServer4.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
                .AddInMemoryApiScopes(new[]
                {
                  new ApiScope(
                    builder.Configuration["ApiScope_Name"],
                    builder.Configuration["ApiScope_DisplayName"]),
                })
                .AddInMemoryClients(new[]
                {
                  new Client
                  {
                    ClientId = builder.Configuration["Client_Id_0"],
                    ClientName = builder.Configuration["Client_Name_0"],
                    ClientSecrets =
                    {
                      new Secret(builder.Configuration["Client_Secret_0"].Sha256()),
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                      builder.Configuration["ApiScope_Name"],
                    },
                  },
                  new Client
                  {
                    ClientId = builder.Configuration["Client_Id_1"],
                    ClientName = builder.Configuration["Client_Name_1"],
                    ClientSecrets =
                    {
                      new Secret(builder.Configuration["Client_Secret_1"].Sha256()),
                    },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes =
                    {
                      builder.Configuration["ApiScope_Name"],
                    },
                  },
                })
                .AddInMemoryApiResources(new[]
                {
                  new ApiResource
                  {
                    Name = builder.Configuration["ApiResource_Name"],
                    DisplayName = builder.Configuration["ApiResource_DisplayName"],
                    Scopes =
                    {
                      builder.Configuration["ApiScope_Name"],
                    },
                  },
                })
                .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseIdentityServer();
app.Run();
