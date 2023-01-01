// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using IdentityServerSample.IdentityApp.Dtos;
  using Microsoft.AspNetCore.Mvc;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route("api/account")]
  [Produces("application/json")]
  public sealed class AccountController : ControllerBase
  {
    [HttpPost("signin", Name = nameof(AccountController.SingInAccount))]
    public IActionResult SingInAccount([FromBody]SingInAccountRequestDto command)
    {
      return Ok();
    }

    [HttpPost("singout", Name = nameof(AccountController.SingOutAccount))]
    public IActionResult SingOutAccount()
    {
      return Ok();
    }
  }
}
