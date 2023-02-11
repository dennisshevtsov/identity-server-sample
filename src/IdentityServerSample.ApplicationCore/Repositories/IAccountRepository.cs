// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Repositories
{
  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.AccountEntity"/> class.</summary>
  public interface IAccountRepository
  {
    /// <summary>Gets an account by an email.</summary>
    /// <param name="email">An object that represents an email of an account.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<AccountEntity?> GetAccountAsync(string email, CancellationToken cancellationToken);
  }
}
