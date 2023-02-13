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

      Assert.AreEqual(controlAudienceEntityCollection.Length, testAudienceEntityCollection.Length);

      for (int i = 0; i < controlAudienceEntityCollection.Length; i++)
      {
        AreEqual(controlAudienceEntityCollection[i], testAudienceEntityCollection[i]);
      }
    }

    [TestMethod]
    public async Task GetAudiencesByNamesAsync_Should_Return_Audiences_With_Defined_Names()
    {
      var allAudienceEntityCollection = await CreateNewAudienciesAsync(10);
      var controlAudienceEntityCollection =
        allAudienceEntityCollection.Where((entity, index) => index % 2 == 0)
                                   .ToArray();

      var audienceNameCollection =
        controlAudienceEntityCollection.Select(entity => entity.Name!)
                                       .ToArray();

      var testAudienceEntityCollection =
        await _audienceRepository.GetAudiencesByNamesAsync(audienceNameCollection, CancellationToken);

      Assert.AreEqual(controlAudienceEntityCollection.Length, testAudienceEntityCollection.Length);

      for (int i = 0; i < controlAudienceEntityCollection.Length; i++)
      {
        AreEqual(controlAudienceEntityCollection[i], testAudienceEntityCollection[i]);
      }
    }

    [TestMethod]
    public async Task GetAudiencesByNamesAsync_Should_Return_All_Audiences()
    {
      var controlAudienceEntityCollection = await CreateNewAudienciesAsync(10);

      var testAudienceEntityCollection =
        await _audienceRepository.GetAudiencesByNamesAsync(null, CancellationToken);

      Assert.AreEqual(controlAudienceEntityCollection.Length, testAudienceEntityCollection.Length);

      for (int i = 0; i < controlAudienceEntityCollection.Length; i++)
      {
        AreEqual(controlAudienceEntityCollection[i], testAudienceEntityCollection[i]);
      }
    }

    [TestMethod]
    public async Task GetAudiencesByScopesAsync_Should_Return_Audiences_That_Relate_To_At_Least_One_Defined_Scope()
    {
      var allAudienceEntityCollection = await CreateNewAudienciesAsync(10);
      var controlAudienceEntityCollection =
        allAudienceEntityCollection.Where((entity, index) => index % 2 == 0)
                                   .ToArray();

      var scopeNameCollection =
        controlAudienceEntityCollection.Select(entity => entity.Scopes!.First())
                                       .ToArray();

      var testAudienceEntityCollection =
        await _audienceRepository.GetAudiencesByScopesAsync(scopeNameCollection, CancellationToken);

      Assert.AreEqual(controlAudienceEntityCollection.Length, testAudienceEntityCollection.Length);

      for (int i = 0; i < controlAudienceEntityCollection.Length; i++)
      {
        AreEqual(controlAudienceEntityCollection[i], testAudienceEntityCollection[i]);
      }
    }

    private async Task<AudienceEntity> CreateNewAudienceAsync()
    {
      var audienceEntity = new AudienceEntity
      {
        Name = Guid.NewGuid().ToString(),
        DisplayName = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Scopes = new[]
        {
          Guid.NewGuid().ToString(),
          Guid.NewGuid().ToString(),
        },
      };

      var audienceEntityEntry = DbContext.Add(audienceEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      audienceEntityEntry.State = EntityState.Detached;

      return audienceEntity;
    }

    private async Task<AudienceEntity[]> CreateNewAudienciesAsync(int audiences)
    {
      var audienceEntityCollection = new List<AudienceEntity>();

      for (int i = 0; i < audiences; i++)
      {
        audienceEntityCollection.Add(await CreateNewAudienceAsync());
      }

      return audienceEntityCollection.OrderBy(entity => entity.Name)
                                     .ToArray();
    }

    private void AreEqual(AudienceEntity control, AudienceEntity test)
    {
      Assert.AreEqual(control.Name, test.Name);
      Assert.AreEqual(control.DisplayName, test.DisplayName);
      Assert.AreEqual(control.Description, test.Description);

      Assert.IsNotNull(test.Scopes);
      Assert.AreEqual(control.Scopes!.Count, test.Scopes.Count);
      Assert.AreEqual(control.Scopes![0], test.Scopes[0]);
      Assert.AreEqual(control.Scopes![1], test.Scopes[1]);
    }
  }
}
