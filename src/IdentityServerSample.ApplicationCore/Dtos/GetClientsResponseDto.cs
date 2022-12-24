// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Dtos
{
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Represents a response to the get clients request.</summary>
  public sealed class GetClientsResponseDto
  {
    /// <summary>Gets/sets an object that represents a collection of clients.</summary>
    public ClientDto[]? Clients { get; set; }

    /// <summary>Represents details of a client.</summary>
    public sealed class ClientDto : IClientIdentity
    {
      /// <summary>Gets/sets an object that reprsents a name of a client.</summary>
      public string? Name { get; set; }

      /// <summary>Gets/sets an object that repesents a display name of a client.</summary>
      public string? DisplayName { get; set; }
    }
  }
}
