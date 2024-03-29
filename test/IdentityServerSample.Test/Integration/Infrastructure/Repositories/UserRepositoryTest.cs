﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories.Test
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;

  using IdentityServerSample.Infrastructure.Test;
  using IdentityServerSample.ApplicationCore.Identities;
  using IdentityServerSample.ApplicationCore.Repositories;
  using IdentityServerSample.ApplicationCore.Entities;

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

    [TestMethod]
    public async Task AddUserAsync_Should_Create_New_User()
    {
      var controlUserEntity = UserRepositoryTest.GenerateNewUser();

      await _userRepository.AddUserAsync(controlUserEntity, CancellationToken);

      IsDetached(controlUserEntity);

      var actualUserEntity =
        await DbContext.Set<UserEntity>()
                       .WithPartitionKey(controlUserEntity.UserId.ToString())
                       .FirstOrDefaultAsync();

      Assert.IsNotNull(actualUserEntity);
      UserRepositoryTest.AreEqual(controlUserEntity, actualUserEntity);
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_User_With_Defined_User_Id()
    {
      var allUserEntityCollection = await CreateNewUsersAsync(10);

      var controlUserEntity = allUserEntityCollection[2];

      IUserIdentity identity = controlUserEntity;

      var testUserEntity = await _userRepository.GetUserAsync(identity, CancellationToken);

      Assert.IsNotNull(testUserEntity);

      UserRepositoryTest.AreEqual(controlUserEntity, testUserEntity);
      IsDetached(testUserEntity);
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_Null_For_Unknown_Id()
    {
      await CreateNewUsersAsync(10);

      var identity = Guid.NewGuid().ToUserIdentity();

      var testUserEntity = await _userRepository.GetUserAsync(identity, CancellationToken);

      Assert.IsNull(testUserEntity);
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_User_With_Defined_Email()
    {
      var allUserEntityCollection = await CreateNewUsersAsync(10);
      var controlUserEntity = allUserEntityCollection[2];

      var userName = controlUserEntity.Email!;

      var testUserEntity = await _userRepository.GetUserAsync(userName, CancellationToken);

      Assert.IsNotNull(testUserEntity);

      UserRepositoryTest.AreEqual(controlUserEntity, testUserEntity);
      IsDetached(testUserEntity);
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_User_With_Defined_Case_Insensitive_Email()
    {
      var allUserEntityCollection = await CreateNewUsersAsync(10);
      var controlUserEntity = allUserEntityCollection[2];

      var userName = controlUserEntity.Email!.ToUpper();

      var testUserEntity = await _userRepository.GetUserAsync(userName, CancellationToken);

      Assert.IsNotNull(testUserEntity);

      UserRepositoryTest.AreEqual(controlUserEntity, testUserEntity);
      IsDetached(testUserEntity);
    }

    [TestMethod]
    public async Task GetUserAsync_Should_Return_Null_Unknown_Email()
    {
      await CreateNewUsersAsync(10);

      var userName = Guid.NewGuid().ToString();

      var testUserEntity = await _userRepository.GetUserAsync(userName, CancellationToken);

      Assert.IsNull(testUserEntity);
    }

    private static UserEntity GenerateNewUser() => new UserEntity
    {
      Name = Guid.NewGuid().ToString(),
      Email = $"{Guid.NewGuid()}@test.test",
      PasswordHash = Guid.NewGuid().ToString(),
    };

    private async Task<UserEntity> CreateNewUserAsync()
    {
      var userEntity = UserRepositoryTest.GenerateNewUser();

      var userEntityEntry = DbContext.Add(userEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      userEntityEntry.State = EntityState.Detached;

      return userEntity;
    }

    private async Task<UserEntity[]> CreateNewUsersAsync(int users)
    {
      var userEntityCollection = new List<UserEntity>();

      for (int i = 0; i < users; i++)
      {
        userEntityCollection.Add(await CreateNewUserAsync());
      }

      return userEntityCollection.OrderBy(entity => entity.Name)
                                 .ToArray();
    }

    private static void AreEqual(UserEntity control, UserEntity test)
    {
      Assert.AreEqual(control.Name, test.Name);
      Assert.AreEqual(control.Email, test.Email);
      Assert.AreEqual(control.PasswordHash, test.PasswordHash);
    }

    private void IsDetached(UserEntity userEntity)
      => Assert.AreEqual(EntityState.Detached, DbContext.Entry(userEntity).State);
  }
}
