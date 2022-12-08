// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Defaults
{
  /// <summary>Provides values of routes.</summary>
  public static class Routing
  {
    /// <summary>A value that represents a base for the user endpoints.</summary>
    public const string UserRoute = "api/user";

    /// <summary>A value that represents a route to get an authenticated user.</summary>
    public const string GetAuthenticatedUserRoute = "authenticated";


    /// <summary>A value that represents a base for the task endpoints.</summary>
    public const string TaskRoute = "api/task";
  }
}
