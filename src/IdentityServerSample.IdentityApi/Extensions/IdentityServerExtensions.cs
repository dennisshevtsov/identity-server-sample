// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  /// <summary>Provides a simple API to configure a pipeline.</summary>
  public static class IdentityServerExtensions
  {
    /// <summary>Sets up the Identity Server.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">An object that represents a set of key/value application configuration properties.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpIdentityServer(
      this IServiceCollection services, IConfiguration configuration)
    {
      services.SetUpIdentityServer(options =>
              {
                options.UserInteraction.ErrorUrl = $"{configuration["IdentityApp_Url"]}/error";
                options.UserInteraction.ErrorIdParameter = "errorId";

                options.UserInteraction.LoginUrl = $"{configuration["IdentityApp_Url"]}/signin";
                options.UserInteraction.LoginReturnUrlParameter = "returnUrl";

                options.UserInteraction.LogoutUrl = "/api/account/signout";
                options.UserInteraction.LogoutIdParameter = "signoutId";
              });

      return services;
    }
  }
}
