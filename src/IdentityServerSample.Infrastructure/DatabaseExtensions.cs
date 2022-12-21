// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.Options;

  using IdentityServerSample.ApplicationCore.Repositories;
  using IdentityServerSample.Infrastructure;
  using IdentityServerSample.Infrastructure.Repositories;

  /// <summary>Provides a simple API to register database services.</summary>
  public static class DatabaseExtensions
  {
    /// <summary>Registers database services.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">An object that represents a set of key/value application configuration properties.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection AddDatabase(
      this IServiceCollection services, IConfiguration configuration)
    {
      services.Configure<DatabaseOptions>(configuration);
      services.AddDbContext<DbContext, IdentityServerSampleDbContext>(
        (provider, builder) =>
        {
          var options = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

          if (string.IsNullOrWhiteSpace(options.AccountEndpoint))
          {
            throw new ArgumentException(nameof(options.AccountEndpoint));
          }

          if (string.IsNullOrWhiteSpace(options.AccountKey))
          {
            throw new ArgumentException(nameof(options.AccountKey));
          }

          if (string.IsNullOrWhiteSpace(options.DatabaseName))
          {
            throw new ArgumentException(nameof(options.DatabaseName));
          }

          builder.UseCosmos(options.AccountEndpoint, options.AccountKey, options.DatabaseName);
        });

      services.AddScoped<IAudienceRepository, AudienceRepository>();
      services.AddScoped<IClientRepository, ClientRepository>();
      services.AddScoped<IScopeRepository, ScopeRepository>();

      return services;
    }
  }
}
