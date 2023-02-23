// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using IdentityServerSample.ApplicationCore.Identities;

namespace IdentityServerSample.ApplicationCore.Dtos
{
  /// <summary>Represents a response to the get audiences request.</summary>
  public sealed class GetAudiencesResponseDto
  {
    /// <summary>Gets/sets an object that represents a collection of audiences.</summary>
    public AudienceDto[]? Audiences { get; set; }

    /// <summary>Represents details of an audience.</summary>
    public sealed class AudienceDto : IAudienceIdentity
    {
      /// <summary>Gets/sets an object that reprsents a name of an audience.</summary>
      public string? AudienceName { get; set; }

      /// <summary>Gets/sets an object that repesents a display name of an audience.</summary>
      public string? DisplayName { get; set; }
    }
  }
}
