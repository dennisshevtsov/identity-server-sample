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
        testUserEntity = new UserEntity
        {
          Email = testUserEmail,
          Name = testUserName,
          Scopes = new List<UserScopeEntity>
          {
            new UserScopeEntity
            {
              ScopeName = Scopes.ApplicationScope,
            },
          },
        };

        await _userManager.CreateAsync(testUserEntity, testUserPasword);
      }
    }
  }
}
