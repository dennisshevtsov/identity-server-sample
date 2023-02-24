// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Identities
{
  /// <summary>Represents an identity of a client.</summary>
  public interface IClientIdentity
  {
    /// <summary>Gets/sets an object that reprsents a name of a client.</summary>
    public string? ClientName { get; set; }
  }
}
