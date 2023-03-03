// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using System;

  using AutoMapper;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to execute queries and commands with clients.</summary>
  public sealed class ClientService : IClientService
  {
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.ApplicationCore.Services.ClientService"/> class.</summary>
    /// <param name="mapper">An object that provides a simple API to map objects of different types.</param>
    /// <param name="clientRepository">An object provides a simple API to clients in a database.</param>
    public ClientService(IMapper mapper, IClientRepository clientRepository)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
    }

    /// <summary>Adds a new client.</summary>
    /// <param name="clientEntity">An object that represents details of a client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation.</returns>
    public Task AddClientAsync(ClientEntity clientEntity, CancellationToken cancellationToken)
      => _clientRepository.AddClientAsync(clientEntity, cancellationToken);

    /// <summary>Gets a client by its name.</summary>
    /// <param name="clientName">An object that represents a name of a client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<ClientEntity?> GetClientAsync(string clientName, CancellationToken cancellationToken)
      => _clientRepository.GetClientAsync(clientName, cancellationToken);

    /// <summary>Gets clients that satisfied defined conditions.</summary>
    /// <param name="query">An object that represents conditions to query clients.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<GetClientsResponseDto> GetClientsAsync(GetClientsRequestDto query, CancellationToken cancellationToken)
    {
      var clientEntityCollection =
        await _clientRepository.GetClientsAsync(cancellationToken);
      var getClientsResponseDtoCollection =
        _mapper.Map<GetClientsResponseDto>(clientEntityCollection);

      return getClientsResponseDtoCollection;
    }

    /// <summary>Checks if the defined origin is allowed.</summary>
    /// <param name="origin">An object that represents an origin.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<bool> CheckIfOriginIsAllowedAsync(string origin, CancellationToken cancellationToken)
      => await _clientRepository.GetFirstClientWithOriginAsync(origin, cancellationToken) != null;
  }
}
