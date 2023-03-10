// Copyright (c) Dennis Shevtsov. All rights reserved.
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
  [Route("api/audience")]
  [Produces(ContentType.Json)]
  public sealed class AudienceController : ControllerBase
  {
    private readonly IAudienceService _audienceService;

    private readonly IMapper _mapper;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.WebApi.Controllers.AudienceController"/> class.</summary>
    /// <param name="audienceService">An object that provides a simple API to execute audience queries and commands.</param>
    /// <param name="mapper">An object that provides a simple API to map objects of different types.</param>
    public AudienceController(IAudienceService audienceService, IMapper mapper)
    {
      _audienceService = audienceService ?? throw new ArgumentNullException(nameof(audienceService));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>Handles the get audiences query request.</summary>
    /// <param name="query">An oject that represents conditions to query audiences.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that produces a result at some time in the future.</returns>
    [HttpGet(Name = nameof(AudienceController.GetAudiences))]
    [ProducesResponseType(typeof(GetAudiencesResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAudiences([FromRoute] GetAudiencesRequestDto query, CancellationToken cancellationToken)
    {
      return Ok(await _audienceService.GetAudiencesAsync(query, cancellationToken));
    }

    /// <summary>Handles the get audience query request.</summary>
    /// <param name="query">An oject that represents conditions to query audiences.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that produces a result at some time in the future.</returns>
    [HttpGet("{audienceName}", Name = nameof(AudienceController.GetAudience))]
    [ProducesResponseType(typeof(GetAudienceResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAudience([FromRoute] GetAudienceRequestDto query, CancellationToken cancellationToken)
    {
      var audienceEntity = await _audienceService.GetAudienceAsync(query, cancellationToken);

      if (audienceEntity == null)
      {
        return NotFound();
      }

      var responseDto = _mapper.Map<GetAudienceResponseDto>(audienceEntity);

      return Ok(responseDto);
    }
  }
}
