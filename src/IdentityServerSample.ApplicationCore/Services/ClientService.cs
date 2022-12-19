// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using System;

  using AutoMapper;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to execute queries and commands with clients.</summary>
  public sealed class ClientService : IClientService
  {
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.ApplicationCore.Services.ClientService"/> class.</summary>
    /// <param name="clientRepository">An object provides a simple API to clients in a database.</param>
    /// <param name="mapper">An object that provides a simple API to map objects of different types.</param>
    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
      _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

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
  }
}
