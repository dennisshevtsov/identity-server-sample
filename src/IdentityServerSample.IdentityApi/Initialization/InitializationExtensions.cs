// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using IdentityServerSample.IdentityApi.Initialization;

  /// <summary>Provides methods to initialize the database.</summary>
  public static class InitializationExtensions
  {
    /// <summary>Registers the <see cref="IdentityServerSample.IdentityApi.Initialization.DatabaseInitializer"/>.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection AddDatabaseInitializer(this IServiceCollection services)
    {
      services.AddScoped<DatabaseInitializer>();

      return services;
    }

    /// <summary>Sets up the database with all required data.</summary>
    /// <param name="app">An object that defines a class that provides the mechanisms to configure an application's request pipeline.</param>
    /// <returns>An object that defines a class that provides the mechanisms to configure an application's request pipeline.</returns>
    public static IApplicationBuilder SetUpDatabase(this IApplicationBuilder app)
    {
      using (var scope = app.ApplicationServices.CreateScope())
      {
        scope.ServiceProvider.GetRequiredService<DatabaseInitializer>()
                             .ExecuteAsync(CancellationToken.None)
                             .GetAwaiter()
                             .GetResult();
      }

      return app;
    }
  }
}
