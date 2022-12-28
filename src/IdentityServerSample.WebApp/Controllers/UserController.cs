// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApp.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.WebApp.Defaults;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route("api/user")]
  [Produces(ContentType.Json)]
  public sealed class UserController : ControllerBase
  {
    [HttpGet(Name = nameof(UserController.GetUsers))]
    [ProducesResponseType(typeof(GetUsersResponseDto), StatusCodes.Status200OK)]
    public IActionResult GetUsers(GetUsersRequestDto query)
    {
      return Ok();
    }
  }
}
