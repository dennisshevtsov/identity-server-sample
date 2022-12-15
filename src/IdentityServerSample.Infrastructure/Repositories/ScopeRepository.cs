// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories
{
  using System;

  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to query and save scopes in the database.</summary>
  public sealed class ScopeRepository : IScopeRepository
  {
    private readonly DbContext _dbContext;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.Infrastructure.Repositories.ScopeRepository"/> class.</summary>
    /// <param name="dbContext">An object that represents a session with the database and can be used to query and save instances of your entities.</param>
    public ScopeRepository(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>Gets a collection of available scopes.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<ScopeEntity>> GetScopesAsync(CancellationToken cancellationToken)
    {
      return _dbContext.Set<ScopeEntity>()
                       .ToListAsync(cancellationToken);
    }

    /// <summary>Gets a collection of scopes that satisfy defined conditions.</summary>
    /// <param name="scopes">An object that represents a collection of scope names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<ScopeEntity>> GetScopesAsync(string[] scopes, CancellationToken cancellationToken)
    {
      return _dbContext.Set<ScopeEntity>()
                       .Where(entity => scopes.Contains(entity.Name))
                       .ToListAsync(cancellationToken);
    }
  }
}
