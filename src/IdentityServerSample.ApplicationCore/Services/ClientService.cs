// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using System;
  
  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to execute queries and commands with clients.</summary>
  public sealed class ClientService : IClientService
  {
    private readonly IClientRepository _clientRepository;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.ApplicationCore.Services.ClientService"/> class.</summary>
    /// <param name="clientRepository">An object provides a simple API to clients in a database.</param>
    public ClientService(IClientRepository clientRepository)
    {
      _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
    }

    /// <summary>Gets clients that satisfied defined conditions.</summary>
    /// <param name="query">An object that represents conditions to query clients.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<GetClientsResponseDto> GetClientsAsync(GetClientsRequestDto query, CancellationToken cancellationToken)
    {
      return Task.FromResult(new GetClientsResponseDto());
    }
  }
}
