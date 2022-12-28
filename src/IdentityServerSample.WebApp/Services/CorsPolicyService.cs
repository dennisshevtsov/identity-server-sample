// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApp.Services
{
  using System;

  using IdentityServer4.Services;
  using Microsoft.Extensions.Configuration;

  /// <summary>Provides a simple API to determine if CORS is allowed.</summary>
  public sealed class CorsPolicyService : ICorsPolicyService
  {
    private IConfiguration _configuration;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.WebApp.Services.CorsPolicyService"/> class.</summary>
    /// <param name="configuration">An object that represents a set of key/value application configuration properties.</param>
    public CorsPolicyService(IConfiguration configuration)
    {
      _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    /// <summary>Checks if an origin is allowed.</summary>
    /// <param name="origin">An object that represents an origin.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task<bool> IsOriginAllowedAsync(string origin)
    {
      return Task.FromResult(origin == _configuration["WebApp_Url"]);
    }
  }
}
