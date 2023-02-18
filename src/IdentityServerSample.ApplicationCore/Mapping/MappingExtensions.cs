// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using IdentityServerSample.ApplicationCore.Mapping;

  /// <summary>Provides a simple API to register application services.</summary>
  public static class MappingExtensions
  {
    /// <summary>Registers the mapping profiles int the DI container.</summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
      services.AddAutoMapper(config =>
      {
        config.AddProfile(new AudienceMappingProfile());
        config.AddProfile(new ClientMappingProfile());
        config.AddProfile(new ScopeMappingProfile());
      });

      return services;
    }
  }
}
