// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using System;

  using IdentityServer4;
  using IdentityServer4.Models;
  using IdentityServer4.Services;
  using IdentityServer4.Test;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.IdentityApp.Defaults;
  using IdentityServerSample.IdentityApp.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [Route(Routing.AccountRoute)]
  public class AccountController : Controller
  {
    private readonly TestUserStore _userStore;
    private readonly IIdentityServerInteractionService _identityServerInteractionService;

    /// <summary>Inititalizes a new instance of the <see cref="IdentityServerSample.IdentityApp.Controllers.AccountController"/> class.</summary>
    /// <param name="userStore">An object that represents a store for test users.</param>
    /// <param name="identityServerInteractionService">An object that provides a simple API to communicate with IdentityServer.</param>
    public AccountController(
      TestUserStore userStore,
      IIdentityServerInteractionService identityServerInteractionService)
    {
      _userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
      _identityServerInteractionService = identityServerInteractionService ??
        throw new ArgumentNullException(nameof(identityServerInteractionService));
    }

    /// <summary>Handles the get sign-in page request.</summary>
    /// <param name="vm">An object that represents details of sign-in credentials.</param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet(Routing.SignInRoute)]
    public IActionResult GetSignIn(SignInViewModel vm)
    {
      ModelState.Clear();

      return View("SignInView", vm);
    }

    /// <summary>Handles the post sign-in credentials request.</summary>
    /// <param name="vm">An object that represents details of sign-in credentials.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    [HttpPost(Routing.SignInRoute)]
    public async Task<IActionResult> PostSignIn(SignInViewModel vm)
    {
      if (ModelState.IsValid)
      {
        if (_userStore.ValidateCredentials(vm.Email, vm.Password))
        {
          var testUser = _userStore.FindByUsername(vm.Email);
          var identityServerUser = new IdentityServerUser(testUser.SubjectId)
          {
            DisplayName = testUser.Username,
          };

          await HttpContext.SignInAsync(identityServerUser);

          return Redirect(vm.ReturnUrl!);
        }

        ModelState.AddModelError(
          nameof(SignInViewModel.Email), "The credentials is not valid.");
      }

      return View("SignInView", vm);
    }

    /// <summary>Handles the get sign-out page request.</summary>
    /// <param name="vm">An object that represents details of a sing-out request.</param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet(Routing.SignOutRoute)]
    public async Task<IActionResult> GetSignOut(SignOutViewModel vm)
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
    [HttpPost(Routing.SignOutRoute)]
    public async Task<IActionResult> PostSignOut(SignOutViewModel vm)
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
