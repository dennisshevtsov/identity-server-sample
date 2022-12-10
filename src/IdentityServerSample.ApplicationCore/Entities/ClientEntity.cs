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

    public string? Name { get; set; }
  }
}
