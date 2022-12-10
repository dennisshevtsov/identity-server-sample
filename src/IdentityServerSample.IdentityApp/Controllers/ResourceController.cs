// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.IdentityApp.Defaults;
  using IdentityServerSample.IdentityApp.Dtos;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route("api/resource")]
  [Produces(ContentType.Json)]
  public sealed class ResourceController : ControllerBase
  {
    [HttpGet(Name = nameof(ResourceController.GetResources))]
    [ProducesResponseType(typeof(GetResourcesResponseDto), StatusCodes.Status200OK)]
    public IActionResult GetResources(GetResourcesRequestDto query)
    {
      return Ok();
    }

    [HttpGet(Name = nameof(ResourceController.GetResource))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetResourceResponseDto), StatusCodes.Status200OK)]
    public IActionResult GetResource(GetResourceRequestDto query)
    {
      return Ok();
    }

    [HttpPost(Name = nameof(ResourceController.CreateResource))]
    [ProducesResponseType(typeof(CreateResourceResponseDto), StatusCodes.Status200OK)]
    [Consumes(typeof(CreateResourceRequestDto), ContentType.Json)]
    public IActionResult CreateResource(CreateResourceRequestDto command)
    {
      return Ok();
    }

    [HttpPut(Name = nameof(ResourceController.UpdateResource))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes(typeof(UpdateResourceRequestDto), ContentType.Json)]
    public IActionResult UpdateResource(UpdateResourceRequestDto commad)
    {
      return NoContent();
    }

    [HttpDelete(Name = nameof(ResourceController.DeleteResource))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes(typeof(DeleteResourceRequestDto), ContentType.Json)]
    public IActionResult DeleteResource(DeleteResourceRequestDto command)
    {
      return NoContent();
    }
  }
}
