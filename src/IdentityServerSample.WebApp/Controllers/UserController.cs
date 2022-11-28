// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.WebApi.Defaults;
  using IdentityServerSample.WebApi.Dtos;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route(Routes.UserRoute)]
  [Produces(ContentType.Json)]
  public sealed class UserController : ControllerBase
  {
    /// <summary>Handles the get authenticated user request.</summary>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet(Routes.GetAuthenticatedUserRoute, Name = nameof(UserController.Get))]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
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
