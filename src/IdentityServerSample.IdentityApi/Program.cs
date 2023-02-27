﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDatabaseInitializer();

builder.Services.SetUpAntiforgery();
builder.Services.SetUpAspNetIdentity();
builder.Services.SetUpIdentityServer(builder.Configuration);
builder.Services.SetUpDatabase(builder.Configuration);
builder.Services.SetUpMapping();
builder.Services.SetUpServices();

var app = builder.Build();

app.InitializeDatabase();

app.UseSwagger();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseRouting();
app.UseIdentityServer();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
