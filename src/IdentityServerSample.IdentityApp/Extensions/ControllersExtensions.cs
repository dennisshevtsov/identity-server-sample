// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.AspNetCore.Mvc.Razor;

  public static class ControllersExtensions
  {
    public static IServiceCollection AddConfiguredControllers(
      this IServiceCollection services)
    {
      services.AddControllersWithViews()
              .AddViewOptions(options =>
              {
                options.HtmlHelperOptions.ClientValidationEnabled = false;
              });
      services.Configure<RazorViewEngineOptions>(options =>
              {
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/Views/{0}" + RazorViewEngine.ViewExtension);
              });

      return services;
    }
  }
}
