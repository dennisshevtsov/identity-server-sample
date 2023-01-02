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

    public static IServiceCollection AddConfiguredAntiforgery(this IServiceCollection services)
    {
      services.AddAntiforgery(options => options.HeaderName = AntiforgeryExtensions.XsrfTokenHeaderName);

      return services;
    }

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
