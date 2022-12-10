// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Identities
{
  /// <summary>Represents an identity of a user.</summary>
  public interface IUserIdentity
  {
    /// <summary>Gets/sets an object that reprsents an ID of a user.</summary>
    public Guid UserId { get; set; }
  }
}
