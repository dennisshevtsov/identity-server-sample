// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Controllers
{
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;

  [AllowAnonymous]
  public sealed class AccountController : ControllerBase
  {
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
      return Redirect(returnUrl);
    }
  }
}
