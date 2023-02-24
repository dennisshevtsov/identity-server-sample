// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Entities
{
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Represents details of an audience.</summary>
  public sealed class AudienceScopeEntity : IAudienceIdentity, IScopeIdentity
  {
    /// <summary>Gets/sets an object that represents a name of an audience.</summary>
    public string? AudienceName { get; set; }

    /// <summary>Gets/sets an object that represents a name of a scope.</summary>
    public string? ScopeName { get; set; }
  }
}
