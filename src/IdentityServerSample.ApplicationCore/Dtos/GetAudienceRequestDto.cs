// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Dtos
{
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Represents conditions to query an audience.</summary>
  public sealed class GetAudienceRequestDto : IAudienceIdentity
  {
    /// <summary>Gets/sets an object that represents an ID of an audience.</summary>
    public string? AudienceName { get; set; }
  }
}
