// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Mapping.Test
{
  using Microsoft.Extensions.DependencyInjection;

  [TestClass]
  public sealed class AudienceMappingTest
  {
#pragma warning disable CS8618
    private IDisposable _disposable;
    private IMapper _mapper;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      var serviceScope = new ServiceCollection().SetUpApplicationCoreMapping()
                                                .BuildServiceProvider()
                                                .CreateScope();

      _disposable = serviceScope;
      _mapper = serviceScope.ServiceProvider.GetRequiredService<IMapper>();
    }

    [TestCleanup]
    public void Cleanup()
    {
      _disposable?.Dispose();
    }

    [TestMethod]
    public void Map_Should_Create_GetClientsResponseDto()
    {
      var audienceEntityCollection = new[]
      {
        new AudienceEntity
        {
          AudienceName = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
        new AudienceEntity
        {
          AudienceName = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
        new AudienceEntity
        {
          AudienceName = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
      };

      var getAudiencesResponseDto = _mapper.Map<GetAudiencesResponseDto>(audienceEntityCollection);

      Assert.IsNotNull(getAudiencesResponseDto);

      Assert.IsNotNull(getAudiencesResponseDto.Audiences);
      Assert.AreEqual(audienceEntityCollection.Length, getAudiencesResponseDto.Audiences!.Length);

      var audienceDtoCollection = getAudiencesResponseDto.Audiences.ToArray();

      Assert.AreEqual(audienceEntityCollection[0].AudienceName, audienceDtoCollection[0].AudienceName);
      Assert.AreEqual(audienceEntityCollection[0].DisplayName, audienceDtoCollection[0].DisplayName);

      Assert.AreEqual(audienceEntityCollection[1].AudienceName, audienceDtoCollection[1].AudienceName);
      Assert.AreEqual(audienceEntityCollection[1].DisplayName, audienceDtoCollection[1].DisplayName);

      Assert.AreEqual(audienceEntityCollection[2].AudienceName, audienceDtoCollection[2].AudienceName);
      Assert.AreEqual(audienceEntityCollection[2].DisplayName, audienceDtoCollection[2].DisplayName);
    }
  }
}
