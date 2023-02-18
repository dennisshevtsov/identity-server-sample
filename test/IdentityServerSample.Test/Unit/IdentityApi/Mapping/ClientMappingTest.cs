// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.Mapping.Test
{
  using IdentityServer4.Models;

  using IdentityServerSample.ApplicationCore.Entities;

  [TestClass]
  public sealed class ClientMappingTest : MappingTestBase
  {
    [TestMethod]
    public void Map_Should_Create_Client()
    {
      var clientEntity = new ClientEntity
      {
        Name = Guid.NewGuid().ToString(),
        DisplayName = Guid.NewGuid().ToString(),
        Scopes = new List<string>
        {
          Guid.NewGuid().ToString(),
          Guid.NewGuid().ToString(),
        },
        RedirectUris = new List<string>
        {
          Guid.NewGuid().ToString(),
          Guid.NewGuid().ToString(),
        },
        PostRedirectUris = new List<string>
        {
          Guid.NewGuid().ToString(),
          Guid.NewGuid().ToString(),
        },
      };

      var client = Mapper.Map<Client>(clientEntity);

      Assert.IsNotNull(client);

      Assert.AreEqual(clientEntity.Name, client.ClientId);
      Assert.AreEqual(clientEntity.DisplayName, client.ClientName);
      Assert.AreEqual(false, client.RequireClientSecret);

      AreEqual(GrantTypes.Code, client.AllowedGrantTypes);
      AreEqual(clientEntity.Scopes, client.AllowedScopes);
      AreEqual(clientEntity.RedirectUris, client.RedirectUris);
      AreEqual(clientEntity.PostRedirectUris, client.PostLogoutRedirectUris);
    }

    private static void AreEqual(IEnumerable<string> control, IEnumerable<string>? test)
    {
      Assert.IsNotNull(test);
      Assert.AreEqual(control.Count(), test.Count());

      foreach (var value in test)
      {
        Assert.IsTrue(control.Contains(value));
      }
    }
  }
}
