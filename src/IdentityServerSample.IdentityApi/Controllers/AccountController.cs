// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using IdentityServer4.Services;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.IdentityApp.Dtos;
  using IdentityServerSample.IdentityApp.Defaults;
  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route(Routing.AccountRoute)]
  [Produces(ContentType.Json)]
  public sealed class AccountController : ControllerBase
  {
    public const string InvalidCredentialsErrorMessage = "Invalid credentials.";

    private readonly SignInManager<UserEntity> _signInManager;
    private readonly IIdentityServerInteractionService _identityServerInteractionService;

    /// <summary>Inititalizes a new instance of the <see cref="IdentityServerSample.IdentityApp.Controllers.AccountController"/> class.</summary>
    /// <param name="signInManager">An object that provides the APIs for user sign in.</param>
    /// <param name="identityServerInteractionService">An object that provides a simple API to communicate with IdentityServer.</param>
    public AccountController(
      SignInManager<UserEntity> signInManager,
      IIdentityServerInteractionService identityServerInteractionService)
    {
      _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
      _identityServerInteractionService = identityServerInteractionService ??
        throw new ArgumentNullException(nameof(identityServerInteractionService));
    }

    /// <summary>Handles a request to sign in an account.</summary>
    /// <param name="requestDto">An object that represents data to sign in an account.</param>
    /// <returns>An object that represents an asynchronous operation that produces a result at some time in the future.</returns>
    [HttpPost(Routing.SignInRoute, Name = nameof(AccountController.SingInAccount))]
    public async Task<IActionResult> SingInAccount([FromBody] SingInAccountRequestDto requestDto)
    {
      if (ModelState.IsValid)
      {
        var signInResult =
          await _signInManager.PasswordSignInAsync(
            requestDto.Email!, requestDto.Password!, false, false);

        if (signInResult != null && signInResult.Succeeded)
        {
          return NoContent();
        }

        ModelState.AddModelError(
          nameof(SingInAccountRequestDto.Email),
          AccountController.InvalidCredentialsErrorMessage);
      }

      return BadRequest();
    }

    /// <summary>Handles a request to sign out an account.</summary>
    /// <param name="requestDto">An object that represents data to sign out an account.</param>
    /// <returns>An object that represents an asynchronous operation that produces a result at some time in the future.</returns>
    [HttpGet(Routing.SignOutRoute, Name = nameof(AccountController.SingOutAccount))]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> SingOutAccount([FromQuery] SignOutAccountRequestDto requestDto)
    {
      var logoutRequest =
        await _identityServerInteractionService.GetLogoutContextAsync(requestDto.SignOutId)!;

      await _signInManager.SignOutAsync();

      return Redirect(logoutRequest.PostLogoutRedirectUri);
    }
  }
}
