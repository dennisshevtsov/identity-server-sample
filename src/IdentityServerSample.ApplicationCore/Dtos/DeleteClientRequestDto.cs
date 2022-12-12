// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Dtos
{
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Represents data to delete a client.</summary>
  public sealed class DeleteClientRequestDto : IClientIdentity
  {
    /// <summary>Gets/sets an object that reprsents an ID of a client.</summary>
    public string? ClientId { get; set; }
  }
}
