// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories
{
  using System;

  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Repositories;
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.UserEntity"/> class.</summary>
  public sealed class UserRepository : IUserRepository
  {
    private readonly DbContext _dbContext;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.Infrastructure.Repositories.UserRepository"/> class.</summary>
    /// <param name="dbContext">An object that represents a session with the database and can be used to query and save instances of your entities.</param>
    public UserRepository(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>Gets a user by a user identity.</summary>
    /// <param name="userIdentity">An object that represents an identity of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<UserEntity?> GetUserAsync(IUserIdentity userIdentity, CancellationToken cancellationToken)
    {
      var userEntity =
        await _dbContext.Set<UserEntity>()
                        .AsNoTracking()
                        .WithPartitionKey(userIdentity.UserId.ToString())
                        .Where(entity => entity.UserId == userIdentity.UserId)
                        .FirstOrDefaultAsync(cancellationToken);

      return userEntity;
    }

    /// <summary>Gets a user by an email.</summary>
    /// <param name="email">An object that represents an email of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<UserEntity?> GetUserAsync(string email, CancellationToken cancellationToken)
    {
      var userEntity =
        await _dbContext.Set<UserEntity>()
                        .AsNoTracking()
                        .Where(entity => entity.Email == email)
                        .FirstOrDefaultAsync(cancellationToken);

      return userEntity;
    }
  }
}
