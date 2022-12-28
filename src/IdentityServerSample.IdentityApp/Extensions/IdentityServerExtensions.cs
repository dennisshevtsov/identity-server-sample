// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using System.Security.Claims;

  using IdentityServer4.Test;

  using IdentityServerSample.IdentityApp.Defaults;
  using IdentityServerSample.IdentityApp.Services;
  using IdentityServerSample.IdentityApp.Stores;

  /// <summary>Provides a simple API to configure a pipeline.</summary>
  public static class IdentityServerExtensions
  {
    /// <summary>Adds the Identity Server middleware to a pipeline.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">An object that represents a set of key/value application configuration properties.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection AddConfiguredIdentityServer(
      this IServiceCollection services,
      IConfiguration configuration)
    {
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
              .AddResourceStore<ResourceStore>()
              .AddCorsPolicyService<CorsPolicyService>()
              .AddTestUsers(IdentityServerExtensions.GetTestUsers(configuration))
              .AddDeveloperSigningCredential();

      return services;
    }

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
