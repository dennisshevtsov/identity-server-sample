// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.IdenittyServer
{
  using AutoMapper;
  using IdentityServer4.Models;
  using IdentityServer4.Stores;

  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to retrieve a client.</summary>
  public sealed class ClientStore : IClientStore
  {
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    /// <summary>Initializes a new instance of the <see cref="ClientStore"/> class.</summary>
    /// <param name="mapper">An object that provides a simple API to map objects of different types.</param>
    /// <param name="clientRepository">An object that provides a simple API to clients in a database.</param>
    public ClientStore(IMapper mapper, IClientRepository clientRepository)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
        client = _mapper.Map<Client>(clientEntity);
      }

      return client;
    }
  }
}
