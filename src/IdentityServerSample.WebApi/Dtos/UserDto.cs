// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Dtos
{
  /// <summary>Represents details of a user.</summary>
  public sealed class UserDto
  {
    /// <summary>Gets/sets an object that represents a name of a user.</summary>
    public string? Name { get; set; }

    /// <summary>Gets/sets an object that represents a collection of claims.</summary>
    public ClaimDto[]? Claims { get; set; }
  }
}
