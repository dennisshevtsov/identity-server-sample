// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApp.Controllers
{
  using IdentityServer4;
  using IdentityServer4.Test;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.WebApp.Defaults;
  using IdentityServerSample.WebApp.ViewModels;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [AllowAnonymous]
  [Route(Routing.AccountRoute)]
  public sealed class SignInController : Controller
  {
    private readonly TestUserStore _userStore;

    /// <summary>Inititalizes a new instance of the <see cref="IdentityServerSample.WebApp.Controllers.SignInController"/> class.</summary>
    /// <param name="userStore">An object that represents a store for test users.</param>
    public SignInController(TestUserStore userStore)
    {
      _userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
    }

    /// <summary>Handles the get sign-in page request.</summary>
    /// <param name="vm">An object that represents details of sign-in credentials.</param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    [HttpGet(Routing.SignInRoute)]
    public IActionResult Get(SignInViewModel vm)
    {
      ModelState.Clear();

      return View("SignInView", vm);
    }

    /// <summary>Handles the post sign-in credentials request.</summary>
    /// <param name="vm">An object that represents details of sign-in credentials.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    [ValidateAntiForgeryToken]
    [HttpPost(Routing.SignInRoute)]
    public async Task<IActionResult> Post(SignInViewModel vm)
    {
      if (ModelState.IsValid)
      {
        if (_userStore.ValidateCredentials(vm.Email, vm.Password))
        {
          var testUser = _userStore.FindByUsername(vm.Email);
          var identityServerUser = new IdentityServerUser(testUser.SubjectId)
          {
            DisplayName = testUser.Username,
          };

          await HttpContext.SignInAsync(identityServerUser);

          return Redirect(vm.ReturnUrl!);
        }

        ModelState.AddModelError(
          nameof(SignInViewModel.Email), "The credentials is not valid.");
      }

      return View("SignInView", vm);
    }
  }
}
