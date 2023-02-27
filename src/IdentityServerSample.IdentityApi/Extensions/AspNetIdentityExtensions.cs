// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  /// <summary>Provides a simple API to configure a pipeline.</summary>
  public static class AspNetIdentityExtensions
  {
    /// <summary>Sets up the Identity.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpAspNetIdentity(this IServiceCollection services)
    {
      services.SetUpAspNetIdentity(options =>
              {
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
              });

      return services;
    }
  }
}
