// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.IdentityApp.Defaults;
  using IdentityServerSample.IdentityApp.Dtos;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route("api/client")]
  [Produces(ContentType.Json)]
  public sealed class ClientController : ControllerBase
  {
    [HttpGet(Name = nameof(ClientController.GetClients))]
    [ProducesResponseType(typeof(GetClientsResponseDto), StatusCodes.Status200OK)]
    public IActionResult GetClients(GetClientsRequestDto query)
    {
      return Ok();
    }

    [HttpGet(Name = nameof(ClientController.GetClient))]
    [ProducesResponseType(typeof(GetClientResponseDto), StatusCodes.Status200OK)]
    public IActionResult GetClient(GetClientRequestDto query)
    {
      return Ok();
    }

    [HttpPost(Name = nameof(ClientController.CreateClient))]
    [ProducesResponseType(typeof(CreateClientResponseDto), StatusCodes.Status200OK)]
    public IActionResult CreateClient(CreateClientRequestDto command)
    {
      return Ok();
    }

    [HttpPut(Name = nameof(ClientController.UpdateClient))]
    public IActionResult UpdateClient(UpdateClientRequestDto commad)
    {
      return NoContent();
    }

    [HttpDelete(Name = nameof(ClientController.DeleteClient))]
    public IActionResult DeleteClient(DeleteClientRequestDto command)
    {
      return NoContent();
    }
  }
}
