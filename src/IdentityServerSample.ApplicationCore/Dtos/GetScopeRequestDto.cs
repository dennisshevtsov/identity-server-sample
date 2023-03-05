// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Dtos
{
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Represents conditions to query a scope.</summary>
  public sealed class GetScopeRequestDto : IScopeIdentity
  {
    /// <summary>Gets/sets an object that represents a name of a scope.</summary>
    public string? ScopeName { get; set; }
  }
}
