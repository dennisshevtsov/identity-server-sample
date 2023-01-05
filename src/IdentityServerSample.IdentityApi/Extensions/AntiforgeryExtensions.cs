// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.AspNetCore.Antiforgery;

  /// <summary>Provides a simple API to configure a pipeline.</summary>
  public static class AntiforgeryExtensions
  {
    private const string XsrfTokenName = "XSRF-TOKEN";
    private const string XsrfTokenHeaderName = "X-" + AntiforgeryExtensions.XsrfTokenName;

    /// <summary>Adds antiforgery services to a pipeline.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection AddConfiguredAntiforgery(this IServiceCollection services)
    {
      services.AddAntiforgery(options => options.HeaderName = AntiforgeryExtensions.XsrfTokenHeaderName);

      return services;
    }

    /// <summary>Adds antiforgery middleware to a pipeline.</summary>
    /// <param name="app">An object that defines a class that provides the mechanisms to configure an application's request pipeline.</param>
    /// <returns>An object that defines a class that provides the mechanisms to configure an application's request pipeline.</returns>
    public static IApplicationBuilder UseAntiforgery(this IApplicationBuilder app)
    {
      app.Use((context, next) =>
      {
        var tokens = context.RequestServices.GetRequiredService<IAntiforgery>()
                                            .GetAndStoreTokens(context);

        context.Response.Cookies.Append(
          AntiforgeryExtensions.XsrfTokenName,
          tokens.RequestToken!,
          new CookieOptions
          {
            HttpOnly = false,
            SameSite = SameSiteMode.Strict,
          });

        return next.Invoke();
      });

      app.Use(async (context, next) =>
      {
        if (context.Request.Path.StartsWithSegments("/api/account/signin"))
        {
          try
          {
            await context.RequestServices.GetRequiredService<IAntiforgery>()
                                         .ValidateRequestAsync(context);
          }
          catch (AntiforgeryValidationException)
          {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            return;
          }
        }

        await next.Invoke();
      });

      return app;
    }
  }
}
