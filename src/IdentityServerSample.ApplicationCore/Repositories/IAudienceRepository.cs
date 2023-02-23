// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Repositories
{
  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/> class.</summary>
  public interface IAudienceRepository
  {
    /// <summary>Gets a collection of all audiences.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceEntity>> GetAudiencesAsync(CancellationToken cancellationToken);

    /// <summary>Gets a collection of audiences by audience names.</summary>
    /// <param name="audiences">An object that represents a collection of audience names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceEntity>> GetAudiencesByNamesAsync(
      IEnumerable<string>? audiences, CancellationToken cancellationToken);

    /// <summary>Gets a collection of all audiences by scope names.</summary>
    /// <param name="scopes">An object that represents a collection of scope names that relate to audiencies.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceEntity>> GetAudiencesByScopesAsync(
      IEnumerable<string>? scopes, CancellationToken cancellationToken);
  }
}
