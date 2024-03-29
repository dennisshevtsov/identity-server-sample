﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Provides a simple API to execute audience queries and commands.</summary>
  public interface IAudienceService
  {
    /// <summary>Gets a collection of audiences that satisfy defined conditions.</summary>
    /// <param name="requestDto">An object that represents conditions to query audiencies.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<GetAudiencesResponseDto> GetAudiencesAsync(
      GetAudiencesRequestDto requestDto, CancellationToken cancellationToken);

    /// <summary>Gets a collection of audiences.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceEntity>> GetAudiencesAsync(CancellationToken cancellationToken);

    /// <summary>Gets a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/> that have non-empty intersection with a defined collection of scope names.</summary>
    /// <param name="scopes">An object that represents a collection of scope names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceEntity>> GetAudiencesByScopesAsync(
      IEnumerable<string> scopes, CancellationToken cancellationToken);

    /// <summary>Gets a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/> with defined names.</summary>
    /// <param name="audiences">An object that represents a collection of audience names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceEntity>> GetAudiencesByNamesAsync(
      IEnumerable<string> audiences, CancellationToken cancellationToken);

    /// <summary>Gets an instance of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/> with a defined name.</summary>
    /// <param name="identity">An object that represents an identity of an audience.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<AudienceEntity?> GetAudienceAsync(IAudienceIdentity identity, CancellationToken cancellationToken);

    /// <summary>Adds a new audience.</summary>
    /// <param name="requestDto">An object that represents data to add new audience.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation.</returns>
    public Task AddAudienceAsync(AddAudienceRequestDto requestDto, CancellationToken cancellationToken);
  }
}
