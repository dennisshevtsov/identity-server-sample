// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using IdentityServer4;
  using IdentityServer4.Services;
  using IdentityServer4.Test;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.IdentityApp.Dtos;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route("api/account")]
  [Produces("application/json")]
  public sealed class AccountController : ControllerBase
  {
    private readonly TestUserStore _userStore;
    private readonly IIdentityServerInteractionService _identityServerInteractionService;

    /// <summary>Inititalizes a new instance of the <see cref="IdentityServerSample.IdentityApp.Controllers.AccountController"/> class.</summary>
    /// <param name="userStore">An object that represents a store for test users.</param>
    /// <param name="identityServerInteractionService">An object that provides a simple API to communicate with IdentityServer.</param>
    public AccountController(
      TestUserStore userStore,
      IIdentityServerInteractionService identityServerInteractionService)
    {
      _userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
      _identityServerInteractionService = identityServerInteractionService ??
        throw new ArgumentNullException(nameof(identityServerInteractionService));
    }

    [HttpPost("signin", Name = nameof(AccountController.SingInAccount))]
    public async Task<IActionResult> SingInAccount([FromBody] SingInAccountRequestDto requestDto)
    {
      if (ModelState.IsValid)
      {
        if (_userStore.ValidateCredentials(requestDto.Email, requestDto.Password))
        {
          var testUser = _userStore.FindByUsername(requestDto.Email);
          var identityServerUser = new IdentityServerUser(testUser.SubjectId)
          {
            DisplayName = testUser.Username,
          };

          await HttpContext.SignInAsync(identityServerUser);

          return NoContent();
        }

        ModelState.AddModelError(
          nameof(SingInAccountRequestDto.Email), "The credentials is not valid.");
      }

      return BadRequest();
    }

    [HttpGet("singout", Name = nameof(AccountController.SingOutAccount))]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> SingOutAccount(SignOutAccountRequestDto requestDto)
    {
      var logoutRequest =
        await _identityServerInteractionService.GetLogoutContextAsync(requestDto.SignOutId)!;

      await HttpContext.SignOutAsync();

      return Redirect(logoutRequest.PostLogoutRedirectUri);
    }
  }
}
