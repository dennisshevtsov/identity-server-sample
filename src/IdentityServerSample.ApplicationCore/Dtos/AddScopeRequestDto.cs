// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Dtos
{
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Represents data to create a new scope.</summary>
  public sealed class AddScopeRequestDto : IScopeIdentity
  {
    /// <summary>Gets/sets an object that represents a name of a scope.</summary>
    public string? ScopeName { get; set; }

    /// <summary>Gets/sets an object that represents a display name of a scope.</summary>
    public string? DisplayName { get; set; }

    /// <summary>Gets/sets an object that represents a description of a scope.</summary>
    public string? Description { get; set; }
  }
}
