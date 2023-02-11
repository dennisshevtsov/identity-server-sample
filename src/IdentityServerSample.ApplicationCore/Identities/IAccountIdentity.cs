// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Identities
{
  /// <summary>Represents an identity of an account.</summary>
  public interface IAccountIdentity
  {
    /// <summary>Gets/sets an object that reprsents a name of an account.</summary>
    public Guid AccountId { get; set; }
  }
}
