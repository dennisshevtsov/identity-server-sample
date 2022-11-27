// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.ViewModels
{
  /// <summary>Represents details of a sing-out request.</summary>
  public sealed class SignOutViewModel
  {
    /// <summary>Gets/sets an object that represents a return URL.</summary>
    public string? ReturnUrl { get; set; }
  }
}
