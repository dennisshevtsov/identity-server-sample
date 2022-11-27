// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using IdentityServer4.Services;
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.IdentityApp.ViewModels;

  [Route("error")]
  public sealed class ErrorController : Controller
  {
    private readonly IIdentityServerInteractionService _identityServerInteractionService;

    public ErrorController(
      IIdentityServerInteractionService identityServerInteractionService)
    {
      _identityServerInteractionService = identityServerInteractionService ??
        throw new ArgumentNullException(nameof(identityServerInteractionService));
    }

    [HttpGet]
    public async Task<IActionResult> GetError(ErrorViewModel vm)
    {
      var errorMessage =
        await _identityServerInteractionService.GetErrorContextAsync(vm.ErrorId!);

      if (errorMessage != null)
      {
        vm.Message = errorMessage.ErrorDescription;
      }

      return View("ErrorView", vm);
    }
  }
}
