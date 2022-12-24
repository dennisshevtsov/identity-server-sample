// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Test.Unit.Mapping
{
  using AutoMapper;
  using Microsoft.Extensions.DependencyInjection;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Entities;

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
      var serviceScope = new ServiceCollection().AddMapping()
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
          Name = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
        new AudienceEntity
        {
          Name = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
        new AudienceEntity
        {
          Name = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
      };

      var getAudiencesResponseDto = _mapper.Map<GetAudiencesResponseDto>(audienceEntityCollection);

      Assert.IsNotNull(getAudiencesResponseDto);

      Assert.IsNotNull(getAudiencesResponseDto.Audiences);
      Assert.AreEqual(audienceEntityCollection.Length, getAudiencesResponseDto.Audiences!.Length);

      var audienceDtoCollection = getAudiencesResponseDto.Audiences.ToArray();

      Assert.AreEqual(audienceEntityCollection[0].Name, audienceDtoCollection[0].Name);
      Assert.AreEqual(audienceEntityCollection[0].DisplayName, audienceDtoCollection[0].DisplayName);

      Assert.AreEqual(audienceEntityCollection[1].Name, audienceDtoCollection[1].Name);
      Assert.AreEqual(audienceEntityCollection[1].DisplayName, audienceDtoCollection[1].DisplayName);

      Assert.AreEqual(audienceEntityCollection[2].Name, audienceDtoCollection[2].Name);
      Assert.AreEqual(audienceEntityCollection[2].DisplayName, audienceDtoCollection[2].DisplayName);
    }
  }
}
