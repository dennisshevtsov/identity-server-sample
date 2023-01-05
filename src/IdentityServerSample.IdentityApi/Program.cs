// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddConfiguredIdentityServer(builder.Configuration);
builder.Services.AddConfiguredAntiforgery();

var app = builder.Build();

app.UseSwagger();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseRouting();
app.UseIdentityServer();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
