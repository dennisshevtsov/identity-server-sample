// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Pages
{
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Mvc.RazorPages;

  public class SignOutPageModel : PageModel
  {
    public async Task OnGet()
    {
      await HttpContext.SignOutAsync();
    }
  }
}
