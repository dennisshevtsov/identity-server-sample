﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.IdentityApp.Defaults;
  using IdentityServerSample.IdentityApp.Dtos;

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

    [HttpGet(Name = nameof(UserController.GetUser))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetUserResponseDto), StatusCodes.Status200OK)]
    public IActionResult GetUser(GetUserRequestDto query)
    {
      return Ok();
    }

    [HttpPost(Name = nameof(UserController.CreateUser))]
    [ProducesResponseType(typeof(CreateUserResponseDto), StatusCodes.Status200OK)]
    [Consumes(typeof(CreateUserRequestDto), ContentType.Json)]
    public IActionResult CreateUser(CreateUserRequestDto command)
    {
      return Ok();
    }

    [HttpPut(Name = nameof(UserController.UpdateUser))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes(typeof(UpdateUserRequestDto), ContentType.Json)]
    public IActionResult UpdateUser(UpdateUserRequestDto commad)
    {
      return NoContent();
    }

    [HttpDelete(Name = nameof(UserController.DeleteUser))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes(typeof(DeleteUserRequestDto), ContentType.Json)]
    public IActionResult DeleteUser(DeleteUserRequestDto command)
    {
      return NoContent();
    }
  }
}
