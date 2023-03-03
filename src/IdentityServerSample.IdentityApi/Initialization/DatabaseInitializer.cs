// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.Initialization
{
  using Microsoft.AspNetCore.Identity;

  using IdentityServerSample.ApplicationCore.Defaults;
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;
  using IdentityServerSample.ApplicationCore.Services;

  /// <summary>Provides a simple API to initialize the database.</summary>
  public sealed class DatabaseInitializer
  {
    private readonly IConfiguration _configuration;
    private readonly UserManager<UserEntity> _userManager;
    private readonly IScopeService _scopeService;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.IdentityApi.Initialization.DatabaseInitializer"/> class.</summary>
    /// <param name="configuration">An object that represents a set of key/value application configuration properties.</param>
    /// <param name="userManager">An object that provides the APIs for managing user in a persistence store.</param>
    /// <param name="scopeService">An object that provides a simple API to query and save scopes.</param>
    public DatabaseInitializer(
      IConfiguration configuration,
      UserManager<UserEntity> userManager,
      IScopeService scopeService)
    {
      _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
      _scopeService = scopeService ?? throw new ArgumentNullException(nameof(scopeService));
    }

    /// <summary>Initializes the database.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      await AddApplicationScopeAsync(cancellationToken);
      await AddTestUserAsync();
    }

    private async Task AddApplicationScopeAsync(CancellationToken cancellationToken)
    {
      var scopeEntity =
        await _scopeService.GetScopeAsync(
          Scopes.ApplicationScope.ToScopeIdentity(), cancellationToken);

      if (scopeEntity == null)
      {
        scopeEntity = new ScopeEntity
        {
          ScopeName = Scopes.ApplicationScope,
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
        testUserEntity = DatabaseInitializer.CreateDefaultUser(testUserEmail, testUserName);

        await _userManager.CreateAsync(testUserEntity, testUserPasword);
      }
    }

    private static UserEntity CreateDefaultUser(string email, string? name) => new UserEntity
    {
      Email = email,
      Name = name,
      Scopes = new List<UserScopeEntity>
      {
        new UserScopeEntity
        {
          ScopeName = Scopes.ApplicationScope,
        },
      },
    };
  }
}
