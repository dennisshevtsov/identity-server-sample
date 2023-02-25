// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using System;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to query and save scopes.</summary>
  public sealed class ScopeService : IScopeService
  {
    private readonly IScopeRepository _scopeRepository;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.ApplicationCore.Services.ScopeService"/> class.</summary>
    /// <param name="scopeRepository">An object that provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.ScopeEntity"/> class.</param>
    public ScopeService(IScopeRepository scopeRepository)
    {
      _scopeRepository = scopeRepository ?? throw new ArgumentNullException(nameof(scopeRepository));
    }

    /// <summary>Gets a collection of available scopes.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<ScopeEntity>> GetScopesAsync(CancellationToken cancellationToken)
      => _scopeRepository.GetScopesAsync(cancellationToken)

    /// <summary>Gets a collection of scopes with names.</summary>
    /// <param name="scopes">An object that represents a collection of scope names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<ScopeEntity>> GetScopesAsync(
      IEnumerable<string> scopes, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    /// <summary>Gets a collection of standard scopes with names.</summary>
    /// <param name="scopes">An object that represents a collection of scope names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<ScopeEntity>> GetStandardScopesAsync(
      IEnumerable<string> scopes, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
