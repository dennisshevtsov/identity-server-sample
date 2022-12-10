// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.IdentityApp.Defaults;

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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetClientResponseDto), StatusCodes.Status200OK)]
    public IActionResult GetClient(GetClientRequestDto query)
    {
      return Ok();
    }

    [HttpPost(Name = nameof(ClientController.CreateClient))]
    [ProducesResponseType(typeof(CreateClientResponseDto), StatusCodes.Status200OK)]
    [Consumes(typeof(CreateClientRequestDto), ContentType.Json)]
    public IActionResult CreateClient(CreateClientRequestDto command)
    {
      return Ok();
    }

    [HttpPut(Name = nameof(ClientController.UpdateClient))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes(typeof(UpdateClientRequestDto), ContentType.Json)]
    public IActionResult UpdateClient(UpdateClientRequestDto commad)
    {
      return NoContent();
    }

    [HttpDelete(Name = nameof(ClientController.DeleteClient))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes(typeof(DeleteClientRequestDto), ContentType.Json)]
    public IActionResult DeleteClient(DeleteClientRequestDto command)
    {
      return NoContent();
    }
  }
}
