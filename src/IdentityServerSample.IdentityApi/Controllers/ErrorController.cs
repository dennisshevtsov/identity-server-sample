// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using IdentityServer4.Services;
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.IdentityApp.Defaults;
  using IdentityServerSample.IdentityApp.Dtos;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route(Routing.ErrorRoute)]
  [Produces(ContentType.Json)]
  public sealed class ErrorController : ControllerBase
  {
    private readonly IIdentityServerInteractionService _identityServerInteractionService;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.IdentityApp.Controllers.ErrorController"/> class.</summary>
    /// <param name="identityServerInteractionService">An object that provide services be used by the user interface to communicate with IdentityServer.</param>
    public ErrorController(
      IIdentityServerInteractionService identityServerInteractionService)
    {
      _identityServerInteractionService = identityServerInteractionService ??
        throw new ArgumentNullException(nameof(identityServerInteractionService));
    }

    /// <summary>Handles the get error request.</summary>
    /// <param name="vm">An object that represents details of an error.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    [HttpGet(Routing.GetErrorRoute, Name = nameof(ErrorController.GetError))]
    public async Task<IActionResult> GetError([FromRoute] GetErrorRequestDto requestDto)
    {
      var errorMessage =
        await _identityServerInteractionService.GetErrorContextAsync(requestDto.ErrorId!);

      if (errorMessage == null)
      {
        return BadRequest();
      }

      var getErrorResponseDto = new GetErrorResponseDto
      {
        ErrorId = errorMessage.Error,
        Message = errorMessage.ErrorDescription,
      };

      return Ok(getErrorResponseDto);
    }
  }
}
