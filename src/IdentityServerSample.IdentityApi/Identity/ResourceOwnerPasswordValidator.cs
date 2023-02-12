
// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.Identity
{
  using IdentityModel;
  using IdentityServer4.Models;
  using IdentityServer4.Validation;
  using Microsoft.AspNetCore.Identity;

  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Handles validation of resource owner password credentials.</summary>
  public sealed class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
  {
    private readonly SignInManager<UserEntity> _signInManager;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.IdentityApi.Identity.ResourceOwnerPasswordValidator"/> class.</summary>
    /// <param name="signInManager">An object that provides the APIs for user sign in.</param>
    public ResourceOwnerPasswordValidator(SignInManager<UserEntity> signInManager)
    {
      _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    /// <summary>Validates the resource owner password credential</summary>
    /// <param name="context">An object that describes the resource owner password validation context.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
      UserEntity? userEntity;
      SignInResult signInResult;

      if ((userEntity = await _signInManager.UserManager.FindByNameAsync(context.UserName)) != null &&
          (signInResult = await _signInManager.CheckPasswordSignInAsync(userEntity, context.Password, false)) != null &&
          signInResult.Succeeded)
      {
        context.Result = new GrantValidationResult(
          userEntity.AccountId.ToString(),
          OidcConstants.AuthenticationMethods.Password);
      }
      else
      {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
      }
    }
  }
}
