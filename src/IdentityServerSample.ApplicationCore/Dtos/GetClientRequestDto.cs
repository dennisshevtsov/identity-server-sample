﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using IdentityServerSample.ApplicationCore.Identities;

namespace IdentityServerSample.ApplicationCore.Dtos
{
  /// <summary>Represents conditions to query a client.</summary>
  public sealed class GetClientRequestDto : IClientIdentity
  {
    /// <summary>Gets/sets an object that reprsents an ID of a client.</summary>
    public Guid ClientId { get; set; }
  }
}
