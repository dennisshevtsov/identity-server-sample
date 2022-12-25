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
  [Route("api/audience")]
  [Produces(ContentType.Json)]
  public sealed class AudienceController : ControllerBase
  {
    /// <summary>Handles the get audiences query request.</summary>
    /// <param name="query">An oject that represents conditions to query audiences.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    [HttpGet(Name = nameof(AudienceController.GetResources))]
    [ProducesResponseType(typeof(GetAudiencesResponseDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetResources(GetAudiencesRequestDto query, CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }
  }
}
