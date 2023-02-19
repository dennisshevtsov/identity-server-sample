// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.SetUpControllers(builder.Configuration);
builder.Services.SetUpAuthentication(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddMapping();
builder.Services.SetUpDatabase(builder.Configuration);

var app = builder.Build();

app.InitializeDatabase();

app.UseSwagger();
app.UseStaticFiles();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
