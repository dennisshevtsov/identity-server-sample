// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using System.Security.Claims;

  using IdentityServer4.Models;
  using IdentityServer4.Test;

  using IdentityServerSample.IdentityApp.Defaults;

  public static class IdentityServerExtensions
  {
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
              })
              .AddInMemoryApiScopes(IdentityServerExtensions.GetApiScopes(configuration))
              .AddInMemoryClients(IdentityServerExtensions.GetClients(configuration))
              .AddInMemoryApiResources(IdentityServerExtensions.GetApiResources(configuration))
              .AddTestUsers(IdentityServerExtensions.GetTestUsers(configuration))
              .AddDeveloperSigningCredential();

      return services;
    }

    private static IEnumerable<ApiScope> GetApiScopes(IConfiguration configuration)
      => new[]
      {
        new ApiScope(configuration["ApiScope_Name"], configuration["ApiScope_DisplayName"]),
      };

    private static IEnumerable<Client> GetClients(IConfiguration configuration)
      => new[]
      {
        new Client
        {
          ClientId = configuration["Client_Id_0"],
          ClientName = configuration["Client_Name_0"],
          ClientSecrets =
          {
            new Secret(configuration["Client_Secret_0"].Sha256()),
          },
          AllowedGrantTypes = GrantTypes.ClientCredentials,
          AllowedScopes =
          {
            configuration["ApiScope_Name"],
          },
        },
        new Client
        {
          ClientId = configuration["Client_Id_1"],
          ClientName = configuration["Client_Name_1"],
          RequireClientSecret = false,
          AllowedGrantTypes = GrantTypes.Code,
          AllowedScopes =
          {
            configuration["ApiScope_Name"],
          },
          AllowedCorsOrigins =
          {
            "http://localhost:4201",
            "http://localhost:4202",
          },
          RedirectUris =
          {
            "http://localhost:4201",
            "http://localhost:4202",
            "http://localhost:4202/sign-in-callback",
          },
        },
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

    private static List<TestUser> GetTestUsers(IConfiguration configuration)
      => new List<TestUser>
      {
        new TestUser
        {
          SubjectId = "test",
          Username = "test@test.test",
          Password = "test",
          IsActive = true,
          Claims = {
            new Claim("scope", configuration["ApiScope_Name"]!),
          },
        },
      };
  }
}
