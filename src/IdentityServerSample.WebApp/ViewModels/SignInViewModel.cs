// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApp.ViewModels
{
  using System.ComponentModel.DataAnnotations;

  /// <summary>Represents details of sign-in credentials.</summary>
  public sealed class SignInViewModel
  {
    /// <summary>Gets/sets an object that represents a return URL.</summary>
    public string? ReturnUrl { get; set; }

    /// <summary>Gets/sets an object that represents an email of an account.</summary>
    [Required]
    public string? Email { get; set; }

    /// <summary>Gets/sets an object that represents a password of an account.</summary>
    [Required]
    public string? Password { get; set; }

    /// <summary>Gets/sets an object that indicates if a sing-in request is persintent.</summary>
    public bool RememberMe { get; set; }
  }
}
