// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using IdentityServer4;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.IdentityApp.ViewModels;

  [Route("account")]
  public class AccountController : Controller
  {
    [HttpGet("sign-in")]
    public IActionResult GetSignIn(SignInViewModel vm)
    {
      ModelState.Clear();

      return View("SignInView", vm);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> PostSignIn(SignInViewModel vm)
    {
      if (!ModelState.IsValid)
      {
        return View("SignInView", vm);
      }

      await HttpContext.SignInAsync(new IdentityServerUser("test")
      {
        DisplayName = "test",
      });

      return Redirect(vm.ReturnUrl!);
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
