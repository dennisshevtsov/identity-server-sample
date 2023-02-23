// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.Mapping.Test
{
  using IdentityServer4.Models;

  [TestClass]
  public sealed class AudienceMappingTest : MappingTestBase
  {
    [TestMethod]
    public void Map_Should_Create_Api_Resource()
    {
      var audienceEntity = new AudienceEntity
      {
        AudienceName = Guid.NewGuid().ToString(),
        DisplayName = Guid.NewGuid().ToString(),
      };

      var apiResource = Mapper.Map<ApiResource>(audienceEntity);

      Assert.IsNotNull(apiResource);

      Assert.AreEqual(audienceEntity.AudienceName, apiResource.Name);
      Assert.AreEqual(audienceEntity.DisplayName, apiResource.DisplayName);
    }
  }
}
