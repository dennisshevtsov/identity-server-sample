// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.AspNetIdentity
{
  using System.Security.Claims;

  using IdentityModel;
  using Microsoft.AspNetCore.Identity;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;
  using IdentityServerSample.ApplicationCore.Services;

  /// <summary>Provides an abstraction for a store which manages user accounts.</summary>
  public sealed class UserStore : IUserStore<UserEntity>, IUserPasswordStore<UserEntity>, IUserRoleStore<UserEntity>, IUserEmailStore<UserEntity>, IUserClaimStore<UserEntity>
  {
    private readonly IUserService _userService;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.IdentityApi.AspNetIdentity.UserStore"/> class.</summary>
    /// <param name="userService">An object that provides a simple API to modify/query instances of <see cref="IdentityServerSample.ApplicationCore.Entities.UserEntity"/> class.</param>
    public UserStore(IUserService userService)
    {
      _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    #region Members of IUserStore

    /// <summary>
    /// Gets the user identifier for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose identifier should be retrieved.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the identifier for the specified <paramref name="user"/>.</returns>
    public Task<string> GetUserIdAsync(UserEntity user, CancellationToken cancellationToken)
      => Task.FromResult(user.UserId.ToString());

    /// <summary>
    /// Gets the user name for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose name should be retrieved.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the name for the specified <paramref name="user"/>.</returns>
    public Task<string?> GetUserNameAsync(UserEntity user, CancellationToken cancellationToken)
      => Task.FromResult(user.Email);

    /// <summary>
    /// Sets the given <paramref name="userName" /> for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose name should be set.</param>
    /// <param name="userName">The user name to set.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task SetUserNameAsync(UserEntity user, string? userName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Gets the normalized user name for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose normalized name should be retrieved.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the normalized user name for the specified <paramref name="user"/>.</returns>
    public Task<string?> GetNormalizedUserNameAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Sets the given normalized name for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose name should be set.</param>
    /// <param name="normalizedName">The normalized name to set.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task SetNormalizedUserNameAsync(UserEntity user, string? normalizedName, CancellationToken cancellationToken)
    {
      user.Email = normalizedName;

      return Task.CompletedTask;
    }

    /// <summary>
    /// Creates the specified <paramref name="user"/> in the user store.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the creation operation.</returns>
    public Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Updates the specified <paramref name="user"/> in the user store.
    /// </summary>
    /// <param name="user">The user to update.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the update operation.</returns>
    public Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Deletes the specified <paramref name="user"/> from the user store.
    /// </summary>
    /// <param name="user">The user to delete.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the delete operation.</returns>
    public Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Finds and returns a user, if any, who has the specified <paramref name="userId"/>.
    /// </summary>
    /// <param name="userId">The user ID to search for.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, containing the user matching the specified <paramref name="userId"/> if it exists.
    /// </returns>
    public async Task<UserEntity?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
      var identity = userId.ToUserIdentity();

      if (identity == null)
      {
        return null;
      }

      return await _userService.GetUserAsync(identity, cancellationToken);
    }

    /// <summary>
    /// Finds and returns a user, if any, who has the specified normalized user name.
    /// </summary>
    /// <param name="normalizedUserName">The normalized user name to search for.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, containing the user matching the specified <paramref name="normalizedUserName"/> if it exists.
    /// </returns>
    public async Task<UserEntity?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
      var userEntity = await _userService.GetUserAsync(
        normalizedUserName, cancellationToken);

      return userEntity;
    }

    #endregion

    #region Members of IUserPasswordStore

    /// <summary>
    /// Sets the password hash for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose password hash to set.</param>
    /// <param name="passwordHash">The password hash to set.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task SetPasswordHashAsync(UserEntity user, string? passwordHash, CancellationToken cancellationToken)
    {
      user.PasswordHash = passwordHash;

      return Task.CompletedTask;
    }

    /// <summary>
    /// Gets the password hash for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose password hash to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, returning the password hash for the specified <paramref name="user"/>.</returns>
    public Task<string?> GetPasswordHashAsync(UserEntity user, CancellationToken cancellationToken)
      => Task.FromResult(user.PasswordHash);

    /// <summary>
    /// Gets a flag indicating whether the specified <paramref name="user"/> has a password.
    /// </summary>
    /// <param name="user">The user to return a flag for, indicating whether they have a password or not.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, returning true if the specified <paramref name="user"/> has a password
    /// otherwise false.
    /// </returns>
    public Task<bool> HasPasswordAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    #endregion

    #region Members of IUserRoleStore

    /// <summary>
    /// Add the specified <paramref name="user"/> to the named role.
    /// </summary>
    /// <param name="user">The user to add to the named role.</param>
    /// <param name="roleName">The name of the role to add the user to.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task AddToRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Remove the specified <paramref name="user"/> from the named role.
    /// </summary>
    /// <param name="user">The user to remove the named role from.</param>
    /// <param name="roleName">The name of the role to remove.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task RemoveFromRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Gets a list of role names the specified <paramref name="user"/> belongs to.
    /// </summary>
    /// <param name="user">The user whose role names to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing a list of role names.</returns>
    public Task<IList<string>> GetRolesAsync(UserEntity user, CancellationToken cancellationToken)
      => Task.FromResult<IList<string>>(new List<string>());

    /// <summary>
    /// Returns a flag indicating whether the specified <paramref name="user"/> is a member of the given named role.
    /// </summary>
    /// <param name="user">The user whose role membership should be checked.</param>
    /// <param name="roleName">The name of the role to be checked.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, containing a flag indicating whether the specified <paramref name="user"/> is
    /// a member of the named role.
    /// </returns>
    public Task<bool> IsInRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Returns a list of Users who are members of the named role.
    /// </summary>
    /// <param name="roleName">The name of the role whose membership should be returned.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, containing a list of users who are in the named role.
    /// </returns>
    public Task<IList<UserEntity>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    #endregion

    #region Members of IUserEmailStore

    /// <summary>
    /// Sets the <paramref name="email"/> address for a <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose email should be set.</param>
    /// <param name="email">The email to set.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task SetEmailAsync(UserEntity user, string? email, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Gets the email address for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose email should be returned.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The task object containing the results of the asynchronous operation, the email address for the specified <paramref name="user"/>.</returns>
    public Task<string?> GetEmailAsync(UserEntity user, CancellationToken cancellationToken)
      => Task.FromResult(user.Email);

    /// <summary>
    /// Gets a flag indicating whether the email address for the specified <paramref name="user"/> has been verified, true if the email address is verified otherwise
    /// false.
    /// </summary>
    /// <param name="user">The user whose email confirmation status should be returned.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The task object containing the results of the asynchronous operation, a flag indicating whether the email address for the specified <paramref name="user"/>
    /// has been confirmed or not.
    /// </returns>
    public Task<bool> GetEmailConfirmedAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Sets the flag indicating whether the specified <paramref name="user"/>'s email address has been confirmed or not.
    /// </summary>
    /// <param name="user">The user whose email confirmation status should be set.</param>
    /// <param name="confirmed">A flag indicating if the email address has been confirmed, true if the address is confirmed otherwise false.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task SetEmailConfirmedAsync(UserEntity user, bool confirmed, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Gets the user, if any, associated with the specified, normalized email address.
    /// </summary>
    /// <param name="normalizedEmail">The normalized email address to return the user for.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The task object containing the results of the asynchronous lookup operation, the user if any associated with the specified normalized email address.
    /// </returns>
    public Task<UserEntity?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Returns the normalized email for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose email address to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The task object containing the results of the asynchronous lookup operation, the normalized email address if any associated with the specified user.
    /// </returns>
    public Task<string?> GetNormalizedEmailAsync(UserEntity user, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Sets the normalized email for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose email address to set.</param>
    /// <param name="normalizedEmail">The normalized email to set for the specified <paramref name="user"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task SetNormalizedEmailAsync(UserEntity user, string? normalizedEmail, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    #endregion

    #region Member of UserClaimStore

    /// <summary>
    /// Gets a list of <see cref="Claim"/>s to be belonging to the specified <paramref name="user"/> as an asynchronous operation.
    /// </summary>
    /// <param name="user">The role whose claims to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that represents the result of the asynchronous query, a list of <see cref="Claim"/>s.
    /// </returns>
    public Task<IList<Claim>> GetClaimsAsync(UserEntity user, CancellationToken cancellationToken)
    {
      IList<Claim> claims = new List<Claim>();

      claims.Add(new Claim(JwtClaimTypes.PreferredUserName, user.Name!));
      claims.Add(new Claim(JwtClaimTypes.EmailVerified, "true"));
      claims.Add(new Claim(JwtClaimTypes.Scope, "identity-server-sample-api-scope"));

      return Task.FromResult(claims);
    }

    /// <summary>
    /// Add claims to a user as an asynchronous operation.
    /// </summary>
    /// <param name="user">The user to add the claim to.</param>
    /// <param name="claims">The collection of <see cref="Claim"/>s to add.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task AddClaimsAsync(UserEntity user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Replaces the given <paramref name="claim"/> on the specified <paramref name="user"/> with the <paramref name="newClaim"/>
    /// </summary>
    /// <param name="user">The user to replace the claim on.</param>
    /// <param name="claim">The claim to replace.</param>
    /// <param name="newClaim">The new claim to replace the existing <paramref name="claim"/> with.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task ReplaceClaimAsync(UserEntity user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Removes the specified <paramref name="claims"/> from the given <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user to remove the specified <paramref name="claims"/> from.</param>
    /// <param name="claims">A collection of <see cref="Claim"/>s to remove.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task RemoveClaimsAsync(UserEntity user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    /// <summary>
    /// Returns a list of users who contain the specified <see cref="Claim"/>.
    /// </summary>
    /// <param name="claim">The claim to look for.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that represents the result of the asynchronous query, a list of <typeparamref name="TUser"/> who
    /// contain the specified claim.
    /// </returns>
    public Task<IList<UserEntity>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
      => throw new NotImplementedException();

    #endregion

    #region Members of IDisposable

    /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
    public void Dispose() { }

    #endregion
  }
}
