// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using System;

  using AutoMapper;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to execute audience queries and commands.</summary>
  public sealed class AudienceService : IAudienceService
  {
    private readonly IMapper _mapper;
    private readonly IAudienceRepository _audienceRepository;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.ApplicationCore.Services.AudienceService"/> class.</summary>
    /// <param name="mapper">An object that provides a simple API to map objects of different types.</param>
    /// <param name="audienceRepository">An object that provides a simple API to query and save audiences.</param>
    public AudienceService(
      IMapper mapper,
      IAudienceRepository audienceRepository)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _audienceRepository = audienceRepository ?? throw new ArgumentNullException(nameof(audienceRepository));
    }

    /// <summary>Gets a collection of audiences that satisfy defined conditions.</summary>
    /// <param name="query">An object that represents conditions to query audiencies.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<GetAudiencesResponseDto> GetAudiencesAsync(
      GetAudiencesRequestDto query, CancellationToken cancellationToken)
    {
      var audienceEntityCollection = await _audienceRepository.GetAudiencesAsync(cancellationToken);
      var getAudiencesResponseDto = _mapper.Map<GetAudiencesResponseDto>(audienceEntityCollection);

      return getAudiencesResponseDto;
    }

    /// <summary>Gets a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/> that have non-empty intersection with a defined collection of scope names.</summary>
    /// <param name="scopes">An object that represents a collection of scope names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<List<AudienceEntity>> GetAudiencesAsync(
      IEnumerable<string> scopes, CancellationToken cancellationToken)
    {
      var audienceEntityCollection =
        await _audienceRepository.GetAudiencesByScopesAsync(
          scopes, CancellationToken.None);

      return audienceEntityCollection;
    }
  }
}
