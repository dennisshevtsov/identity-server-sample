// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.Identity
{
  using System.Security.Claims;

  using IdentityModel;
  using Microsoft.AspNetCore.Identity;

  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Provides an abstraction for a factory to create a <see cref="System.Security.Claims.ClaimsPrincipal"/> from a user.</summary>
  public sealed class UserClaimsPrincipalFactory : IUserClaimsPrincipalFactory<UserEntity>
  {
    /// <summary>Creates an instance of the <see cref="System.Security.Claims.ClaimsPrincipal"/> class using an instance of the <see cref="IdentityServerSample.ApplicationCore.Entities.UserEntity"/> class.</summary>
    /// <param name="user">An object that represents details of a user.</param>
    /// <returns></returns>
    public Task<ClaimsPrincipal> CreateAsync(UserEntity user)
    {
      var claims = new[]
      {
        new Claim(JwtClaimTypes.Subject, user.UserId.ToString()),
        new Claim(JwtClaimTypes.PreferredUserName, user.Name!),
        new Claim(JwtClaimTypes.Name, user.Name!),
        new Claim(JwtClaimTypes.Email, user.Email!),
        new Claim(JwtClaimTypes.EmailVerified, "true"),
        new Claim("scope", "identity-server-sample-api-scope"),
      };

      var identity = new ClaimsIdentity(claims, "Bearer");
      var principal = new ClaimsPrincipal(identity);

      return Task.FromResult(principal);
    }
  }
}
