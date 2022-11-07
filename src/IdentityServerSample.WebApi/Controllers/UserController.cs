// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Api.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.WebApi.Dtos;

  [Route("api/user")]
  public sealed class UserController : ControllerBase
  {
    [HttpGet("authenticated")]
    public IActionResult Get()
    {
      return Ok(new UserDto
      {
        Name = User.Identity?.Name,
        Claims = User.Claims.Select(claim => new ClaimDto
                            {
                              Type = claim.Type,
                              Value = claim.Value,
                            })
                            .ToArray(),
      });
    }
  }
}
