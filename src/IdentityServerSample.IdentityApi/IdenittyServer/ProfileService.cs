// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.IdenittyServer
{
  using System;

  using IdentityServer4.Extensions;
  using IdentityServer4.Models;
  using IdentityServer4.Services;
  using Microsoft.AspNetCore.Identity;

  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>This interface allows IdentityServer to connect to your user and profile store.</summary>
  public sealed class ProfileService : IProfileService
  {
    private readonly UserManager<UserEntity> _userManager;
    private readonly IUserClaimsPrincipalFactory<UserEntity> _userClaimsPrincipalFactory;

    /// <summary>Initializes a new instance of the <see cref="ProfileService"/> class.</summary>
    /// <param name="userManager">An object provides the APIs for managing user in a persistence store.</param>
    /// <param name="userClaimsPrincipalFactory">An object that provides an abstraction for a factory to create a <see cref="System.Security.Claims.ClaimsPrincipal"/> from a user.</param>
    public ProfileService(
      UserManager<UserEntity> userManager,
      IUserClaimsPrincipalFactory<UserEntity> userClaimsPrincipalFactory)
    {
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
      _userClaimsPrincipalFactory = userClaimsPrincipalFactory ?? throw new ArgumentNullException(nameof(userClaimsPrincipalFactory));
    }

    /// <summary>This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)</summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
      var userId = context.Subject.GetSubjectId();
      var userEntity = await _userManager.FindByIdAsync(userId);

      if (userEntity != null)
      {
        var principal = await _userClaimsPrincipalFactory.CreateAsync(userEntity);

        context.AddRequestedClaims(principal.Claims);
      }
    }

    /// <summary>This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in). (e.g. during token issuance or validation).</summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public Task IsActiveAsync(IsActiveContext context) => Task.CompletedTask;
  }
}
