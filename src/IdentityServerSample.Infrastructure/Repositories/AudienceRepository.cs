// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories
{
  using System;

  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;
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
    public Task<List<AudienceEntity>> GetAudiencesAsync(CancellationToken cancellationToken)
    {
      return _dbContext.Set<AudienceEntity>()
                       .AsNoTracking()
                       .OrderBy(entity => entity.AudienceName)
                       .ToListAsync(cancellationToken);
    }

    /// <summary>Gets a collection of audiences by audience names.</summary>
    /// <param name="identities">An object that represents a collection of the <see cref="IdentityServerSample.ApplicationCore.Identities.IAudienceIdentity"/>.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceEntity>> GetAudiencesAsync(
      IEnumerable<IAudienceIdentity>? identities, CancellationToken cancellationToken)
    {
      var query = _dbContext.Set<AudienceEntity>()
                            .AsNoTracking();

      if (identities != null && identities.Any())
      {
        var audienceNameCollection =
          identities.Select(identity => identity.AudienceName)
                    .ToList();

        query = query.Where(entity => audienceNameCollection.Contains(entity.AudienceName));
      }

      return query.OrderBy(entity => entity.AudienceName)
                  .ToListAsync(cancellationToken);
    }

    /// <summary>Gets an instance of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/> with a defined name.</summary>
    /// <param name="identity">An object that represents an identity of an audience.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<AudienceEntity?> GetAudienceAsync(IAudienceIdentity identity, CancellationToken cancellationToken)
      => _dbContext.Set<AudienceEntity>()
                   .AsNoTracking()
                   .WithPartitionKey(identity.AudienceName!)
                   .SingleOrDefaultAsync(cancellationToken);

    /// <summary>Adds a new audience.</summary>
    /// <param name="audienceEntity">An object that represents details of an audience.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation.</returns>
    public async Task AddAudienceAsync(AudienceEntity audienceEntity, CancellationToken cancellationToken)
    {
      var audienceEntityEntry = _dbContext.Add(audienceEntity);

      await _dbContext.SaveChangesAsync(cancellationToken);

      audienceEntityEntry.State = EntityState.Detached;
    }
  }
}
