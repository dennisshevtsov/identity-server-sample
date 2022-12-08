// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.WebApi.Defaults;
  using IdentityServerSample.WebApi.Dtos;

  /// <summary>Provides a simple API to handle HTTP requests.<</summary>
  [ApiController]
  [Route(Routing.TaskRoute)]
  [Produces(ContentType.Json)]
  public sealed class TaskController : ControllerBase
  {
    [HttpGet(Name = nameof(TaskController.GetTasks))]
    [ProducesResponseType(typeof(GetTasksResponseDto), StatusCodes.Status200OK)]
    public IActionResult GetTasks()
    {
      return Ok();
    }
  }
}
