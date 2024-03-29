﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Provides a simple API to execute queries and commands with clients.</summary>
  public interface IClientService
  {
    /// <summary>Adds a new client.</summary>
    /// <param name="clientEntity">An object that represents details of a client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation.</returns>
    public Task AddClientAsync(ClientEntity clientEntity, CancellationToken cancellationToken);

    /// <summary>Adds a new client.</summary>
    /// <param name="requestDto">An object that represents data to create a new client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation.</returns>
    public Task AddClientAsync(AddClientRequestDto requestDto, CancellationToken cancellationToken);

    /// <summary>Gets a client that satisfy defined conditions.</summary>
    /// <param name="identity">An object that represents an identity of a client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<ClientEntity?> GetClientAsync(IClientIdentity identity, CancellationToken cancellationToken);

    /// <summary>Gets a client by its name.</summary>
    /// <param name="clientName">An object that represents a name of a client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<ClientEntity?> GetClientAsync(string clientName, CancellationToken cancellationToken);

    /// <summary>Gets clients that satisfied defined conditions.</summary>
    /// <param name="query">An object that represents conditions to query clients.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<GetClientsResponseDto> GetClientsAsync(GetClientsRequestDto query, CancellationToken cancellationToken);

    /// <summary>Checks if the defined origin is allowed.</summary>
    /// <param name="origin">An object that represents an origin.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<bool> CheckIfOriginIsAllowedAsync(string origin, CancellationToken cancellationToken);
  }
}
