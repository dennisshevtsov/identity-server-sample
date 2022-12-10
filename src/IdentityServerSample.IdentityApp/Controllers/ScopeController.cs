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
  [Route("api/scope")]
  [Produces(ContentType.Json)]
  public sealed class ScopeController : ControllerBase
  {
    [HttpGet(Name = nameof(ScopeController.GetScopes))]
    [ProducesResponseType(typeof(GetScopesResponseDto), StatusCodes.Status200OK)]
    public IActionResult GetScopes(GetScopesRequestDto query)
    {
      return Ok();
    }

    [HttpGet(Name = nameof(ScopeController.GetScope))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetScopeResponseDto), StatusCodes.Status200OK)]
    public IActionResult GetScope(GetScopeRequestDto query)
    {
      return Ok();
    }

    [HttpPost(Name = nameof(ScopeController.CreateScope))]
    [ProducesResponseType(typeof(CreateScopeResponseDto), StatusCodes.Status200OK)]
    [Consumes(typeof(CreateScopeRequestDto), ContentType.Json)]
    public IActionResult CreateScope(CreateScopeRequestDto command)
    {
      return Ok();
    }

    [HttpPut(Name = nameof(ScopeController.UpdateScope))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes(typeof(UpdateScopeRequestDto), ContentType.Json)]
    public IActionResult UpdateScope(UpdateScopeRequestDto commad)
    {
      return NoContent();
    }

    [HttpDelete(Name = nameof(ScopeController.DeleteScope))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes(typeof(DeleteScopeRequestDto), ContentType.Json)]
    public IActionResult DeleteScope(DeleteScopeRequestDto command)
    {
      return NoContent();
    }
  }
}
