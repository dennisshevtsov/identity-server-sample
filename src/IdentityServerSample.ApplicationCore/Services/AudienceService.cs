// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using System;

  using AutoMapper;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to execute audience queries and commands.</summary>
  public sealed class AudienceService : IAudienceService
  {
    private readonly IMapper _mapper;

    private readonly IAudienceRepository _audienceRepository;

    private readonly IAudienceScopeService _audienceScopeService;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.ApplicationCore.Services.AudienceService"/> class.</summary>
    /// <param name="mapper">An object that provides a simple API to map objects of different types.</param>
    /// <param name="audienceRepository">An object that provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/> class.</param>
    /// <param name="audienceScopeService">An object that provides a simple API to execute audience queries and commands.</param>
    public AudienceService(
      IMapper mapper,
      IAudienceRepository audienceRepository,
      IAudienceScopeService audienceScopeService)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

      _audienceRepository = audienceRepository ??
        throw new ArgumentNullException(nameof(audienceRepository));

      _audienceScopeService = audienceScopeService ??
        throw new ArgumentNullException(nameof(audienceScopeService));
    }

    /// <summary>Gets a collection of audiences that satisfy defined conditions.</summary>
    /// <param name="requestDto">An object that represents conditions to query audiencies.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<GetAudiencesResponseDto> GetAudiencesAsync(
      GetAudiencesRequestDto requestDto, CancellationToken cancellationToken)
    {
      var audienceEntityCollection = await GetAudiencesAsync(cancellationToken);
      
      var getAudiencesResponseDto =
        _mapper.Map<GetAudiencesResponseDto>(audienceEntityCollection);

      return getAudiencesResponseDto;
    }

    /// <summary>Gets a collection of audiences.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<List<AudienceEntity>> GetAudiencesAsync(CancellationToken cancellationToken)
    {
      var audienceEntityCollection =
        await _audienceRepository.GetAudiencesAsync(CancellationToken.None);

      var audienceScopeDictionary =
        await _audienceScopeService.GetAudienceScopesAsync(cancellationToken);

      AudienceService.AddScopes(audienceEntityCollection, audienceScopeDictionary);

      return audienceEntityCollection;
    }

    /// <summary>Gets a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/> that have non-empty intersection with a defined collection of scope names.</summary>
    /// <param name="scopes">An object that represents a collection of scope names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<List<AudienceEntity>> GetAudiencesByScopesAsync(
      IEnumerable<string> scopes, CancellationToken cancellationToken)
    {
      var audienceScopeDictionary =
        await _audienceScopeService.GetAudienceScopesAsync(
          scopes.ToScopeIdentities(), cancellationToken);

      var audienceEntityCollection =
        await _audienceRepository.GetAudiencesAsync(
          audienceScopeDictionary.Keys.ToAudienceIdentities(), CancellationToken.None);

      AudienceService.AddScopes(audienceEntityCollection, audienceScopeDictionary);

      return audienceEntityCollection;
    }

    /// <summary>Gets a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/> with defined names.</summary>
    /// <param name="audiences">An object that represents a collection of audience names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<List<AudienceEntity>> GetAudiencesByNamesAsync(
      IEnumerable<string> audiences, CancellationToken cancellationToken)
    {
      var audienceEntityCollection =
        await _audienceRepository.GetAudiencesAsync(
          audiences.ToAudienceIdentities(), CancellationToken.None);

      var audienceScopeDictionary =
        await _audienceScopeService.GetAudienceScopesAsync(
          audienceEntityCollection, cancellationToken);

      AudienceService.AddScopes(audienceEntityCollection, audienceScopeDictionary);

      return audienceEntityCollection;
    }

    /// <summary>Gets an instance of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/> with a defined name.</summary>
    /// <param name="identity">An object that represents an identity of an audience.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<AudienceEntity?> GetAudienceAsync(IAudienceIdentity identity, CancellationToken cancellationToken)
      => _audienceRepository.GetAudienceAsync(identity, cancellationToken);

    private static void AddScopes(
      AudienceEntity audienceEntity,
      Dictionary<string, List<string>> audienceScopeDictionary)
    {
      if (audienceScopeDictionary.TryGetValue(
          audienceEntity.AudienceName!, out var scopesPerAudience))
      {
        audienceEntity.Scopes = scopesPerAudience;
      }
    }

    private static void AddScopes(
      List<AudienceEntity> audienceEntityCollection,
      Dictionary<string, List<string>> audienceScopeDictionary)
    {
      for (int i = 0; i < audienceEntityCollection.Count; i++)
      {
        AudienceService.AddScopes(audienceEntityCollection[i], audienceScopeDictionary);
      }
    }
  }
}
