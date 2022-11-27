﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using System;

  using IdentityServer4;
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

    /// <summary>Inititalizes a new instance of the <see cref="IdentityServerSample.IdentityApp.Controllers.AccountController"/> class.</summary>
    /// <param name="userStore">An object that represents a store for test users.</param>
    public AccountController(TestUserStore userStore)
    {
      _userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
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
    public IActionResult GetSignOut(SignOutViewModel vm)
    {
      return View("SignOutView", vm);
    }

    /// <summary>Handles the post sign-out request.</summary>
    /// <param name="vm">An object that represents details of a sing-out request.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    [HttpPost(Routing.SignOutRoute)]
    public async Task<IActionResult> PostSignOut(SignOutViewModel vm)
    {
      await HttpContext.SignOutAsync();

      return Redirect(vm.ReturnUrl!);
    }
  }
}
