// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
                {
                  var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                              .RequireClaim("scope", builder.Configuration["ApiScope_Name"]!)
                                                              .Build();
                  var filter = new AuthorizeFilter(policy);

                  options.Filters.Add(filter);
                });
builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                  options.Authority = builder.Configuration["IdentityApi_Url"];
                  options.Audience = builder.Configuration["ApiResource_Name"];
                  options.RequireHttpsMetadata = false;
                });

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
