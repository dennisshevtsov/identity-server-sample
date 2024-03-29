﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Repositories
{
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.UserEntity"/> class.</summary>
  public interface IUserRepository
  {
    /// <summary>Creates a new user.</summary>
    /// <param name="userEntity">An object that represents details of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task AddUserAsync(UserEntity userEntity, CancellationToken cancellationToken);

    /// <summary>Gets a user by a user identity.</summary>
    /// <param name="userIdentity">An object that represents an identity of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<UserEntity?> GetUserAsync(IUserIdentity userIdentity, CancellationToken cancellationToken);

    /// <summary>Gets a user by an email.</summary>
    /// <param name="email">An object that represents an email of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<UserEntity?> GetUserAsync(string email, CancellationToken cancellationToken);
  }
}
