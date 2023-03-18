// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Repositories
{
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceScopeEntity"/> class.</summary>
  public interface IAudienceScopeRepository
  {
    /// <summary>Adds scopes for an audience.</summary>
    /// <param name="audienceScopeEntityCollection">An object that represents a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceScopeEntity"/>.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task AddAudienceScopesAsync(List<AudienceScopeEntity> audienceScopeEntityCollection, CancellationToken cancellationToken);

    /// <summary>Gets a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceScopeEntity"/> for the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/>.</summary>
    /// <param name="identity">An object that represents an identity of an audience.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceScopeEntity>> GetAudienceScopesAsync(
      IAudienceIdentity identity, CancellationToken cancellationToken);

    /// <summary>Gets a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceScopeEntity"/> that relate to defined scopes.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceScopeEntity>> GetAudienceScopesAsync(CancellationToken cancellationToken);

    /// <summary>Gets a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceScopeEntity"/> that relate to defined scopes.</summary>
    /// <param name="identities">An object that represents a collection of the <see cref="IdentityServerSample.ApplicationCore.Identities.IScopeIdentity"/>.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceScopeEntity>> GetAudienceScopesAsync(
      IEnumerable<IScopeIdentity> identities, CancellationToken cancellationToken);

    /// <summary>Gets a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceScopeEntity"/> for defined audiences.</summary>
    /// <param name="identities">An object that represents a collection of the <see cref="IdentityServerSample.ApplicationCore.Identities.IAudienceIdentity"/>.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceScopeEntity>> GetAudienceScopesAsync(
      IEnumerable<IAudienceIdentity> identities, CancellationToken cancellationToken);
  }
}
