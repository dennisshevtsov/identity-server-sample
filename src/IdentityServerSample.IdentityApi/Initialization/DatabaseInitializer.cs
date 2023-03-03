// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.Initialization
{
  using IdentityServer4;
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

    private readonly IClientService _clientService;
    private readonly IScopeService _scopeService;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.IdentityApi.Initialization.DatabaseInitializer"/> class.</summary>
    /// <param name="configuration">An object that represents a set of key/value application configuration properties.</param>
    /// <param name="userManager">An object that provides the APIs for managing user in a persistence store.</param>
    /// <param name="clientService">An object that provides a simple API to execute queries and commands with clients.</param>
    /// <param name="scopeService">An object that provides a simple API to query and save scopes.</param>
    public DatabaseInitializer(
      IConfiguration configuration,
      UserManager<UserEntity> userManager,
      IClientService clientService,
      IScopeService scopeService)
    {
      _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

      _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
      _scopeService = scopeService ?? throw new ArgumentNullException(nameof(scopeService));
    }

    /// <summary>Initializes the database.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      await AddApplicationClientAsync(cancellationToken);
      await AddApplicationScopeAsync(cancellationToken);
      await AddTestUserAsync();
    }

    private async Task AddApplicationClientAsync(CancellationToken cancellationToken)
    {
      var webAppUrl = _configuration["WebApp_Url"];
      ClientEntity? clientEntity;

      if (!string.IsNullOrWhiteSpace(webAppUrl) &&
          (clientEntity = await _clientService.GetClientAsync(Clients.ApplicationClient, cancellationToken)) == null)
      {
        clientEntity = CreateDefaultClient(webAppUrl);

        await _clientService.AddClientAsync(clientEntity, cancellationToken);
      }
    }

    private ClientEntity CreateDefaultClient(string webAppUrl) => new ClientEntity
    {
      ClientName = Clients.ApplicationClient,
      Description = "Default client",
      DisplayName = "Identity Sample API Client ID for Code Flow",
      PostRedirectUris = new List<string>
      {
        webAppUrl,
      },
      RedirectUris = new List<string>
      {
        $"{webAppUrl}/signin-callback",
        $"{webAppUrl}/silent-callback",
      },
      Scopes = new List<string>
      {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        Scopes.ApplicationScope,
      },
      CorsOrigins = new List<string>
      {
        webAppUrl,
      },
    };

    private async Task AddApplicationScopeAsync(CancellationToken cancellationToken)
    {
      var scopeEntity =
        await _scopeService.GetScopeAsync(
          Scopes.ApplicationScope.ToScopeIdentity(), cancellationToken);

      if (scopeEntity == null)
      {
        scopeEntity = DatabaseInitializer.CreateDefaultScope();

        await _scopeService.AddScopeAsync(scopeEntity, cancellationToken);
      }
    }

    private static ScopeEntity CreateDefaultScope() => new ScopeEntity
    {
      ScopeName = Scopes.ApplicationScope,
      Description = "Default Application Scope",
      DisplayName = "Default Application Scope",
      Standard = false,
    };

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

    private static UserEntity CreateDefaultUser(string email, string? name) => new()
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
