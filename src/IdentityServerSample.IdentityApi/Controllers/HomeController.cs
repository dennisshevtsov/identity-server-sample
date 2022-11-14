// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using IdentityServer4.Services;

  public sealed class HomeController : ControllerBase
  {
    private readonly IIdentityServerInteractionService _identityServerInteractionService;

    public HomeController(
      IIdentityServerInteractionService identityServerInteractionService)
    {
      _identityServerInteractionService = identityServerInteractionService ??
        throw new ArgumentNullException(nameof(identityServerInteractionService));
    }

    public async Task<IActionResult> Error(string errosId)
    {
      return Ok(await _identityServerInteractionService.GetErrorContextAsync(errosId));
    }
  }
}
