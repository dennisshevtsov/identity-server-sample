// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using System;

  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Services;
  using IdentityServerSample.IdentityApp.Defaults;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route("api/audience")]
  [Produces(ContentType.Json)]
  public sealed class AudienceController : ControllerBase
  {
    private readonly IAudienceService _audienceService;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.IdentityApp.Controllers.AudienceController"/> class.</summary>
    /// <param name="audienceService">An object that provides a simple API to execute audience queries and commands.</param>
    public AudienceController(IAudienceService audienceService)
    {
      _audienceService = audienceService ?? throw new ArgumentNullException(nameof(audienceService));
    }

    /// <summary>Handles the get audiences query request.</summary>
    /// <param name="query">An oject that represents conditions to query audiences.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    [HttpGet(Name = nameof(AudienceController.GetAudiences))]
    [ProducesResponseType(typeof(GetAudiencesResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAudiences([FromRoute] GetAudiencesRequestDto query, CancellationToken cancellationToken)
    {
      return Ok(await _audienceService.GetAudiencesAsync(query, cancellationToken));
    }
  }
}
