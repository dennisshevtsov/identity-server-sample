// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc.Authorization;
  using Microsoft.AspNetCore.Mvc.Razor;

  /// <summary>Provides a simple API to configure a pipeline.</summary>
  public static class ControllersExtensions
  {
    /// <summary>Adds the controller middleware to a pipeline.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">An object that represents a set of key/value application configuration properties.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection AddConfiguredControllers(
      this IServiceCollection services,
      IConfiguration configuration)
    {
      services.AddControllersWithViews(options =>
              {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                             .RequireClaim("scope", configuration["ApiScope_Name"]!)
                                                             .Build();
                var filter = new AuthorizeFilter(policy);

                options.Filters.Add(filter);
              })
              .AddViewOptions(options =>
              {
                options.HtmlHelperOptions.ClientValidationEnabled = false;
              });
      services.Configure<RazorViewEngineOptions>(options =>
              {
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/Views/{0}" + RazorViewEngine.ViewExtension);
              });

      return services;
    }
  }
}
