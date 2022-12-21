// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Entities
{
  /// <summary>Represents a wrapper for a string leteral.</summary>
  public sealed class LiteralEmbeddedEntity
  {
    /// <summary>Gets/sets an object that represents a value of a literal.</summary>
    public string? Value { get; set; }
  }
}
