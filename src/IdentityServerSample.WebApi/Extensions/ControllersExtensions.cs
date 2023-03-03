// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc.Authorization;

  using IdentityServerSample.ApplicationCore.Defaults;

  /// <summary>Provides a simple API to configure a pipeline.</summary>
  public static class ControllersExtensions
  {
    /// <summary>Adds the controller middleware to a pipeline.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpControllers(this IServiceCollection services)
    {
      services.AddControllers(options =>
              {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                             .RequireClaim("scope", Scopes.ApplicationScope)
                                                             .Build();
                var filter = new AuthorizeFilter(policy);

                options.Filters.Add(filter);
              });

      return services;
    }
  }
}
