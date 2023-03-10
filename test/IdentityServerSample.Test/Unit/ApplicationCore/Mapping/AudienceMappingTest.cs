// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Mapping.Test
{
  [TestClass]
  public sealed class AudienceMappingTest : MappingTestBase
  {
    [TestMethod]
    public void Map_Should_Create_GetAudiencesResponseDto()
    {
      var controlAudienceEntityCollection = new[]
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

      var actualGetAudiencesResponseDto = Mapper.Map<GetAudiencesResponseDto>(controlAudienceEntityCollection);

      Assert.IsNotNull(actualGetAudiencesResponseDto);
      AudienceMappingTest.AreEqual(controlAudienceEntityCollection, actualGetAudiencesResponseDto.Audiences);
    }

    [TestMethod]
    public void Map_Should_Create_GetAudienceResponseDto()
    {
      var controlAudienceEntity = new AudienceEntity
      {
        AudienceName = Guid.NewGuid().ToString(),
        DisplayName = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Scopes = new List<string>
        {
          Guid.NewGuid().ToString(),
          Guid.NewGuid().ToString(),
          Guid.NewGuid().ToString(),
        },
      };

      var actualGetAudienceResponseDto = Mapper.Map<GetAudienceResponseDto>(controlAudienceEntity);

      Assert.IsNotNull(actualGetAudienceResponseDto);

      Assert.AreEqual(controlAudienceEntity.AudienceName, actualGetAudienceResponseDto.AudienceName);
      Assert.AreEqual(controlAudienceEntity.DisplayName, actualGetAudienceResponseDto.DisplayName);
      Assert.AreEqual(controlAudienceEntity.Description, actualGetAudienceResponseDto.Description);

      AudienceMappingTest.AreEqual(controlAudienceEntity.Scopes, actualGetAudienceResponseDto.Scopes);
    }

    private static void AreEqual(AudienceEntity[] control, GetAudiencesResponseDto.AudienceDto[]? actual)
    {
      Assert.IsNotNull(actual);
      Assert.AreEqual(control.Length, actual!.Length);

      for (int i = 0; i < control.Length; i++)
      {
        Assert.AreEqual(control[i].AudienceName, actual[i].AudienceName);
        Assert.AreEqual(control[i].DisplayName, actual[i].DisplayName);
      }
    }

    private static void AreEqual(IReadOnlyList<string> control, IReadOnlyList<string>? actual)
    {
      Assert.IsNotNull(actual);
      Assert.AreEqual(control.Count, actual.Count);

      for (int i = 0; i < control.Count; i++)
      {
        Assert.AreEqual(control[i], actual[i]);
      }
    }
  }
}
