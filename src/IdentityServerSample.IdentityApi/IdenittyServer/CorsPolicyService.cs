// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.IdenittyServer
{
  using System;

  using IdentityServer4.Services;
  using Microsoft.Extensions.Configuration;

  using IdentityServerSample.ApplicationCore.Services;

  /// <summary>Provides a simple API to determine if CORS is allowed.</summary>
  public sealed class CorsPolicyService : ICorsPolicyService
  {
    private readonly IConfiguration _configuration;
    private readonly IClientService _clientService;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.WebApp.Services.CorsPolicyService"/> class.</summary>
    /// <param name="configuration">An object that represents a set of key/value application configuration properties.</param>
    public CorsPolicyService(IConfiguration configuration, IClientService clientService)
    {
      _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
      _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
    }

    /// <summary>Checks if an origin is allowed.</summary>
    /// <param name="origin">An object that represents an origin.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task<bool> IsOriginAllowedAsync(string origin)
      => _clientService.CheckIfOriginIsAllowedAsync(origin, CancellationToken.None);
    //{
    //  return Task.FromResult(origin == _configuration["WebApp_Url"]);
    //}
  }
}
