// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.ViewModels
{
  public sealed class SignInViewModel
  {
    public string? ReturnUrl { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool RememberMe { get; set; }
  }
}
