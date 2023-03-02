// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using AutoMapper;
  using IdentityServerSample.IdentityApi.Mapping;

  /// <summary>Provides methods to extend the API of the <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.</summary>
  public static class MappingExtensions
  {
    /// <summary>Registers the mapping profiles int the DI container.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpIdentityServerMapping(this IServiceCollection services)
    {
      //services.AddAutoMapper(config =>
      //{
      //  config.AddProfile(new AudienceMappingProfile());
      //  config.AddProfile(new ClientMappingProfile());
      //  config.AddProfile(new ScopeMappingProfile());
      //});

      services.Configure<MapperConfigurationExpression>(options =>
      {
        options.AddProfile(new AudienceMappingProfile());
        options.AddProfile(new ClientMappingProfile());
        options.AddProfile(new ScopeMappingProfile());
      });

      services.AddAutoMapper(_ => { });

      return services;
    }
  }
}
