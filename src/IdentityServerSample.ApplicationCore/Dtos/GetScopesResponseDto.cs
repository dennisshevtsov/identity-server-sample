// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using IdentityServerSample.ApplicationCore.Identities;

namespace IdentityServerSample.ApplicationCore.Dtos
{
  /// <summary>Represents a response to a get scopes request.</summary>
  public sealed class GetScopesResponseDto
  {
    /// <summary>Gets/sets a collection of scopes.</summary>
    public ScopeDto[]? Scopes { get; set; }

    /// <summary>Represents details of a scope.</summary>
    public sealed class ScopeDto : IScopeIdentity
    {
      /// <summary>Gets/sets an object that represents a name of a scope.</summary>
      public string? ScopeName { get; set; }

      /// <summary>Gets/sets an object that represents a diaplay name of a scope.</summary>
      public string? DisplayName { get; set; }
    }
  }
}
