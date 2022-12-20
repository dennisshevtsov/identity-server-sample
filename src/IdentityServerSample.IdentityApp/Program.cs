// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguredControllers();
builder.Services.AddConfiguredIdentityServer(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddMapping();
builder.Services.AddDatabase(builder.Configuration);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.InitializeDatabase();
app.UseIdentityServer();

app.MapControllers();

app.Run();
