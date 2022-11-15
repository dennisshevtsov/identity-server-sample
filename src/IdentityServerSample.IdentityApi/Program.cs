// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Security.Claims;

using IdentityServer4.Models;
using IdentityServer4.Test;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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
                    AllowedCorsOrigins =
                    {
                      "http://localhost:4200/",
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
                .AddTestUsers(new List<TestUser>
                {
                  new TestUser
                  {
                    SubjectId = "test",
                    Username = "test",
                    Password = "test",
                    IsActive = true,
                    Claims = {
                      new Claim("scope", builder.Configuration["ApiScope_Name"]!),
                    },
                  },
                })
                .AddDeveloperSigningCredential();

var app = builder.Build();

app.MapDefaultControllerRoute();
app.UseIdentityServer();
app.Run();
