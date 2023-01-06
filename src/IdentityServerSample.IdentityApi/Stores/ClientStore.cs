// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Stores
{
  using IdentityServer4;
  using IdentityServer4.Models;
  using IdentityServer4.Stores;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to retrieve a client.</summary>
  public sealed class ClientStore : IClientStore
  {
    private IDictionary<string, Client>? _clients;

    private readonly IConfiguration _configuration;
    private readonly IClientRepository _clientRepository;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.IdentityApp.Stores.ClientStore"/> class.</summary>
    /// <param name="configuration"></param>
    /// <param name="clientRepository">An object that provides a simple API to clients in a database.</param>
    public ClientStore(IConfiguration configuration, IClientRepository clientRepository)
    {
      _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
      _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(configuration));
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
                RedirectUris =
                {
                  $"{_configuration["WebApp_Url"]}/signin-callback",
                  $"{_configuration["WebApp_Url"]}/silent-callback",
                },
                PostLogoutRedirectUris =
                {
                  _configuration["WebApp_Url"],
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
    public async Task<Client?> FindClientByIdAsync(string clientId)
    {
      var clientEntity = await _clientRepository.GetClientAsync(
        clientId, CancellationToken.None);

      Client? client = null;

      if (clientEntity != null)
      {
        client = new Client
        {
          ClientId = clientEntity.Name,
          ClientName = clientEntity.DisplayName,
          RequireClientSecret = false,
          AllowedGrantTypes = GrantTypes.Code,
          AllowedScopes = ClientStore.ToCollection(clientEntity.Scopes),
          RedirectUris = ClientStore.ToCollection(clientEntity.RedirectUris),
          PostLogoutRedirectUris = ClientStore.ToCollection(clientEntity.PostRedirectUris),
        };
      }

      return client;
    }

    private static ICollection<string> ToCollection(
      IEnumerable<LiteralEmbeddedEntity>? entities)
    {
      if (entities == null)
      {
        return new List<string>();
      }

      return entities.Select(entity => entity.Value!)
                     .ToList();
    }
  }
}
