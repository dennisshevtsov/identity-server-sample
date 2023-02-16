// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Extensions
{
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Extends identities with additional methods.</summary>
  public static class IdentityExtensions
  {
    public static IUserIdentity? ToUserIdentity(this Guid userId)
      => new UserIdentity(userId);

    public static IUserIdentity? ToUserIdentity(this string userId)
    {
      if (Guid.TryParse(userId, out var userIdGuid))
      {
        return userIdGuid.ToUserIdentity();
      }

      return null;
    }

    private sealed class UserIdentity : IUserIdentity
    {
      public UserIdentity(Guid userId) => UserId = userId;

      public Guid UserId { get; set; }
    }
  }
}
