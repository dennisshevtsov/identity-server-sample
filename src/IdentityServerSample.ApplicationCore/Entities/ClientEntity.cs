// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Entities
{
  /// <summary>Represents details of a client.</summary>
  public sealed class ClientEntity
  {
    /// <summary>Gets/sets an object that represents an ID of a client.</summary>
    public string? ClientId { get; set; }

    /// <summary>Gets/sets an object that represents a name of a client.</summary>
    public string? Name { get; set; }

    /// <summary>Gets/sets an object that represents a display name of a client.</summary>
    public string? DisplayName { get; set; }

    /// <summary>Gets/sets an object that represents a description of a client.</summary>
    public string? Description { get; set; }

    /// <summary>Gets/sets an object that represents a collection of allowed scopes.</summary>
    public IEnumerable<LiteralEmbeddedEntity>? Scopes { get; set; }

    /// <summary>Gets/sets an object that represents a collection of redirect URIs.</summary>
    public IEnumerable<LiteralEmbeddedEntity>? RedirectUris { get; set; }

    /// <summary>Gets/sets an object that represents a collection of post-redirect URIs.</summary>
    public IEnumerable<LiteralEmbeddedEntity>? PostRedirectUris { get; set; }
  }
}
