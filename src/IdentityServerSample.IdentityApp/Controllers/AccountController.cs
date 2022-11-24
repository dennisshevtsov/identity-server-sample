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
    public IActionResult SignIn()
    {
      return View("SignInView", new SignInViewModel());
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInViewModel vm)
    {
      await HttpContext.SignInAsync(new IdentityServerUser("test")
      {
        DisplayName = "test",
      });

      return Redirect(vm.ReturnUrl!);
    }

    [HttpGet("sign-out")]
    public new IActionResult SignOut()
    {
      return View("SignOutView");
    }

    [HttpPost("sign-out")]
    public async Task<IActionResult> SignOut(SignOutViewModel vm)
    {
      await HttpContext.SignOutAsync();

      return Redirect(vm.ReturnUrl!);
    }
  }
}
