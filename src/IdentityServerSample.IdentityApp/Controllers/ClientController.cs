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
  }
}
