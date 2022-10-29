// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Api.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  [Route("api/identity")]
  public sealed class IdentityController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(new UserDto
      {
        Name = User.Identity?.Name,
        Claims = User.Claims.Select(claim => claim.Value).ToArray(),
      });
    }

    public sealed class UserDto
    {
      public string? Name { get; set; }

      public string[]? Claims { get; set; }
    }
  }
}
