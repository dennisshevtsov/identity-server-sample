// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Stores
{
  using IdentityServer4;
  using IdentityServer4.Models;
  using IdentityServer4.Stores;

  /// <summary>Provides a simple API to retrieve a client.</summary>
  public sealed class ClientStore : IClientStore
  {
    private IDictionary<string, Client>? _clients;

    private readonly IConfiguration _configuration;

    public ClientStore(IConfiguration configuration)
    {
      _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    private IDictionary<string, Client> Clients
    {
      get
      {
        if (_clients == null)
        {
          _clients = new Dictionary<string, Client>
          {
            {
              _configuration["Client_Id_0"]!,
              new Client
              {
                ClientId = _configuration["Client_Id_0"],
                ClientName = _configuration["Client_Name_0"],
                ClientSecrets =
                {
                  new Secret(_configuration["Client_Secret_0"].Sha256()),
                },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes =
                {
                  _configuration["ApiScope_Name"],
                },
              }
            },
            {
              _configuration["Client_Id_1"]!,
              new Client
              {
                ClientId = _configuration["Client_Id_1"],
                ClientName = _configuration["Client_Name_1"],
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes =
                {
                  IdentityServerConstants.StandardScopes.OpenId,
                  IdentityServerConstants.StandardScopes.Profile,
                  _configuration["ApiScope_Name"],
                },
                AllowedCorsOrigins =
                {
                  "http://localhost:4200",
                },
                RedirectUris =
                {
                  "http://localhost:4200/signin-callback",
                  "http://localhost:4200/silent-callback",
                },
                PostLogoutRedirectUris =
                {
                  "http://localhost:4200",
                },
              }
            },
          };
        }

        return _clients;
      }
    }

    /// <summary>Finds a client by a client ID.</summary>
    /// <param name="clientId">An object that represents an ID of a client.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task<Client?> FindClientByIdAsync(string clientId)
    {
      Client? client = null;

      if (Clients.ContainsKey(clientId))
      {
        client = Clients[clientId];
      }

      return Task.FromResult(client);
    }
  }
}
