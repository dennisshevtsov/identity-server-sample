// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Identities
{
  /// <summary>Represents an identity of an identity resource.</summary>
  public interface IIdentityResourceIdentity
  {
    /// <summary>Gets/sets an object that reprsents a name of an identity resource.</summary>
    public string? Name { get; set; }
  }
}
