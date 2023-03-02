// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using AutoMapper;

  using IdentityServerSample.ApplicationCore.Mapping;

  /// <summary>Provides a simple API to register application services.</summary>
  public static class MappingExtensions
  {
    /// <summary>Registers the mapping profiles int the DI container.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpApplicationCoreMapping(this IServiceCollection services)
    {
      services.Configure<MapperConfigurationExpression>(options =>
      {
        options.AddProfile(new AudienceMappingProfile());
        options.AddProfile(new ClientMappingProfile());
        options.AddProfile(new ScopeMappingProfile());
      });

      return services;
    }
  }
}
