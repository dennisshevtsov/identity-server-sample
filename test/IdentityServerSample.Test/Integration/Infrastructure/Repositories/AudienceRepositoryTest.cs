﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories.Test
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;

  using IdentityServerSample.Infrastructure.Test;
  using IdentityServerSample.ApplicationCore.Identities;

  [TestClass]
  public sealed class AudienceRepositoryTest : DbIntegrationTestBase
  {
#pragma warning disable CS8618
    private IAudienceRepository _audienceRepository;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _audienceRepository = ServiceProvider.GetRequiredService<IAudienceRepository>();
    }

    [TestMethod]
    public async Task GetAudiencesAsync_Should_Return_All_Audiences()
    {
      var controlAudienceEntityCollection = await CreateNewAudiencesAsync(10);

      var testAudienceEntityCollection =
        await _audienceRepository.GetAudiencesAsync(CancellationToken);

      Assert.AreEqual(controlAudienceEntityCollection.Count, testAudienceEntityCollection.Count);

      for (int i = 0; i < controlAudienceEntityCollection.Count; i++)
      {
        AudienceRepositoryTest.AreEqual(
          controlAudienceEntityCollection[i], testAudienceEntityCollection[i]);
      }

      AreDetached(testAudienceEntityCollection);
    }

    [TestMethod]
    public async Task GetAudiencesAsync_Should_Return_Audiences_With_Defined_Names()
    {
      var allAudienceEntityCollection = await CreateNewAudiencesAsync(10);
      var controlAudienceEntityCollection =
        allAudienceEntityCollection.Where((entity, index) => index % 2 == 0)
                                   .ToList();

      var audienceIdentities =
        controlAudienceEntityCollection.Select(entity => entity.AudienceName!)
                                       .ToAudienceIdentities();

      var testAudienceEntityCollection =
        await _audienceRepository.GetAudiencesAsync(audienceIdentities, CancellationToken);

      Assert.AreEqual(controlAudienceEntityCollection.Count, testAudienceEntityCollection.Count);

      for (int i = 0; i < controlAudienceEntityCollection.Count; i++)
      {
        AudienceRepositoryTest.AreEqual(
          controlAudienceEntityCollection[i], testAudienceEntityCollection[i]);
      }

      AreDetached(testAudienceEntityCollection);
    }

    [TestMethod]
    public async Task GetAudiencesAsync_Should_Return_All_Audiences_For_Empty_Audience_Identities()
    {
      var controlAudienceEntityCollection = await CreateNewAudiencesAsync(10);

      var testAudienceEntityCollection =
        await _audienceRepository.GetAudiencesAsync(null, CancellationToken);

      Assert.AreEqual(controlAudienceEntityCollection.Count, testAudienceEntityCollection.Count);

      for (int i = 0; i < controlAudienceEntityCollection.Count; i++)
      {
        AudienceRepositoryTest.AreEqual(
          controlAudienceEntityCollection[i], testAudienceEntityCollection[i]);
      }

      AreDetached(testAudienceEntityCollection);
    }

    [TestMethod]
    public async Task GetAudienceAsync_Should_Return_Audience()
    {
      var allAudienceEntityCollection = await CreateNewAudiencesAsync(10);
      var controlAudienceEntity = allAudienceEntityCollection[3];

      IAudienceIdentity identity = controlAudienceEntity;

      var testAudienceEntity =
        await _audienceRepository.GetAudienceAsync(identity, CancellationToken);

      Assert.IsNotNull(testAudienceEntity);
      AudienceRepositoryTest.AreEqual(controlAudienceEntity, testAudienceEntity);
      IsDetached(testAudienceEntity);
    }

    [TestMethod]
    public async Task GetAudienceAsync_Should_Return_Null()
    {
      await CreateNewAudiencesAsync(10);

      var identity = Guid.NewGuid().ToString().ToAudienceIdentity();

      var testAudienceEntity =
        await _audienceRepository.GetAudienceAsync(identity, CancellationToken);

      Assert.IsNull(testAudienceEntity);
    }

    [TestMethod]
    public async Task AddAudienceAsync_Should_Add_New_Audience()
    {
      await CreateNewAudiencesAsync(10);

      var controlAudienceEntity = AudienceRepositoryTest.GenerateNewAudience();

      await _audienceRepository.AddAudienceAsync(controlAudienceEntity, CancellationToken);

      var actualAudienceEntity =
         await DbContext.Set<AudienceEntity>()
                        .AsNoTracking()
                        .WithPartitionKey(controlAudienceEntity.AudienceName!)
                        .SingleOrDefaultAsync(CancellationToken);

      Assert.IsNotNull(actualAudienceEntity);
      AudienceRepositoryTest.AreEqual(controlAudienceEntity, actualAudienceEntity);
      IsDetached(controlAudienceEntity);
    }

    private static AudienceEntity GenerateNewAudience()
      => new AudienceEntity
      {
        AudienceName = Guid.NewGuid().ToString(),
        DisplayName = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

    private async Task<AudienceEntity> CreateNewAudienceAsync()
    {
      var audienceEntity = AudienceRepositoryTest.GenerateNewAudience();

      var audienceEntityEntry = DbContext.Add(audienceEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      audienceEntityEntry.State = EntityState.Detached;

      return audienceEntity;
    }

    private async Task<List<AudienceEntity>> CreateNewAudiencesAsync(int audiences)
    {
      var audienceEntityCollection = new List<AudienceEntity>();

      for (int i = 0; i < audiences; i++)
      {
        audienceEntityCollection.Add(await CreateNewAudienceAsync());
      }

      return audienceEntityCollection.OrderBy(entity => entity.AudienceName)
                                     .ToList();
    }

    private static void AreEqual(AudienceEntity control, AudienceEntity test)
    {
      Assert.AreEqual(control.AudienceName, test.AudienceName);
      Assert.AreEqual(control.DisplayName, test.DisplayName);
      Assert.AreEqual(control.Description, test.Description);
    }

    private void IsDetached(AudienceEntity audienceEntity)
      => Assert.AreEqual(EntityState.Detached, DbContext.Entry(audienceEntity).State);

    private void AreDetached(List<AudienceEntity> audienceEntityCollection)
    {
      foreach (var audienceEntity in audienceEntityCollection)
      {
        IsDetached(audienceEntity);
      }
    }
  }
}
