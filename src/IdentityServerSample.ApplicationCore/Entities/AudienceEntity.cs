﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Entities
{
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Represents details of an audience.</summary>
  public sealed class AudienceEntity : IAudienceIdentity
  {
    /// <summary>Gets/sets an object that represents a name of an audience.</summary>
    public string? AudienceName { get; set; }

    /// <summary>Gets/sets an object that represents a display name of an audience.</summary>
    public string? DisplayName { get; set; }

    /// <summary>Gets/sets an object that represents a description of an audience.</summary>
    public string? Description { get; set; }

    /// <summary>Gets/sets an object that represents a collection of allowed scopes.</summary>
    public IReadOnlyList<string>? Scopes { get; set; }
  }
}
