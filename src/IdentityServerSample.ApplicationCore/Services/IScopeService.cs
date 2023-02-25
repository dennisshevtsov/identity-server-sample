// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to query and save scopes.</summary>
  public interface IScopeService
  {
    /// <summary>Gets a collection of available scopes.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<ScopeEntity>> GetScopesAsync(CancellationToken cancellationToken);

    /// <summary>Gets a collection of scopes with names.</summary>
    /// <param name="scopes">An object that represents a collection of scope names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<ScopeEntity>> GetScopesAsync(
      IEnumerable<string> scopes, CancellationToken cancellationToken);

    /// <summary>Gets a collection of standard scopes with names.</summary>
    /// <param name="scopes">An object that represents a collection of scope names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<ScopeEntity>> GetStandardScopesAsync(
      IEnumerable<string> scopes, CancellationToken cancellationToken);
  }
}
