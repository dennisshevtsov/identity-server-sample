// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories.Test
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;

  using IdentityServerSample.Infrastructure.Test;
  using IdentityServerSample.ApplicationCore.Identities;

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
    public async Task GetUserScopesAsync_Should_Return_User_With_Defined_User_Id()
    {
      var userId = Guid.NewGuid();
      var controlUserScopeEntityCollection = await CreateNewUserScopesAsync(userId, 10);

      var identity = userId.ToUserIdentity();

      var testUserScopeEntityCollection =
        await _userScopeRepository.GetUserScopesAsync(identity, CancellationToken);

      Assert.IsNotNull(testUserScopeEntityCollection);

      AreEqual(controlUserScopeEntityCollection, testUserScopeEntityCollection);
      AreDetached(testUserScopeEntityCollection);
    }

    private async Task<UserScopeEntity> CreateNewUserScopeAsync(Guid userId)
    {
      var userScopeEntity = new UserScopeEntity
      {
        UserId = userId,
        Name = Guid.NewGuid().ToString(),
      };

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

      return userScopeEntityCollection.OrderBy(entity => entity.Name)
                                      .ToList();
    }

    private void AreEqual(UserScopeEntity control, UserScopeEntity test)
    {
      Assert.AreEqual(control.Name, test.Name);
      Assert.AreEqual(control.UserId, test.UserId);
    }

    private void AreEqual(List<UserScopeEntity> control, List<UserScopeEntity> test)
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
