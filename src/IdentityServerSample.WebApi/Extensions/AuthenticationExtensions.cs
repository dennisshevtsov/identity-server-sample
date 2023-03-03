// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.AspNetCore.Authentication.JwtBearer;

  using IdentityServerSample.ApplicationCore.Defaults;

  /// <summary>Provides a simple API to configure a pipeline.</summary>
  public static class AuthenticationExtensions
  {
    /// <summary>Adds the authentication middleware to a pipeline.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">An object that represents a set of key/value application configuration properties.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpAuthentication(
      this IServiceCollection services,
      IConfiguration configuration)
    {
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                options.Authority = configuration["IdentityApi_Url"];
                options.Audience = Audiences.ApplicationAudience;
                options.RequireHttpsMetadata = false;
              });

      return services;
    }
  }
}
