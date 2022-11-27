// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using IdentityServer4.Services;
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.IdentityApp.ViewModels;
  using IdentityServerSample.IdentityApp.Defaults;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route(Routing.ErrorRoute)]
  public sealed class ErrorController : Controller
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
