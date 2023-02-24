// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using System;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to query and save scopes.</summary>
  public sealed class ScopeService : IScopeService
  {
    private readonly IScopeRepository _scopeRepository;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.ApplicationCore.Services.ScopeService"/> class.</summary>
    /// <param name="scopeRepository">An object that provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.ScopeEntity"/> class.</param>
    public ScopeService(IScopeRepository scopeRepository)
    {
      _scopeRepository = scopeRepository ?? throw new ArgumentNullException(nameof(scopeRepository));
    }

    /// <summary>Gets a collection of available scopes.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<List<ScopeEntity>> GetScopesAsync(CancellationToken cancellationToken)
    {
      var scopeEntityCollection = await _scopeRepository.GetScopesAsync(cancellationToken);

      scopeEntityCollection.Add(new ScopeEntity
      {
        ScopeName = "openid",
        DisplayName = "OpenID Scope",
        Description = "The required OpenID scope.",
        Standard = true,
      });
      scopeEntityCollection.Add(new ScopeEntity
      {
        ScopeName = "profile",
        DisplayName = "Profile scope.",
        Description = "This scope value requests access to the End-User's default profile Claims, which are: name, family_name, given_name, middle_name, nickname, preferred_username, profile, picture, website, gender, birthdate, zoneinfo, locale, and updated_at.",
        Standard = true,
      });
      scopeEntityCollection.Add(new ScopeEntity
      {
        ScopeName = "email",
        DisplayName = "Email Scope",
        Description = "This scope value requests access to the email and email_verified Claims.",
        Standard = true,
      });
      scopeEntityCollection.Add(new ScopeEntity
      {
        ScopeName = "address",
        DisplayName = "Address Scope",
        Description = "This scope value requests access to the address Claim.",
        Standard = true,
      });
      scopeEntityCollection.Add(new ScopeEntity
      {
        ScopeName = "phone",
        DisplayName = "Phone Scope",
        Description = "This scope value requests access to the phone_number and phone_number_verified Claims. ",
        Standard = true,
      });

      return scopeEntityCollection;
    }
  }
}
