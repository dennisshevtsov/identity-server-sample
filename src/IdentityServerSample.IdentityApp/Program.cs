// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using IdentityServerSample.IdentityApp.Filters;
using Microsoft.AspNetCore.Antiforgery;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddConfiguredIdentityServer(builder.Configuration);
builder.Services.AddSingleton<ValidateAntiforgeryTokenAuthorizationFilter>();
builder.Services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
var app = builder.Build();

app.Use((context, next) =>
{
  var tokens = context.RequestServices.GetRequiredService<IAntiforgery>().GetAndStoreTokens(context);
  context.Response.Cookies.Append("X-XSRF-TOKEN", tokens.RequestToken!,
          new CookieOptions { HttpOnly = false });

  return next.Invoke();
});

app.UseSwagger();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
