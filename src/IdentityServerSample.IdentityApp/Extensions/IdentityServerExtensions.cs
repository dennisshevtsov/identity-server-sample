// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using System.Security.Claims;

  using IdentityServer4.Models;
  using IdentityServer4.Services;
  using IdentityServer4.Test;

  using IdentityServerSample.IdentityApp.Defaults;
  using IdentityServerSample.IdentityApp.Stores;

  public static class IdentityServerExtensions
  {
    public static IServiceCollection AddConfiguredIdentityServer(
      this IServiceCollection services,
      IConfiguration configuration)
    {
      services.AddSingleton<ICorsPolicyService>(
        provider => new DefaultCorsPolicyService(provider.GetRequiredService<ILogger<DefaultCorsPolicyService>>())
        {
          AllowedOrigins= new[]
          {
            "http://localhost:4200",
          },
        });

      services.AddIdentityServer(options =>
              {
                options.UserInteraction.ErrorUrl = $"/{Routing.ErrorRoute}";
                options.UserInteraction.ErrorIdParameter = Routing.ErrorIdRouteParameter;

                options.UserInteraction.LoginUrl = $"/{Routing.AccountRoute}/{Routing.SignInRoute}";
                options.UserInteraction.LoginReturnUrlParameter = Routing.ReturnUrlRouteParameter;

                options.UserInteraction.LogoutUrl = $"/{Routing.AccountRoute}/{Routing.SignOutRoute}";
                options.UserInteraction.LogoutIdParameter = Routing.SignOutIdRouteParameter;
              })
              .AddClientStore<ClientStore>()
              .AddInMemoryApiScopes(IdentityServerExtensions.GetApiScopes(configuration))
              .AddInMemoryApiResources(IdentityServerExtensions.GetApiResources(configuration))
              .AddInMemoryIdentityResources(IdentityServerExtensions.GetIdentityResources())
              .AddTestUsers(IdentityServerExtensions.GetTestUsers(configuration))
              .AddDeveloperSigningCredential();

      return services;
    }

    private static IEnumerable<ApiScope> GetApiScopes(IConfiguration configuration)
      => new[]
      {
        new ApiScope(configuration["ApiScope_Name"], configuration["ApiScope_DisplayName"]),
      };

    private static IEnumerable<ApiResource> GetApiResources(IConfiguration configuration)
      => new[]
      {
        new ApiResource
        {
          Name = configuration["ApiResource_Name"],
          DisplayName = configuration["ApiResource_DisplayName"],
          Scopes =
          {
            configuration["ApiScope_Name"],
          },
        },
      };

    private static IEnumerable<IdentityResource> GetIdentityResources()
      => new IdentityResource[]
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
      };

    private static List<TestUser> GetTestUsers(IConfiguration configuration)
      => new List<TestUser>
      {
        new TestUser
        {
          SubjectId = "test",
          Username = "test@test.test",
          Password = "test",
          IsActive = true,
          Claims =
          {
            new Claim("scope", configuration["ApiScope_Name"]!),
          },
        },
      };
  }
}
