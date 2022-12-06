// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Stores
{
  using IdentityServer4.Models;
  using IdentityServer4.Stores;

  /// <summary>Provides a simple API to query resources.</summary>
  public sealed class ResourceStore : IResourceStore
  {
    /// <summary>Gets identity resources by scope name.</summary>
    /// <param name="scopeNames"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
    {
      throw new NotImplementedException();
    }

    /// <summary>Gets API scopes by scope name.</summary>
    /// <param name="scopeNames"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
    {
      throw new NotImplementedException();
    }

    /// <summary>Gets API resources by scope name.</summary>
    /// <param name="scopeNames"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
    {
      throw new NotImplementedException();
    }

    /// <summary>Gets API resources by API resource name.</summary>
    /// <param name="apiResourceNames"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
    {
      throw new NotImplementedException();
    }

    /// <summary>Gets all resources.</summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Resources> GetAllResourcesAsync()
    {
      throw new NotImplementedException();
    }
  }
}
