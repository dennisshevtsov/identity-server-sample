// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  /// <summary>Provides methods to extend the API of the <see cref="Microsoft.AspNetCore.Builder.IApplicationBuilder"/>.</summary>
  public static class DatabaseExtensions
  {
    /// <summary>Sets up the database with all required data.</summary>
    /// <param name="app">An object that defines a class that provides the mechanisms to configure an application's request pipeline.</param>
    /// <returns>An object that defines a class that provides the mechanisms to configure an application's request pipeline.</returns>
    public static IApplicationBuilder SetUpDatabase(this IApplicationBuilder app)
    {


      return app;
    }
  }
}
