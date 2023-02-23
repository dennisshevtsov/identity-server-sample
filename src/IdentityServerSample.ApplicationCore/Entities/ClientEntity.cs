// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Entities
{
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Represents details of a client.</summary>
  public sealed class ClientEntity : IClientIdentity
  {
    /// <summary>Gets/sets an object that represents a name of a client.</summary>
    public string? ClientName { get; set; }

    /// <summary>Gets/sets an object that represents a display name of a client.</summary>
    public string? DisplayName { get; set; }

    /// <summary>Gets/sets an object that represents a description of a client.</summary>
    public string? Description { get; set; }

    /// <summary>Gets/sets an object that represents a collection of allowed scopes.</summary>
    public IReadOnlyList<string>? Scopes { get; set; }

    /// <summary>Gets/sets an object that represents a collection of redirect URIs.</summary>
    public IReadOnlyList<string>? RedirectUris { get; set; }

    /// <summary>Gets/sets an object that represents a collection of post-redirect URIs.</summary>
    public IReadOnlyList<string>? PostRedirectUris { get; set; }

    /// <summary>Gets/sets an object that represents a collection of CORS origins.</summary>
    public IReadOnlyList<string>? CorsOrigins { get; set; }
  }
}
