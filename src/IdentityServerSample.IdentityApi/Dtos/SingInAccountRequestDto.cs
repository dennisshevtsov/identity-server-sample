// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Dtos
{
  /// <summary>Represents data to sign in an account.</summary>
  public sealed class SingInAccountRequestDto
  {
    /// <summary>Gets/sets an object that represents an email.</summary>
    public string? Email { get; set; }

    /// <summary>Gets/sets an object that represents a password.</summary>
    public string? Password { get; set; }
  }
}
