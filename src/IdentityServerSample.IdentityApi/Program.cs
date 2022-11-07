// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((context, builder) =>
            {
              var sourceStartIndex =
                context.HostingEnvironment.ContentRootPath.Length -
                context.HostingEnvironment.ApplicationName.Length -
                "src\\".Length - 1;
              var rootPath = context.HostingEnvironment.ContentRootPath.Remove(sourceStartIndex)
                                                                         .ToString();
              var contentRootPath = rootPath + "common\\appsettings.json";

              builder.AddJsonFile(contentRootPath);
            });

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
                    ClientId = builder.Configuration["Client_Id"],
                    ClientName = builder.Configuration["Client_Name"],
                    ClientSecrets =
                    {
                      new Secret(builder.Configuration["Client_Secret"].Sha256()),
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
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
