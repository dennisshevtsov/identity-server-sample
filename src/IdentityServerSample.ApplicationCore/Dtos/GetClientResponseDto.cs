// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Dtos
{
  using IdentityServerSample.ApplicationCore.Identities;

  public sealed class GetClientResponseDto : IClientIdentity
  {
    /// <summary>Gets/sets an object that reprsents a name of a client.</summary>
    public string? ClientName { get; set; }

    /// <summary>Gets/sets an object that repesents a display name of a client.</summary>
    public string? DisplayName { get; set; }

    /// <summary>Gets/sets an object that repesents a desciption of a client.</summary>
    public string? Desciption { get; set; }
  }
}
