// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Identities
{
  /// <summary>Represents an identity of a scope.</summary>
  public interface IScopeIdentity
  {
    /// <summary>Gets/sets an object that represents a name of a scope.</summary>
    public string? ScopeName { get; set; }
  }
}
