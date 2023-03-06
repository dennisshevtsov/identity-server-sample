﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApp.Controllers
{
  using System;
  using System.Threading;

  using AutoMapper;
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Services;
  using IdentityServerSample.WebApp.Defaults;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route("api/client")]
  [Produces(ContentType.Json)]
  public sealed class ClientController : ControllerBase
  {
    private readonly IClientService _clientService;

    private readonly IMapper _mapper;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.WebApp.Controllers.ClientController"/> class.</summary>
    /// <param name="clientService">An object that provides a simple API to execute queries and commands with clients.</param>
    /// <param name="mapper">An object that provides a simple API to map objects of different types.</param>
    public ClientController(IClientService clientService, IMapper mapper)
    {
      _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>Handles the get clients query request.</summary>
    /// <param name="requestDto">An oject that represents conditions to query clients.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that produces a result at some time in the future.</returns>
    [HttpGet(Name = nameof(ClientController.GetClients))]
    [ProducesResponseType(typeof(GetClientsResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClients([FromRoute] GetClientsRequestDto requestDto, CancellationToken cancellationToken)
    {
      return Ok(await _clientService.GetClientsAsync(requestDto, cancellationToken));
    }

    /// <summary>Handles the get client query request.</summary>
    /// <param name="requestDto">An oject that represents conditions to query client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that produces a result at some time in the future.</returns>
    [HttpGet("{clientName", Name = nameof(ClientController.GetClient))]
    [ProducesResponseType(typeof(GetClientResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClient([FromRoute] GetClientRequestDto requestDto, CancellationToken cancellationToken)
    {
      var clientEntity = await _clientService.GetClientAsync(requestDto, cancellationToken);

      if (clientEntity == null)
      {
        return NotFound();
      }

      var getClientResponseDto = _mapper.Map<GetClientResponseDto>(clientEntity);

      return Ok(getClientResponseDto);
    }
  }
}
