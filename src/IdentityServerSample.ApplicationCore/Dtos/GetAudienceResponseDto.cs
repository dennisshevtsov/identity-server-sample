// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Dtos
{
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Represents details of an audience.</summary>
  public sealed class GetAudienceResponseDto : IAudienceIdentity
  {
    /// <summary>Gets/sets an object that represents an ID of an audience.</summary>
    public string? AudienceName { get; set; }

    /// <summary>Gets/sets an object that repesents a display name of an audience.</summary>
    public string? DisplayName { get; set; }
  }
}
