// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories.Test
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Repositories;
  using IdentityServerSample.Infrastructure.Test;

  [TestClass]
  public sealed class UserRepositoryTest : DbIntegrationTestBase
  {
#pragma warning disable CS8618
    private IUserRepository _userRepository;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _userRepository = ServiceProvider.GetRequiredService<IUserRepository>();
    }

    private async Task<UserEntity> CreateNewUserAsync()
    {
      var userEntity = new UserEntity
      {
        Name = Guid.NewGuid().ToString(),
        Email = Guid.NewGuid().ToString(),
        PasswordHash = Guid.NewGuid().ToString(),
      };

      var scopeEntityEntry = DbContext.Add(userEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      scopeEntityEntry.State = EntityState.Detached;

      return userEntity;
    }

    private async Task<UserEntity[]> CreateNewUsersAsync(int audiences)
    {
      var userEntityCollection = new List<UserEntity>();

      for (int i = 0; i < audiences; i++)
      {
        userEntityCollection.Add(await CreateNewUserAsync());
      }

      return userEntityCollection.OrderBy(entity => entity.Name)
                                 .ToArray();
    }

    private void AreEqual(UserEntity control, UserEntity test)
    {
      Assert.AreEqual(control.Name, test.Name);
      Assert.AreEqual(control.Email, test.Email);
      Assert.AreEqual(control.PasswordHash, test.PasswordHash);
    }

    private void IsDetached(UserEntity userEntity)
      => Assert.AreEqual(EntityState.Detached, DbContext.Entry(userEntity).State);

    private void AreDetached(List<UserEntity> userEntityCollection)
    {
      foreach (var userEntity in userEntityCollection)
      {
        IsDetached(userEntity);
      }
    }
  }
}
