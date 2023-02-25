// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityServer.Stores
{
  using System.Linq;

  using AutoMapper;
  using IdentityServer4.Models;
  using IdentityServer4.Stores;

  using IdentityServerSample.ApplicationCore.Services;

  /// <summary>Provides a simple API to query resources.</summary>
  public sealed class ResourceStore : IResourceStore
  {
    private readonly IMapper _mapper;

    private readonly IAudienceService _audienceService;
    private readonly IScopeService _scopeService;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.WebApp.Stores.ResourceStore"/> class.</summary>
    /// <param name="mapper">An object that provides a simple API to map objects of different types.</param>
    /// <param name="audienceService">An object that provides a simple API to execute audience queries and commands.</param>
    /// <param name="audienceService">An object that provides a simple API to query and save scopes.</param>
    public ResourceStore(
      IMapper mapper,
      IAudienceService audienceService,
      IScopeService scopeService)
    {
      //_identityResources = new IdentityResource[]
      //{
      //  new IdentityResources.OpenId(),
      //  new IdentityResources.Profile(),
      //  new IdentityResources.Email(),
      //  new IdentityResources.Phone(),
      //  new IdentityResources.Address(),
      //};

      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

      _audienceService = audienceService ??
        throw new ArgumentNullException(nameof(audienceService));
      _scopeService = scopeService ?? throw new ArgumentNullException(nameof(scopeService));
    }

    /// <summary>Gets identity resources by scope name.</summary>
    /// <param name="scopeNames">An object that represents a collection of scope names.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(
      IEnumerable<string> scopeNames)
    {
      var standardScopeEntityCollection =
       await _scopeService.GetStandardScopesAsync(CancellationToken.None);
      var identityResources =
        _mapper.Map<IEnumerable<IdentityResource>>(standardScopeEntityCollection);

      return identityResources;
    }

    /// <summary>Gets API scopes by scope name.</summary>
    /// <param name="scopeNames">An object that represents a collection of scope names.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(
      IEnumerable<string> scopeNames)
    {
      var scopeEntityCollection =
        await _scopeService.GetScopesAsync(scopeNames, CancellationToken.None);

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
        await _audienceService.GetAudiencesByScopesAsync(
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
        await _audienceService.GetAudiencesByNamesAsync(
          apiResourceNames, CancellationToken.None);

      var apiResourceCollection =
        _mapper.Map<IEnumerable<ApiResource>>(audienceEntityCollection);

      return apiResourceCollection;
    }

    /// <summary>Gets all resources.</summary>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task<Resources> GetAllResourcesAsync()
    {
      var standardScopeEntityCollection =
        await _scopeService.GetStandardScopesAsync(CancellationToken.None);
      var identityResources =
        _mapper.Map<IEnumerable<IdentityResource>>(standardScopeEntityCollection);

      var audienceEntityCollection =
        await _audienceService.GetAudiencesAsync(CancellationToken.None);
      var apiResourceCollection =
        _mapper.Map<IEnumerable<ApiResource>>(audienceEntityCollection);

      var scopeEntityCollection = await _scopeService.GetScopesAsync(CancellationToken.None);
      var apiScopeCollection =
        _mapper.Map<IEnumerable<ApiScope>>(standardScopeEntityCollection);

      return new Resources(identityResources, apiResourceCollection, apiScopeCollection);
    }
  }
}
