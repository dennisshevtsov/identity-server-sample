// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using IdentityServer4;
  using IdentityServerSample.IdentityApp.ViewModels;
  using Microsoft.AspNetCore.Mvc;

  public sealed class AccountController : Controller
  {
    [HttpGet]
    public IActionResult Login()
    {
      return View("LoginView", new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
      await HttpContext.SignInAsync(new IdentityServerUser("test")
      {
        DisplayName = "test",
      });

      return Redirect(vm.ReturnUrl!);
    }
  }
}
