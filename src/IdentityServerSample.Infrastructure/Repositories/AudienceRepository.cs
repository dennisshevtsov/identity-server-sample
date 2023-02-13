// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories
{
  using System;

  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/> class.</summary>
  public sealed class AudienceRepository : IAudienceRepository
  {
    private readonly DbContext _dbContext;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.Infrastructure.Repositories.AudienceRepository"/> class.</summary>
    /// <param name="dbContext">An object that represents a session with the database and can be used to query and save instances of your entities.</param>
    public AudienceRepository(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>Gets a collection of all audiences.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<AudienceEntity[]> GetAudiencesAsync(CancellationToken cancellationToken)
    {
      return _dbContext.Set<AudienceEntity>()
                       .AsNoTracking()
                       .OrderBy(entity => entity.Name)
                       .ToArrayAsync(cancellationToken);
    }

    /// <summary>Gets a collection of audiences by audience names.</summary>
    /// <param name="audiences">An object that represents a collection of audience names.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<AudienceEntity[]> GetAudiencesByNamesAsync(string[]? audiences, CancellationToken cancellationToken)
    {
      var query = _dbContext.Set<AudienceEntity>()
                            .AsNoTracking();

      if (audiences != null && audiences.Length > 0)
      {
        query = query.Where(entity => audiences.Contains(entity.Name));
      }

      return query.OrderBy(entity => entity.Name)
                  .ToArrayAsync(cancellationToken);
    }

    /// <summary>Gets a collection of all audiences by scope names.</summary>
    /// <param name="scopes">An object that represents a collection of scope names that relate to audiencies.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<AudienceEntity[]> GetAudiencesByScopesAsync(string[] scopes, CancellationToken cancellationToken)
    {
      var audienceEntityCollection =
        await _dbContext.Set<AudienceEntity>()
                        .AsNoTracking()
                        .OrderBy(entity => entity.Name)
                        .ToArrayAsync();


      if (scopes != null && scopes.Length > 0)
      {
        audienceEntityCollection =
          audienceEntityCollection.Where(entity => entity.Scopes != null && entity.Scopes.Any(scope => scopes.Contains(scope)))
                                  .ToArray();
      }

      return audienceEntityCollection;
    }
  }
}
