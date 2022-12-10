﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using IdentityServerSample.ApplicationCore.Entities;

namespace IdentityServerSample.ApplicationCore.Repositories
{
  /// <summary>Provides a simple API to clients in a database.</summary>
  public interface IClientRepository
  {
    /// <summary>Gets a collection of active clients.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<IEnumerable<ClientEntity>> GetClientAsync(CancellationToken cancellationToken);
  }
}
