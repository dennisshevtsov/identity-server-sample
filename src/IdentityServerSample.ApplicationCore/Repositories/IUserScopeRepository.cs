// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Repositories
{
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.UserScopeEntity"/> class.</summary>
  public interface IUserScopeRepository
  {
    /// <summary>Gets a collection of scopes for a user.</summary>
    /// <param name="identity">An object that represents an identity of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<List<UserScopeEntity>> GetUserScopesAsync(IUserIdentity identity, CancellationToken cancellationToken);

    /// <summary>Creates scopes for a user.</summary>
    /// <param name="userEntity">An object that represents details of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task AddUserScopesAsync(UserEntity userEntity, CancellationToken cancellationToken);
  }
}
