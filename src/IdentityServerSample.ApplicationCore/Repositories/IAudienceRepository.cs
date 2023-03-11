// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Repositories
{
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/> class.</summary>
  public interface IAudienceRepository
  {
    /// <summary>Gets a collection of all audiences.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceEntity>> GetAudiencesAsync(CancellationToken cancellationToken);

    /// <summary>Gets a collection of audiences by audience names.</summary>
    /// <param name="identities">An object that represents a collection of the <see cref="IdentityServerSample.ApplicationCore.Identities.IAudienceIdentity"/>.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceEntity>> GetAudiencesAsync(
      IEnumerable<IAudienceIdentity>? identities, CancellationToken cancellationToken);

    /// <summary>Gets an instance of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/> with a defined name.</summary>
    /// <param name="identity">An object that represents an identity of an audience.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<AudienceEntity?> GetAudienceAsync(IAudienceIdentity identity, CancellationToken cancellationToken);

    /// <summary>Adds a new audience.</summary>
    /// <param name="audienceEntity">An object that represents details of an audience.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation.</returns>
    public Task AddAudienceAsync(AudienceEntity audienceEntity, CancellationToken cancellationToken);
  }
}
