﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using IdentityServerSample.ApplicationCore.Dtos;

  /// <summary>Provides a simple API to execute queries and commands with clients.</summary>
  public sealed class ClientService : IClientService
  {
    /// <summary>Gets a client that satisfies defined conditions.</summary>
    /// <param name="query">An object that represents conditions to query a client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<GetClientResponseDto> GetClientAsync(GetClientRequestDto query, CancellationToken cancellationToken)
    {
      return Task.FromResult(new GetClientResponseDto());
    }

    /// <summary>Gets clients that satisfied defined conditions.</summary>
    /// <param name="query">An object that represents conditions to query clients.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<GetClientsResponseDto> GetClientsAsync(GetClientsRequestDto query, CancellationToken cancellationToken)
    {
      return Task.FromResult(new GetClientsResponseDto());
    }

    /// <summary>Creates a client.</summary>
    /// <param name="command">An object that repreesents data to create a client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<CreateClientResponseDto> CreateClientAsync(CreateClientRequestDto command, CancellationToken cancellationToken)
    {
      return Task.FromResult(new CreateClientResponseDto());
    }

    /// <summary>Updates a client.</summary>
    /// <param name="command">An object that represents data to update a client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task UpdateClientAsync(UpdateClientRequestDto command, CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}
