// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Entities
{
  /// <summary>Represents details of a scope.</summary>
  public sealed class ScopeEntity
  {
    /// <summary>Gets/sets an object that represents a name of a scope.</summary>
    public string? Name { get; set; }

    /// <summary>Gets/sets an object that represents a display name of a scope.</summary>
    public string? DisplayName { get; set; }

    /// <summary>Gets/sets an object that represents a description of a scope.</summary>
    public string? Description { get; set; }

    /// <summary>Gets/sets an object that indicates if a scope is standard.</summary>
    public bool Standard { get; set; }
  }
}
