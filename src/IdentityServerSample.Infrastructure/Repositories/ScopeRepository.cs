// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories
{
  using System;
  using System.Linq;

  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.ScopeEntity"/> class.</summary>
  public sealed class ScopeRepository : IScopeRepository
  {
    private readonly DbContext _dbContext;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.Infrastructure.Repositories.ScopeRepository"/> class.</summary>
    /// <param name="dbContext">An object that represents a session with the database and can be used to query and save instances of your entities.</param>
    public ScopeRepository(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>Creates a new scope.</summary>
    /// <param name="scopeEntity">An object that represents details of a scope.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task AddScopeAsync(ScopeEntity scopeEntity, CancellationToken cancellationToken)
    {
      var scopeEntityEntry = _dbContext.Add(scopeEntity);

      await _dbContext.SaveChangesAsync(cancellationToken);

      scopeEntityEntry.State = EntityState.Detached;
    }

    /// <summary>Gets a scope by its identity.</summary>
    /// <param name="identity">An object that represents an identity of a scope.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<ScopeEntity?> GetScopeAsync(IScopeIdentity identity, CancellationToken cancellationToken)
      => _dbContext.Set<ScopeEntity>()
                   .AsNoTracking()
                   .WithPartitionKey(identity.ScopeName!.ToString())
                   .FirstOrDefaultAsync();

    /// <summary>Gets a collection of scopes that satisfy defined conditions.</summary>
    /// <param name="identities">An object that represents a collection the <see cref="IdentityServerSample.ApplicationCore.Identities.IScopeIdentity"/>.</param>
    /// <param name="standard">An object that indicates if scopes are standard.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<ScopeEntity>> GetScopesAsync(
      IEnumerable<IScopeIdentity>? identities, bool standard, CancellationToken cancellationToken)
    {
      var dbScopeSet = _dbContext.Set<ScopeEntity>()
                                 .AsNoTracking()
                                 .Where(entity => entity.Standard == standard);

      if (identities != null && identities.Any())
      {
        var scopeNameCollection =
          identities.Select(identity => identity.ScopeName)
                    .ToList();

        dbScopeSet = dbScopeSet.Where(entity => scopeNameCollection.Contains(entity.ScopeName));
      }

      return dbScopeSet.OrderBy(entity => entity.ScopeName)
                       .ToListAsync(cancellationToken);
    }
  }
}
