// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Controllers
{
  using IdentityServer4;
  using Microsoft.AspNetCore.Mvc;

  public sealed class AccountController : ControllerBase
  {
    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl)
    {
      await HttpContext.SignInAsync(new IdentityServerUser("test")
      {
        DisplayName = "test",
      });

      return Redirect(returnUrl);
    }
  }
}
