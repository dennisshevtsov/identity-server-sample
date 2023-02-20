// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using IdentityServerSample.ApplicationCore.Services;

  /// <summary>Provides a simple API to register application services.</summary>
  public static class ServicesExtensions
  {
    /// <summary>Registers application services.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpServices(this IServiceCollection services)
    {
      services.AddScoped<IAudienceService, AudienceService>();
      services.AddScoped<IClientService, ClientService>();
      services.AddScoped<IScopeService, ScopeService>();
      services.AddScoped<IUserService, UserService>();

      return services;
    }
  }
}
