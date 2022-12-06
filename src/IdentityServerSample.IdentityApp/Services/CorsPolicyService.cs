// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Services
{
  using IdentityServer4.Services;

  /// <summary>Provides a simple API to determine if CORS is allowed.</summary>
  public sealed class CorsPolicyService : ICorsPolicyService
  {
    /// <summary>Checks if an origin is allowed.</summary>
    /// <param name="origin">An object that represents an origin.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task<bool> IsOriginAllowedAsync(string origin)
    {
      return Task.FromResult(origin == "http://localhost:4200");
    }
  }
}
