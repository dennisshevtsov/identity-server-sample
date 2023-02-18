// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.Mapping.Test
{
  using IdentityServer4.Models;

  using IdentityServerSample.ApplicationCore.Entities;

  [TestClass]
  public sealed class ScopeMappingTest : MappingTestBase
  {
    [TestMethod]
    public void Map_Should_Create_Api_Scope()
    {
      var scopeEntity = new ScopeEntity
      {
        Name = Guid.NewGuid().ToString(),
        DisplayName = Guid.NewGuid().ToString(),
      };

      var scope = Mapper.Map<ApiScope>(scopeEntity);

      Assert.IsNotNull(scope);

      Assert.AreEqual(scopeEntity.Name, scope.Name);
      Assert.AreEqual(scopeEntity.DisplayName, scope.DisplayName);
    }
  }
}
