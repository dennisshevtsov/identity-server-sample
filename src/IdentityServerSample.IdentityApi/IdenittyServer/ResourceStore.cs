// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.IdenittyServer
{
  using System.Linq;

  using AutoMapper;
  using IdentityServer4.Models;
  using IdentityServer4.Stores;

  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to query resources.</summary>
  public sealed class ResourceStore : IResourceStore
  {
    private readonly IEnumerable<IdentityResource> _identityResources;

    private readonly IMapper _mapper;

    private readonly IAudienceRepository _audienceRepository;
    private readonly IScopeRepository _scopeRepository;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.WebApp.Stores.ResourceStore"/> class.</summary>
    /// <param name="mapper">An object that provides a simple API to map objects of different types.</param>
    /// <param name="audienceRepository">An object that provides a simple API to query and save audiences.</param>
    /// <param name="scopeRepository">An object that provides a simple API to query and save audiences.</param>
    public ResourceStore(
      IMapper mapper,
      IScopeRepository scopeRepository,
      IAudienceRepository audienceRepository)
    {
      _identityResources = new IdentityResource[]
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
        new IdentityResources.Phone(),
        new IdentityResources.Address(),
      };

      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _scopeRepository = scopeRepository ??
        throw new ArgumentNullException(nameof(scopeRepository));
      _audienceRepository = audienceRepository ??
        throw new ArgumentNullException(nameof(audienceRepository));
    }

    /// <summary>Gets identity resources by scope name.</summary>
    /// <param name="scopeNames">An object that represents a collection of scope names.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(
      IEnumerable<string> scopeNames)
    {
      return Task.FromResult(_identityResources.Where(resource => scopeNames.Contains(resource.Name)));
    }

    /// <summary>Gets API scopes by scope name.</summary>
    /// <param name="scopeNames">An object that represents a collection of scope names.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(
      IEnumerable<string> scopeNames)
    {
      var scopeEntityCollection =
        await _scopeRepository.GetScopesAsync(
          scopeNames.ToArray(), CancellationToken.None);

      var apiScopeCollection =
        _mapper.Map<IEnumerable<ApiScope>>(scopeEntityCollection);

      return apiScopeCollection;
    }

    /// <summary>Gets API resources by scope name.</summary>
    /// <param name="scopeNames">An object that represents a collection of scope names.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(
      IEnumerable<string> scopeNames)
    {
      var audienceEntityCollection =
        await _audienceRepository.GetAudiencesByScopesAsync(
          scopeNames.ToArray(), CancellationToken.None);

      var apiResourceCollection =
        _mapper.Map<IEnumerable<ApiResource>>(audienceEntityCollection);

      return apiResourceCollection;
    }

    /// <summary>Gets API resources by API resource name.</summary>
    /// <param name="apiResourceNames">An object that represents a collection of API resource names.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(
      IEnumerable<string> apiResourceNames)
    {
      var audienceEntityCollection =
        await _audienceRepository.GetAudiencesByNamesAsync(apiResourceNames.ToArray(), CancellationToken.None);

      var apiResourceCollection =
        _mapper.Map<IEnumerable<ApiResource>>(audienceEntityCollection);

      return apiResourceCollection;
    }

    /// <summary>Gets all resources.</summary>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task<Resources> GetAllResourcesAsync()
    {
      var scopeEntityCollection =
        await _scopeRepository.GetScopesAsync(CancellationToken.None);

      var apiScopeCollection =
        _mapper.Map<IEnumerable<ApiScope>>(scopeEntityCollection);

      var audienceEntityCollection =
        await _audienceRepository.GetAudiencesAsync(CancellationToken.None);

      var apiResourceCollection =
        _mapper.Map<IEnumerable<ApiResource>>(audienceEntityCollection);

      return new Resources(_identityResources, apiResourceCollection, apiScopeCollection);
    }
  }
}
