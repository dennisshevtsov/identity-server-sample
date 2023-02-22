// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using IdentityServer4.Configuration;

  using IdentityServerSample.IdentityServer.Services;
  using IdentityServerSample.IdentityServer.Stores;

  /// <summary>Provides a simple API to configure a pipeline.</summary>
  public static class IdentityServerExtensions
  {
    /// <summary>Sets up the Identity Server.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <param name="setupAction">An object that represents a action to configure the Identity Server.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpIdentityServer(
      this IServiceCollection services, Action<IdentityServerOptions> setupAction)
    {
      services.AddIdentityServer(setupAction)
              .AddClientStore<ClientStore>()
              .AddResourceStore<ResourceStore>()
              .AddCorsPolicyService<CorsPolicyService>()
              .AddProfileService<ProfileService>()
              .AddDeveloperSigningCredential();

      return services;
    }
  }
}
