// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.Identity
{
  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to configure a pipeline.</summary>
  public static class IdentityExtensions
  {
    /// <summary>Sets up the Identity.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">An object that represents a set of key/value application configuration properties.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpIdentity(this IServiceCollection services)
    {
      services.AddIdentity<UserEntity, RoleEntity>()
              .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
              .AddUserStore<UserStore>()
              .AddRoleStore<RoleStore>();

      return services;
    }
  }
}
