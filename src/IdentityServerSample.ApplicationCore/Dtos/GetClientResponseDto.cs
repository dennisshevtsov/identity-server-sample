// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Dtos
{
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Represents details of a client.</summary>
  public sealed class GetClientResponseDto : IClientIdentity
  {
    /// <summary>Gets/sets an object that reprsents an ID of a client.</summary>
    public string? ClientId { get; set; }

    /// <summary>Gets/sets an object that reprsents a name of a client.</summary>
    public string? Name { get; set; }

    /// <summary>Gets/sets an object that reprsents a display name of a client.</summary>
    public string? DisplayName { get; set; }

    /// <summary>Gets/sets an object that reprsents a description of a client.</summary>
    public string? Description { get; set; }
  }
}
