// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Dtos
{
  public sealed class UserDto
  {
    public string? Name { get; set; }

    public ClaimDto[]? Claims { get; set; }
  }
}
