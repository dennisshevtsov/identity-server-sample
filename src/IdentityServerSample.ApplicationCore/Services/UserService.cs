// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to modify/query instances of <see cref="IdentityServerSample.ApplicationCore.Entities.UserEntity"/> class.</summary>
  public sealed class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    private readonly IUserScopeRepository _userScopeRepository;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.ApplicationCore.Services.UserService"/> class.</summary>
    /// <param name="userRepository"></param>
    /// <param name="userScopeRepository"></param>
    public UserService(IUserRepository userRepository, IUserScopeRepository userScopeRepository)
    {
      _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
      _userScopeRepository = userScopeRepository ?? throw new ArgumentNullException(nameof(userScopeRepository));
    }

    /// <summary>Creates a new user.</summary>
    /// <param name="userEntity">An object that represents details of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task AddUserAsync(UserEntity userEntity, CancellationToken cancellationToken)
    {
      await _userRepository.AddUserAsync(userEntity, cancellationToken);
      await _userScopeRepository.UpdateUserScopesAsync(userEntity, cancellationToken);
    }

    /// <summary>Gets a user by a user ID.</summary>
    /// <param name="identity">An object that represents an identity of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<UserEntity?> GetUserAsync(IUserIdentity identity, CancellationToken cancellationToken)
    {
      var userEntity = await _userRepository.GetUserAsync(identity, cancellationToken);

      if (userEntity != null)
      {
        userEntity.Scopes =
          await _userScopeRepository.GetUserScopesAsync(identity, cancellationToken);
      }

      return userEntity;
    }

    /// <summary>Gets a user by a user ID.</summary>
    /// <param name="email">An object that represents an email of a user.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<UserEntity?> GetUserAsync(string email, CancellationToken cancellationToken)
    {
      var userEntity = await _userRepository.GetUserAsync(email, cancellationToken);

      if (userEntity != null)
      {
        userEntity.Scopes =
          await _userScopeRepository.GetUserScopesAsync(userEntity, cancellationToken);
      }

      return userEntity;
    }
  }
}
