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

  /// <summary>Provides a simple API to query and save scopes.</summary>
  public sealed class ScopeService : IScopeService
  {
    private readonly IMapper _mapper;
    private readonly IScopeRepository _scopeRepository;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.ApplicationCore.Services.ScopeService"/> class.</summary>
    /// <param name="mapper">An object that provides a simple API to map objects of different types.</param>
    /// <param name="scopeRepository">An object that provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.ScopeEntity"/> class.</param>
    public ScopeService(
      IMapper mapper,
      IScopeRepository scopeRepository)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _scopeRepository = scopeRepository ??
        throw new ArgumentNullException(nameof(scopeRepository));
    }

    /// <summary>Gets a collection of available scopes.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<ScopeEntity>> GetScopesAsync(CancellationToken cancellationToken)
      => _scopeRepository.GetScopesAsync(null, false, cancellationToken);

    /// <summary>Gets a collection of scopes with names.</summary>
    /// <param name="scopes">An object that represents a collection of scope names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<ScopeEntity>> GetScopesAsync(
      IEnumerable<string> scopes, CancellationToken cancellationToken)
      => _scopeRepository.GetScopesAsync(scopes.ToScopeIdentities(), false, cancellationToken);

    /// <summary>Gets a collection of standard scopes.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<ScopeEntity>> GetStandardScopesAsync(CancellationToken cancellationToken)
      => _scopeRepository.GetScopesAsync(null, true, cancellationToken);

    /// <summary>Gets a collection of standard scopes with names.</summary>
    /// <param name="scopes">An object that represents a collection of scope names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<ScopeEntity>> GetStandardScopesAsync(
      IEnumerable<string> scopes, CancellationToken cancellationToken)
      => _scopeRepository.GetScopesAsync(scopes.ToScopeIdentities(), true, cancellationToken);

    /// <summary>Gets a scope by its identity.</summary>
    /// <param name="identity">An object that represents an identity of a scope.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<ScopeEntity?> GetScopeAsync(
      IScopeIdentity identity, CancellationToken cancellationToken)
      => _scopeRepository.GetScopeAsync(identity, cancellationToken);

    /// <summary>Creates a new scope.</summary>
    /// <param name="scopeEntity">An object that represents details of a scope.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation.</returns>
    public Task AddScopeAsync(ScopeEntity scopeEntity, CancellationToken cancellationToken)
      => _scopeRepository.AddScopeAsync(scopeEntity, cancellationToken);

    /// <summary>Creates a new scope.</summary>
    /// <param name="requestDto">An object that represents data to create a new scope.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation.</returns>
    public async Task AddScopeAsync(AddScopeRequestDto requestDto, CancellationToken cancellationToken)
    {
      var scopeEntity = _mapper.Map<ScopeEntity>(requestDto);

      await _scopeRepository.AddScopeAsync(scopeEntity, cancellationToken);
    }
  }
}
