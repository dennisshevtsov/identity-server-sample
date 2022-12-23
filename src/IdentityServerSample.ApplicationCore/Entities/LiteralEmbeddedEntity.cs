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

    /// <summary>Creates an instance of the <see cref="IdentityServerSample.ApplicationCore.Entities.LiteralEmbeddedEntity"/> from a string literal.</summary>
    /// <param name="value">An object that represents a string literal.</param>
    public static implicit operator LiteralEmbeddedEntity(string? value)
      => new() { Value = value };
  }
}
