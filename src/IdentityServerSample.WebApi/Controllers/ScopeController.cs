// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApp.Controllers
{
  using System;

  using AutoMapper;
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Services;
  using IdentityServerSample.WebApp.Defaults;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route("api/scope")]
  [Produces(ContentType.Json)]
  public sealed class ScopeController : ControllerBase
  {
    private readonly IScopeService _scopeService;
    private readonly IMapper _mapper;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.WebApp.Controllers.ScopeController"/> class.</summary>
    /// <param name="scopeService">An object that provides a simple API to query and save scopes.</param>
    /// <param name="mapper">An object that provides a simple API to map objects of different types.</param>
    public ScopeController(IScopeService scopeService, IMapper mapper)
    {
      _scopeService = scopeService ?? throw new ArgumentNullException(nameof(scopeService));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet(Name = nameof(ScopeController.GetScopes))]
    [ProducesResponseType(typeof(GetScopesResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetScopes([FromRoute] GetScopesRequestDto query, CancellationToken cancellationToken)
    {
      var scopeEntityCollection = await _scopeService.GetScopesAsync(cancellationToken);
      var getScopesResponseDto = _mapper.Map<GetScopesResponseDto>(scopeEntityCollection);

      return Ok(getScopesResponseDto);
    }
  }
}
