// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
                .AddInMemoryApiScopes(builder.Configuration.GetSection("Scopes"))
                .AddInMemoryClients(builder.Configuration.GetSection("Clients"));

var app = builder.Build();

app.UseIdentityServer();
app.Run();
