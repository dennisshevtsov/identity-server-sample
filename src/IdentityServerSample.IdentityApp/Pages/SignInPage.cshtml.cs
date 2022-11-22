// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Pages
{
  using IdentityServer4;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.RazorPages;

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None,NoStore = true)]
  public class SignInPageModel : PageModel
  {
    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }

    [BindProperty(SupportsGet = false)]
    public string? Email { get; set; }

    [BindProperty(SupportsGet = false)]
    public string? Password { get; set; }

    [BindProperty(SupportsGet = false)]
    public bool RememberMe { get; set; }

    public async Task<IActionResult> OnGet()
    {
      await HttpContext.SignInAsync(new IdentityServerUser("test")
      {
        DisplayName = "test",
      });

      return Redirect(ReturnUrl!);
    }
  }
}
