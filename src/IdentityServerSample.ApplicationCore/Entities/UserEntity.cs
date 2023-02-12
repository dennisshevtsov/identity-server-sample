// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Entities
{
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Represents details of a user.</summary>
  public sealed class UserEntity : IAccountIdentity
  {
    /// <summary>Gets/sets an object that represents an ID of an account.</summary>
    public Guid AccountId { get; set; }

    /// <summary>Gets/sets an object that represents a name of a user.</summary>
    public string? Name { get; set; }

    /// <summary>Gets/sets an object that represents an email of a user.</summary>
    public string? Email { get; set; }

    /// <summary>Gets/sets an object that represents a password hash of a user.</summary>
    public string? PasswordHash { get; set; }
  }
}
