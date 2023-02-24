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
      var controlAudienceEntityCollection = await CreateNewAudienciesAsync(10);

      var testAudienceEntityCollection =
        await _audienceRepository.GetAudiencesAsync(CancellationToken);

      Assert.AreEqual(controlAudienceEntityCollection.Count, testAudienceEntityCollection.Count);

      for (int i = 0; i < controlAudienceEntityCollection.Count; i++)
      {
        AreEqual(controlAudienceEntityCollection[i], testAudienceEntityCollection[i]);
      }

      AreDetached(testAudienceEntityCollection);
    }

    [TestMethod]
    public async Task GetAudiencesAsync_Should_Return_Audiences_With_Defined_Names()
    {
      var allAudienceEntityCollection = await CreateNewAudienciesAsync(10);
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
        AreEqual(controlAudienceEntityCollection[i], testAudienceEntityCollection[i]);
      }

      AreDetached(testAudienceEntityCollection);
    }

    [TestMethod]
    public async Task GetAudiencesAsync_Should_Return_All_Audiences_For_Empty_Audience_Identities()
    {
      var controlAudienceEntityCollection = await CreateNewAudienciesAsync(10);

      var testAudienceEntityCollection =
        await _audienceRepository.GetAudiencesAsync(null, CancellationToken);

      Assert.AreEqual(controlAudienceEntityCollection.Count, testAudienceEntityCollection.Count);

      for (int i = 0; i < controlAudienceEntityCollection.Count; i++)
      {
        AreEqual(controlAudienceEntityCollection[i], testAudienceEntityCollection[i]);
      }

      AreDetached(testAudienceEntityCollection);
    }

    private async Task<AudienceEntity> CreateNewAudienceAsync()
    {
      var audienceEntity = new AudienceEntity
      {
        AudienceName = Guid.NewGuid().ToString(),
        DisplayName = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var audienceEntityEntry = DbContext.Add(audienceEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      audienceEntityEntry.State = EntityState.Detached;

      return audienceEntity;
    }

    private async Task<List<AudienceEntity>> CreateNewAudienciesAsync(int audiences)
    {
      var audienceEntityCollection = new List<AudienceEntity>();

      for (int i = 0; i < audiences; i++)
      {
        audienceEntityCollection.Add(await CreateNewAudienceAsync());
      }

      return audienceEntityCollection.OrderBy(entity => entity.AudienceName)
                                     .ToList();
    }

    private void AreEqual(AudienceEntity control, AudienceEntity test)
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
