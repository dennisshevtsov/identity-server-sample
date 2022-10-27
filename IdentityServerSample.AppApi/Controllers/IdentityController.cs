// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.AppApi.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  [Route("api/identity")]
  public sealed class IdentityController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(new
      {
        name = User.Identity?.Name,
        claims = User.Claims.Select(claim => claim.Value).ToArray(),
      });
    }
  }
}
