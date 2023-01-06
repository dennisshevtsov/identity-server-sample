// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Stores
{
  using System.Linq;

  using IdentityServer4.Models;
  using IdentityServer4.Stores;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to query resources.</summary>
  public sealed class ResourceStore : IResourceStore
  {
    private readonly IEnumerable<IdentityResource> _identityResources;
    private readonly IEnumerable<ApiResource> _apiResources;

    private readonly IScopeRepository _scopeRepository;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.WebApp.Stores.ResourceStore"/> class.</summary>
    /// <param name="configuration">An object that represents a set of key/value application configuration properties.</param>
    public ResourceStore(IConfiguration configuration, IScopeRepository scopeRepository)
    {
      _identityResources = new IdentityResource[]
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
      };

      _apiResources = new[]
      {
        new ApiResource
        {
          Name = configuration["ApiResource_Name"],
          DisplayName = configuration["ApiResource_DisplayName"],
          Scopes =
          {
            configuration["ApiScope_Name"],
          },
        },
      };

      _scopeRepository = scopeRepository ?? throw new ArgumentNullException(nameof(scopeRepository));
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
        scopeEntityCollection.Select(ResourceStore.ToApiScope)
                             .ToArray();

      return apiScopeCollection;
    }

    /// <summary>Gets API resources by scope name.</summary>
    /// <param name="scopeNames">An object that represents a collection of scope names.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(
      IEnumerable<string> scopeNames)
    {
      var apiRecources = _apiResources;

      if (scopeNames != null && scopeNames.Any())
      {
        apiRecources = apiRecources.Where(
          resource => resource.Scopes != null &&
                      resource.Scopes.Any(scope => scopeNames.Contains(scope)));
      }

      return Task.FromResult(apiRecources);
    }

    /// <summary>Gets API resources by API resource name.</summary>
    /// <param name="apiResourceNames">An object that represents a collection of API resource names.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(
      IEnumerable<string> apiResourceNames)
    {
      return Task.FromResult(_apiResources.Where(resource => apiResourceNames.Contains(resource.Name)));
    }

    /// <summary>Gets all resources.</summary>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task<Resources> GetAllResourcesAsync()
    {
      var scopeEntityCollection =
        await _scopeRepository.GetScopesAsync(CancellationToken.None);

      var apiScopeCollection =
        scopeEntityCollection.Select(ResourceStore.ToApiScope)
                             .ToArray();

      return new Resources(_identityResources, _apiResources, apiScopeCollection);
    }

    private static ApiScope ToApiScope(ScopeEntity scopeEntity)
    {
      return new ApiScope
      {
        Name = scopeEntity.Name,
        DisplayName = scopeEntity.DisplayName,
      };
    }
  }
}
