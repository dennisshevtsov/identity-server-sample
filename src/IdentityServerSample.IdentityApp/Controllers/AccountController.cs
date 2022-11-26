// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using IdentityServer4;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.IdentityApp.ViewModels;
  using IdentityServer4.Test;
  using System;

  [Route("account")]
  public class AccountController : Controller
  {
    private readonly TestUserStore _userStore;

    public AccountController(TestUserStore userStore)
    {
      _userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
    }

    [HttpGet("sign-in")]
    public IActionResult GetSignIn(SignInViewModel vm)
    {
      ModelState.Clear();

      return View("SignInView", vm);
    }

    [HttpPost("sign-in")]
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

        ModelState.AddModelError(string.Empty, "The credentials is not valid.");
      }

      return View("SignInView", vm);
    }

    [HttpGet("sign-out")]
    public IActionResult GetSignOut(SignOutViewModel vm)
    {
      return View("SignOutView", vm);
    }

    [HttpPost("sign-out")]
    public async Task<IActionResult> PostSignOut(SignOutViewModel vm)
    {
      await HttpContext.SignOutAsync();

      return Redirect(vm.ReturnUrl!);
    }
  }
}
