// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApp.Controllers
{
  using IdentityServer4.Models;
  using IdentityServer4.Services;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.WebApp.Defaults;
  using IdentityServerSample.WebApp.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route(Routing.AccountRoute)]
  public sealed class SignOutController : Controller
  {
    private readonly IIdentityServerInteractionService _identityServerInteractionService;

    /// <summary>Inititalizes a new instance of the <see cref="IdentityServerSample.WebApp.Controllers.SignOutController"/> class.</summary>
    /// <param name="userStore">An object that represents a store for test users.</param>
    /// <param name="identityServerInteractionService">An object that provides a simple API to communicate with IdentityServer.</param>
    public SignOutController(
      IIdentityServerInteractionService identityServerInteractionService)
    {
      _identityServerInteractionService = identityServerInteractionService ??
        throw new ArgumentNullException(nameof(identityServerInteractionService));
    }

    /// <summary>Handles the get sign-out page request.</summary>
    /// <param name="vm">An object that represents details of a sing-out request.</param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet(Routing.SignOutRoute)]
    public async Task<IActionResult> Get(SignOutViewModel vm)
    {
      var logoutRequest =
        await _identityServerInteractionService.GetLogoutContextAsync(vm.SignOutId)!;

      if (logoutRequest.ShowSignoutPrompt)
      {
        return View("SignOutView", vm);
      }

      return await SignOut(logoutRequest);
    }

    /// <summary>Handles the post sign-out request.</summary>
    /// <param name="vm">An object that represents details of a sing-out request.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    [ValidateAntiForgeryToken]
    [HttpPost(Routing.SignOutRoute)]
    public async Task<IActionResult> Post(SignOutViewModel vm)
    {
      var logoutRequest =
        await _identityServerInteractionService.GetLogoutContextAsync(vm.SignOutId)!;

      return await SignOut(logoutRequest);
    }

    private async Task<IActionResult> SignOut(LogoutRequest logoutRequest)
    {
      await HttpContext.SignOutAsync();

      return Redirect(logoutRequest.PostLogoutRedirectUri);
    }
  }
}
