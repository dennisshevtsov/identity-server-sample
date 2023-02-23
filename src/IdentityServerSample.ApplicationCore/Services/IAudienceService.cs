// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to execute audience queries and commands.</summary>
  public interface IAudienceService
  {
    /// <summary>Gets a collection of audiences that satisfy defined conditions.</summary>
    /// <param name="query">An object that represents conditions to query audiencies.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<GetAudiencesResponseDto> GetAudiencesAsync(
      GetAudiencesRequestDto query, CancellationToken cancellationToken);

    /// <summary>Gets a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/> that have non-empty intersection with a defined collection of scope names.</summary>
    /// <param name="scopes">An object that represents a collection of scope names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceEntity>> GetAudiencesAsync(
      IEnumerable<string> scopes, CancellationToken cancellationToken);
  }
}
