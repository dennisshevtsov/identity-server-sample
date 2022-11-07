// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

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

builder.Services.AddControllers(options =>
                {
                  var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
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

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
