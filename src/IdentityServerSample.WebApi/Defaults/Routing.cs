// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Defaults
{
  /// <summary>Provides values of routes.</summary>
  public static class Routing
  {
    /// <summary>A value that represents the base of the account route.</summary>
    public const string AccountRoute = "account";

    /// <summary>A value that represents a route to get sign-in page.</summary>
    public const string SignInRoute = "sing-in";

    /// <summary>A value that represents a route to get sign-out page.</summary>
    public const string SignOutRoute = "sing-out";

    /// <summary>A value that represents the base of the error route.</summary>
    public const string ErrorRoute = "error";

    /// <summary>A value that represents the name of the errorId route parameter.</summary>
    public const string ErrorIdRouteParameter = "errorId";

    /// <summary>A value that represents the name of the errorId route parameter.</summary>
    public const string SignOutIdRouteParameter = "signOutId";

    /// <summary>A value that represents the name of the returnUrl route parameter.</summary>
    public const string ReturnUrlRouteParameter = "returnUrl";
  }
}
