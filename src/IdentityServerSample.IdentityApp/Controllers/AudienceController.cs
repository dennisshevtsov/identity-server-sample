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
    [HttpGet(Name = nameof(AudienceController.GetResources))]
    [ProducesResponseType(typeof(GetAudiencesResponseDto), StatusCodes.Status200OK)]
    public IActionResult GetResources(GetAudiencesRequestDto query)
    {
      return Ok();
    }
  }
}
