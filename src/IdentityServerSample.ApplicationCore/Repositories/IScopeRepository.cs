// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Repositories
{
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.ScopeEntity"/> class.</summary>
  public interface IScopeRepository
  {
    /// <summary>Creates a new scope.</summary>
    /// <param name="scopeEntity">An object that represents details of a scope.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task AddScopeAsync(ScopeEntity scopeEntity, CancellationToken cancellationToken);

    /// <summary>Gets a scope by its identity.</summary>
    /// <param name="identity">An object that represents an identity of a scope.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<ScopeEntity> GetScopeAsync(IScopeIdentity identity, CancellationToken cancellationToken);

    /// <summary>Gets a collection of scopes that satisfy defined conditions.</summary>
    /// <param name="identities">An object that represents a collection the <see cref="IdentityServerSample.ApplicationCore.Identities.IScopeIdentity"/>.</param>
    /// <param name="standard">An object that indicates if scopes are standard.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<ScopeEntity>> GetScopesAsync(
      IEnumerable<IScopeIdentity>? identities, bool standard, CancellationToken cancellationToken);
  }
}
