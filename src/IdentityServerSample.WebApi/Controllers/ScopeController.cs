﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Controllers
{
  using System;

  using AutoMapper;
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Services;
  using IdentityServerSample.WebApi.Defaults;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route("api/scope")]
  [Produces(ContentType.Json)]
  public sealed class ScopeController : ControllerBase
  {
    private readonly IScopeService _scopeService;
    private readonly IMapper _mapper;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.WebApi.Controllers.ScopeController"/> class.</summary>
    /// <param name="scopeService">An object that provides a simple API to query and save scopes.</param>
    /// <param name="mapper">An object that provides a simple API to map objects of different types.</param>
    public ScopeController(IScopeService scopeService, IMapper mapper)
    {
      _scopeService = scopeService ?? throw new ArgumentNullException(nameof(scopeService));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>Handles the GET request.</summary>
    /// <param name="requestDto">An object that represents conditions to query scopes.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    [HttpGet(Name = nameof(ScopeController.GetScopes))]
    [ProducesResponseType(typeof(GetScopesResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetScopes([FromRoute] GetScopesRequestDto requestDto, CancellationToken cancellationToken)
    {
      var scopeEntityCollection = await _scopeService.GetScopesAsync(cancellationToken);
      var getScopesResponseDto = _mapper.Map<GetScopesResponseDto>(scopeEntityCollection);

      return Ok(getScopesResponseDto);
    }

    /// <summary>Handles the GET request.</summary>
    /// <param name="requestDto">An object that represents conditions to query a scope.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    [HttpGet("{scopeName}", Name = nameof(ScopeController.GetScope))]
    [ProducesResponseType(typeof(GetScopeResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetScope([FromRoute] GetScopeRequestDto requestDto, CancellationToken cancellationToken)
    {
      var scopeEntityCollection = await _scopeService.GetScopeAsync(requestDto, cancellationToken);
      var getScopeResponseDto = _mapper.Map<GetScopeResponseDto>(scopeEntityCollection);

      return Ok(getScopeResponseDto);
    }

    /// <summary>Handles the POST request.</summary>
    /// <param name="requestDto">An object that represents data to create a new scope.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    [HttpPost(Name = nameof(ScopeController.AddScope))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddScope([FromBody] AddScopeRequestDto requestDto, CancellationToken cancellationToken)
    {
      await _scopeService.AddScopeAsync(requestDto, cancellationToken);

      return CreatedAtRoute(
        nameof(ScopeController.GetScope),
        new GetScopeRequestDto { ScopeName = requestDto.ScopeName },
        requestDto);
    }
  }
}
