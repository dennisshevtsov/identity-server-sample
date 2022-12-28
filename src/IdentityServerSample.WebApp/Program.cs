// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguredControllers(builder.Configuration);
builder.Services.AddConfiguredIdentityServer(builder.Configuration);
builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                  options.Authority = builder.Configuration["IdentityApi_Url"];
                  options.Audience = builder.Configuration["ApiResource_Name"];
                  options.RequireHttpsMetadata = false;
                });
builder.Services.AddServices();
builder.Services.AddMapping();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.InitializeDatabase();

app.UseSwagger();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityServer();

app.MapControllers();

app.Run();
