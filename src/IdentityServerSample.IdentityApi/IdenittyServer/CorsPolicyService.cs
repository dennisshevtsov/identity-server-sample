// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.IdenittyServer
{
  using System;

  using IdentityServer4.Services;

  using IdentityServerSample.ApplicationCore.Services;

  /// <summary>Provides a simple API to determine if CORS is allowed.</summary>
  public sealed class CorsPolicyService : ICorsPolicyService
  {
    private readonly IClientService _clientService;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.WebApp.Services.CorsPolicyService"/> class.</summary>
    /// <param name="clientService">An object that provides a simple API to execute queries and commands with clients.</param>
    public CorsPolicyService(IClientService clientService)
    {
      _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
    }

    /// <summary>Checks if an origin is allowed.</summary>
    /// <param name="origin">An object that represents an origin.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task<bool> IsOriginAllowedAsync(string origin)
      => _clientService.CheckIfOriginIsAllowedAsync(origin, CancellationToken.None);
  }
}
