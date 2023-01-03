// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Dtos
{
  /// <summary>Represents data to sign out an account.</summary>
  public sealed class SignOutAccountRequestDto
  {
    /// <summary>Gets/sets an object that represents a sign out ID.</summary>
    public string? SignOutId { get; set; }
  }
}
