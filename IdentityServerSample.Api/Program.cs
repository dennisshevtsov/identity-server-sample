// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options => options.Authority = builder.Configuration.GetValue<string>("Authority"));

var app = builder.Build();

app.UseAuthentication();
app.MapControllers();
app.Run();
