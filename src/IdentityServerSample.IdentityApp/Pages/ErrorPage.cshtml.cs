// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Pages
{
  using IdentityServer4.Services;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.RazorPages;

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public class ErrorPageModel : PageModel
  {
    private readonly IIdentityServerInteractionService _identityServerInteractionService;

    public ErrorPageModel(
      IIdentityServerInteractionService identityServerInteractionService)
    {
      _identityServerInteractionService = identityServerInteractionService ??
        throw new ArgumentNullException(nameof(identityServerInteractionService));
    }

    [BindProperty(SupportsGet = true)]
    public string? ErrorId { get; set; }

    public string? Message { get; private set; }

    public async Task OnGet()
    {
      var errorMessage =
        await _identityServerInteractionService.GetErrorContextAsync(ErrorId!);

      if (errorMessage != null)
      {
        Message = errorMessage.ErrorDescription;
      }
    }
  }
}
