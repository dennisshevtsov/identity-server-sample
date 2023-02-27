// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories.Test
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;

  using IdentityServerSample.Infrastructure.Test;
  using IdentityServerSample.ApplicationCore.Identities;
  using IdentityServerSample.ApplicationCore.Repositories;
  using Microsoft.Azure.Cosmos;

  [TestClass]
  public sealed class UserScopeRepositoryTest : DbIntegrationTestBase
  {
#pragma warning disable CS8618
    private IUserScopeRepository _userScopeRepository;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _userScopeRepository = ServiceProvider.GetRequiredService<IUserScopeRepository>();
    }

    [TestMethod]
    public async Task AddUserScopesAsync_Should_Update_Scopes()
    {
      var userId = Guid.NewGuid();
      var controlUserScopeEntityCollection =
        UserScopeRepositoryTest.GenerateNewUserScopes(userId, 10);

      var controlUserEntity = new UserEntity
      {
        UserId = userId,
        Scopes = controlUserScopeEntityCollection,
      };

      await _userScopeRepository.AddUserScopesAsync(controlUserEntity, CancellationToken);

      AreDetached(controlUserScopeEntityCollection);

      var actualUserEntityCollection =
        await DbContext.Set<UserScopeEntity>()
                       .WithPartitionKey(userId.ToString())
                       .OrderBy(entity => entity.ScopeName)
                       .ToListAsync(CancellationToken);

      UserScopeRepositoryTest.AreEqual(controlUserScopeEntityCollection, actualUserEntityCollection);
    }

    [TestMethod]
    public async Task GetUserScopesAsync_Should_Return_User_With_Defined_User_Id()
    {
      var userId = Guid.NewGuid();
      var controlUserScopeEntityCollection = await CreateNewUserScopesAsync(userId, 10);

      var identity = userId.ToUserIdentity();

      var testUserScopeEntityCollection =
        await _userScopeRepository.GetUserScopesAsync(identity, CancellationToken);

      Assert.IsNotNull(testUserScopeEntityCollection);

      UserScopeRepositoryTest.AreEqual(controlUserScopeEntityCollection, testUserScopeEntityCollection);
      AreDetached(testUserScopeEntityCollection);
    }

    private static UserScopeEntity GenerateNewUserScope(Guid userId) => new UserScopeEntity
    {
      UserId = userId,
      ScopeName = Guid.NewGuid().ToString(),
    };

    private static List<UserScopeEntity> GenerateNewUserScopes(Guid userId, int scopes)
    {
      var userScopeEntityCollection = new List<UserScopeEntity>();

      for (int i = 0; i < scopes; i++)
      {
        userScopeEntityCollection.Add(UserScopeRepositoryTest.GenerateNewUserScope(userId));
      }

      userScopeEntityCollection =
        userScopeEntityCollection.OrderBy(entity => entity.ScopeName)
                                 .ToList();

      return userScopeEntityCollection;
    }

    private async Task<UserScopeEntity> CreateNewUserScopeAsync(Guid userId)
    {
      var userScopeEntity = UserScopeRepositoryTest.GenerateNewUserScope(userId);

      var useScoperEntityEntry = DbContext.Add(userScopeEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      useScoperEntityEntry.State = EntityState.Detached;

      return userScopeEntity;
    }

    private async Task<List<UserScopeEntity>> CreateNewUserScopesAsync(Guid userId, int userScopes)
    {
      var userScopeEntityCollection = new List<UserScopeEntity>();

      for (int i = 0; i < userScopes; i++)
      {
        userScopeEntityCollection.Add(await CreateNewUserScopeAsync(userId));
      }

      return userScopeEntityCollection.OrderBy(entity => entity.ScopeName)
                                      .ToList();
    }

    private static void AreEqual(UserScopeEntity control, UserScopeEntity test)
    {
      Assert.AreEqual(control.ScopeName, test.ScopeName);
      Assert.AreEqual(control.UserId, test.UserId);
    }

    private static void AreEqual(List<UserScopeEntity> control, List<UserScopeEntity> test)
    {
      Assert.AreEqual(control.Count, test.Count);

      for (int i = 0; i < control.Count; i++)
      {
        AreEqual(control[i], test[i]);
      }
    }

    private void IsDetached(UserScopeEntity userScopeEntity)
      => Assert.AreEqual(EntityState.Detached, DbContext.Entry(userScopeEntity).State);

    private void AreDetached(List<UserScopeEntity> userScopeEntityCollection)
    {
      foreach (var userScopeEntity in userScopeEntityCollection)
      {
        IsDetached(userScopeEntity);
      }
    }
  }
}
