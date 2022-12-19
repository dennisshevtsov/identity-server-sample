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

    /// <summary>Handles the get clients query request.</summary>
    /// <param name="query">An oject that represents conditions to query clients.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    [HttpGet(Name = nameof(ClientController.GetClients))]
    [ProducesResponseType(typeof(GetClientsResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClients(GetClientsRequestDto query, CancellationToken cancellationToken)
    {
      return Ok(await _clientService.GetClientsAsync(query, cancellationToken));
    }
  }
}
