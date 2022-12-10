﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Dtos
{
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Represents data to update a user.</summary>
  public sealed class UpdateUserRequestDto : IUserIdentity
  {
    /// <summary>Gets/sets an object that reprsents an ID of a user.</summary>
    public Guid UserId { get; set; }
  }
}
