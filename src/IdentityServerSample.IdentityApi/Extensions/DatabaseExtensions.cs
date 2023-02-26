// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.AspNetCore.Identity;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Services;
  using IdentityServerSample.ApplicationCore.Defaults;
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Provides methods to extend the API of the <see cref="Microsoft.AspNetCore.Builder.IApplicationBuilder"/>.</summary>
  public static class DatabaseExtensions
  {
    /// <summary>Registers the <see cref="Microsoft.Extensions.DependencyInjection.DatabaseInitializer"/>.</summary>
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

  public sealed class DatabaseInitializer
  {
    private readonly IConfiguration _configuration;
    private readonly UserManager<UserEntity> _userManager;
    private readonly IScopeService _scopeService;

    public DatabaseInitializer(
      IConfiguration configuration,
      UserManager<UserEntity> userManager,
      IScopeService scopeService)
    {
      _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
      _scopeService = scopeService ?? throw new ArgumentNullException(nameof(scopeService));
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      await AddApplicationScopeAsync(cancellationToken);
      await AddTestUserAsync();
    }

    private async Task AddApplicationScopeAsync(CancellationToken cancellationToken)
    {
      var scopeEntity =
        await _scopeService.GetScopeAsync(
          Scope.ApplicationScope.ToScopeIdentity(), cancellationToken);

      if (scopeEntity != null)
      {
        scopeEntity = new ScopeEntity
        {
          ScopeName = Scope.ApplicationScope,
          Description = "Default Application Scope",
          DisplayName = "Default Application Scope",
          Standard = false,
        };

        await _scopeService.AddScopeAsync(scopeEntity, cancellationToken);
      }
    }

    private async Task AddTestUserAsync()
    {
      var testUserEmail = _configuration["TestUser_Email"];
      var testUserName = _configuration["TestUser_Name"];
      var testUserPasword = _configuration["TestUser_Password"];

      UserEntity? testUserEntity = null;

      if (!string.IsNullOrWhiteSpace(testUserEmail) &&
          !string.IsNullOrWhiteSpace(testUserPasword) &&
          (testUserEntity = await _userManager.FindByNameAsync(testUserEmail)) == null)
      {
        testUserEntity = new UserEntity
        {
          Email = testUserEmail,
          Name = testUserName,
          Scopes = new List<UserScopeEntity>
          {
            new UserScopeEntity
            {
              ScopeName = Scope.ApplicationScope,
            },
          },
        };

        await _userManager.CreateAsync(testUserEntity, testUserPasword);
      }
    }
  }
}
