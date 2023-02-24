// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Identities
{
  /// <summary>Extends identities with additional methods.</summary>
  public static class IdentityExtensions
  {
    /// <summary>Converts an instance of <see cref="Guid"/> to an instance of the <see cref="IdentityServerSample.ApplicationCore.Identities.IUserIdentity"/>.</summary>
    /// <param name="userId">An object that represents a user ID.</param>
    /// <returns>An object that represents an identity of a user.</returns>
    public static IUserIdentity ToUserIdentity(this Guid userId)
      => new UserIdentity(userId);

    /// <summary>Converts an instance of <see cref="string"/> to an instance of the <see cref="IdentityServerSample.ApplicationCore.Identities.IUserIdentity"/>.</summary>
    /// <param name="userId">An object that represents a user ID.</param>
    /// <returns>An object that represents an identity of a user.</returns>
    public static IUserIdentity? ToUserIdentity(this string userId)
    {
      if (Guid.TryParse(userId, out var userIdGuid))
      {
        return userIdGuid.ToUserIdentity();
      }

      return null;
    }

    /// <summary>Converts an instance of <see cref="string"/> to an instance of the <see cref="IdentityServerSample.ApplicationCore.Identities.IAudienceIdentity"/>.</summary>
    /// <param name="audienceName">An object that represents a name of a scope.<param>
    /// <returns>An object that represents an identity of a user.</returns>
    public static IAudienceIdentity ToAudienceIdentity(this string audienceName)
      => new AudienceIdentity(audienceName);

    /// <summary>Converts a collection of <see cref="string"/> to a collection of the <see cref="IdentityServerSample.ApplicationCore.Identities.IAudienceIdentity"/>.</summary>
    /// <param name="audienceNameCollection">An object that represents a collection scope names.<param>
    /// <returns>An object that represents an identity of a user.</returns>
    public static IEnumerable<IAudienceIdentity> ToAudienceIdentities(
      this IEnumerable<string> audienceNameCollection)
      => audienceNameCollection.Select(audienceName => audienceName.ToAudienceIdentity());

    /// <summary>Converts an instance of <see cref="string"/> to an instance of the <see cref="IdentityServerSample.ApplicationCore.Identities.IScopeIdentity"/>.</summary>
    /// <param name="scopeName">An object that represents a name of a scope.<param>
    /// <returns>An object that represents an identity of a user.</returns>
    public static IScopeIdentity ToScopeIdentity(this string scopeName)
      => new ScopeIdentity(scopeName);

    /// <summary>Converts a collection of <see cref="string"/> to a collection of the <see cref="IdentityServerSample.ApplicationCore.Identities.IScopeIdentity"/>.</summary>
    /// <param name="scopeNameCollection">An object that represents a collection scope names.<param>
    /// <returns>An object that represents an identity of a user.</returns>
    public static IEnumerable<IScopeIdentity> ToScopeIdentities(
      this IEnumerable<string> scopeNameCollection)
      => scopeNameCollection.Select(scopeName => scopeName.ToScopeIdentity());

    private struct UserIdentity : IUserIdentity
    {
      public UserIdentity(Guid userId) => UserId = userId;

      public Guid UserId { get; set; }
    }

    private struct AudienceIdentity : IAudienceIdentity
    {
      public AudienceIdentity(string audienceName) => AudienceName = audienceName;

      public string? AudienceName { get; set; }
    }

    private struct ScopeIdentity : IScopeIdentity
    {
      public ScopeIdentity(string scopeName) => ScopeName = scopeName;

      public string? ScopeName { get; set; }
    }
  }
}
