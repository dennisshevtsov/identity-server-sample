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
  
  /// <summary>Provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceScopeEntity"/> class.</summary>
  public sealed class AudienceScopeRepository : IAudienceScopeRepository
  {
    private readonly DbContext _dbContext;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.Infrastructure.Repositories.AudienceScopeRepository"/> class.</summary>
    /// <param name="dbContext">An object that represents a session with the database and can be used to query and save instances of your entities.</param>
    public AudienceScopeRepository(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>Gets a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceScopeEntity"/> for the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceEntity"/>.</summary>
    /// <param name="identity">An object that represents an identity of an audience.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceScopeEntity>> GetAudienceScopesAsync(
      IAudienceIdentity identity, CancellationToken cancellationToken)
      => _dbContext.Set<AudienceScopeEntity>()
                   .AsNoTracking()
                   .WithPartitionKey(identity.AudienceName!)
                   .OrderBy(entity => entity.ScopeName)
                   .ToListAsync(cancellationToken);

    /// <summary>Gets a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceScopeEntity"/> that relate to defined scopes.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<AudienceScopeEntity>> GetAudienceScopesAsync(CancellationToken cancellationToken)
      => _dbContext.Set<AudienceScopeEntity>()
                   .AsNoTracking()
                   .OrderBy(entity => entity.ScopeName)
                   .ToListAsync(cancellationToken);

    /// <summary>Gets a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceScopeEntity"/> that relate to defined scopes.</summary>
    /// <param name="identities">An object that represents a collection of the <see cref="IdentityServerSample.ApplicationCore.Identities.IScopeIdentity"/>.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<List<AudienceScopeEntity>> GetAudienceScopesAsync(
      IEnumerable<IScopeIdentity> identities, CancellationToken cancellationToken)
    {
      var audienceNameCollection =
        identities.Select(identity => identity.ScopeName!)
                  .ToList();

      var audienceScopeEntityCollection =
        await _dbContext.Set<AudienceScopeEntity>()
                        .AsNoTracking()
                        .Where(entity => audienceNameCollection.Contains(entity.ScopeName!))
                        .OrderBy(entity => entity.ScopeName)
                        .ToListAsync(cancellationToken);

      return audienceScopeEntityCollection;
    }

    /// <summary>Gets a collection of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceScopeEntity"/> for defined audiences.</summary>
    /// <param name="identities">An object that represents a collection of the <see cref="IdentityServerSample.ApplicationCore.Identities.IAudienceIdentity"/>.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<List<AudienceScopeEntity>> GetAudienceScopesAsync(
      IEnumerable<IAudienceIdentity> identities, CancellationToken cancellationToken)
    {
      var audienceNameCollection =
        identities.Select(identity => identity.AudienceName!)
                  .ToList();

      var audienceScopeEntityCollection =
        await _dbContext.Set<AudienceScopeEntity>()
                        .AsNoTracking()
                        .Where(entity => audienceNameCollection.Contains(entity.AudienceName!))
                        .OrderBy(entity => entity.ScopeName)
                        .ToListAsync(cancellationToken);

      return audienceScopeEntityCollection;
    }
  }
}
