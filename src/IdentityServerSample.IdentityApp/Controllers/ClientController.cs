// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using System;
  using System.Threading;

  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Services;
  using IdentityServerSample.IdentityApp.Defaults;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route("api/client")]
  [Produces(ContentType.Json)]
  public sealed class ClientController : ControllerBase
  {
    private readonly IClientService _clientService;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.IdentityApp.Controllers.ClientController"/> class.</summary>
    /// <param name="clientService">An object that provides a simple API to execute queries and commands with clients.</param>
    public ClientController(IClientService clientService)
    {
      _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
    }

    [HttpGet(Name = nameof(ClientController.GetClients))]
    [ProducesResponseType(typeof(GetClientsResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClients(GetClientsRequestDto query, CancellationToken cancellationToken)
    {
      return Ok(await _clientService.GetClientsAsync(query, cancellationToken));
    }

    [HttpGet(Name = nameof(ClientController.GetClient))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetClientResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClient(GetClientRequestDto query, CancellationToken cancellationToken)
    {
      if (string.IsNullOrWhiteSpace(query.ClientId))
      {
        return NotFound();
      }

      var clientEntity = await _clientService.GetClientAsync(query, cancellationToken);

      if (clientEntity == null)
      {
        return NotFound();
      }

      var getClientResponseDto = new GetClientResponseDto
      {
        ClientId = clientEntity.ClientId,
        Name = clientEntity.Name,
        DisplayName = clientEntity.DisplayName,
        Description = clientEntity.Description,
      };

      return Ok(getClientResponseDto);
    }

    [HttpPost(Name = nameof(ClientController.CreateClient))]
    [ProducesResponseType(typeof(CreateClientResponseDto), StatusCodes.Status200OK)]
    [Consumes(typeof(CreateClientRequestDto), ContentType.Json)]
    public async Task<IActionResult> CreateClient(CreateClientRequestDto command, CancellationToken cancellationToken)
    {
      return Ok(await _clientService.CreateClientAsync(command, cancellationToken));
    }

    [HttpPut(Name = nameof(ClientController.UpdateClient))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes(typeof(UpdateClientRequestDto), ContentType.Json)]
    public async Task<IActionResult> UpdateClient(UpdateClientRequestDto command, CancellationToken cancellationToken)
    {
      await _clientService.UpdateClientAsync(command, cancellationToken);

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
