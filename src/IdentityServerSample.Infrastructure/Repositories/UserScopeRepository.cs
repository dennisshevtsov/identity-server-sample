// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories
{
  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.UserScopeEntity"/> class.</summary>
  public sealed class UserScopeRepository : IUserScopeRepository
  {
    private readonly DbContext _dbContext;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.Infrastructure.Repositories.UserScopeRepository"/> class.</summary>
    /// <param name="dbContext">An object that represents a session with the database and can be used to query and save instances of your entities.</param>
    public UserScopeRepository(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>Gets a collection of scopes for a user.</summary>
    /// <param name="identity">An object that represents an identity of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<List<UserScopeEntity>> GetUserScopesAsync(IUserIdentity identity, CancellationToken cancellationToken)
    {
      var userScopeEntityCollection =
        await _dbContext.Set<UserScopeEntity>()
                        .AsNoTracking()
                        .WithPartitionKey(identity.UserId.ToString())
                        .OrderBy(entity => entity.ScopeName)
                        .ToListAsync(cancellationToken);

      return userScopeEntityCollection;
    }
  }
}
