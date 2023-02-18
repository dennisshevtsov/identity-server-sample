// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Defaults
{
  /// <summary>Provides values of routes.</summary>
  public static class Routing
  {
    /// <summary>A value that represents the base of the account route.</summary>
    public const string AccountRoute = "api/account";

    /// <summary>A value that represents the route to sign in an account.</summary>
    public const string SignInRoute = "signin";

    /// <summary>A value that represents the route to sign out an account.</summary>
    public const string SignOutRoute = "signout";

    /// <summary>A value that represents the base of the error route.</summary>
    public const string ErrorRoute = "api/error";

    /// <summary>A value that represents the roate to get an error.</summary>
    public const string GetErrorRoute = "{errorId}";
  }
}
