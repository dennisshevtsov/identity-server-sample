// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using global::IdentityModel;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.IdentityApi.AspNetIdentity;

  /// <summary>Provides a simple API to configure a pipeline.</summary>
  public static class IdentityExtensions
  {
    /// <summary>Sets up the Identity.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpAspNetIdentity(this IServiceCollection services)
    {
      services.AddIdentity<UserEntity, RoleEntity>(options =>
              {
                options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
                options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
                options.ClaimsIdentity.EmailClaimType = JwtClaimTypes.Email;
                options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
              })
              .AddUserStore<UserStore>()
              .AddRoleStore<RoleStore>();

      return services;
    }
  }
}
