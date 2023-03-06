// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Repositories
{
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.ClientEntity"/> class.</summary>
  public interface IClientRepository
  {
    /// <summary>Adds a new client.</summary>
    /// <param name="clientEntity">An object that represents details of a client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation.</returns>
    public Task AddClientAsync(ClientEntity clientEntity, CancellationToken cancellationToken);

    /// <summary>Gets a client by its name.</summary>
    /// <param name="identity">An object that represents an identity of a client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<ClientEntity?> GetClientAsync(IClientIdentity identity, CancellationToken cancellationToken);

    /// <summary>Gets a client by its name.</summary>
    /// <param name="name">An object that represents a name of a client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<ClientEntity?> GetClientAsync(string name, CancellationToken cancellationToken);

    /// <summary>Gets a collection of active clients.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<ClientEntity[]> GetClientsAsync(CancellationToken cancellationToken);

    /// <summary>Gets a first client with a defined origin.</summary>
    /// <param name="origin">An object that represents an origin.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<ClientEntity?> GetFirstClientWithOriginAsync(string origin, CancellationToken cancellationToken);
  }
}
