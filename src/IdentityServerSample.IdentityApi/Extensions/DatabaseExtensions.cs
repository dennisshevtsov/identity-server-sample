// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.AspNetCore.Identity;

  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Provides methods to extend the API of the <see cref="Microsoft.AspNetCore.Builder.IApplicationBuilder"/>.</summary>
  public static class DatabaseExtensions
  {
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

  public sealed class DatabaseInitializer
  {
    private UserManager<UserEntity> _userManager;

    public DatabaseInitializer(UserManager<UserEntity> userManager)
    {
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      var userEntity = new UserEntity
      {
        Email = "test@test.test",
        Name = "Test User",
      };

      await _userManager.CreateAsync(userEntity, "test");
    }
  }
}
