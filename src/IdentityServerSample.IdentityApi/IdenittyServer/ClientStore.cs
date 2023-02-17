// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.IdenittyServer
{
  using IdentityServer4.Models;
  using IdentityServer4.Stores;

  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to retrieve a client.</summary>
  public sealed class ClientStore : IClientStore
  {
    private readonly IClientRepository _clientRepository;

    /// <summary>Initializes a new instance of the <see cref="ClientStore"/> class.</summary>
    /// <param name="clientRepository">An object that provides a simple API to clients in a database.</param>
    public ClientStore(IClientRepository clientRepository)
    {
      _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
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
          AllowedScopes = clientEntity.Scopes?.ToArray(),
          RedirectUris = clientEntity.RedirectUris?.ToArray(),
          PostLogoutRedirectUris = clientEntity.PostRedirectUris?.ToArray(),
        };
      }

      return client;
    }
  }
}
