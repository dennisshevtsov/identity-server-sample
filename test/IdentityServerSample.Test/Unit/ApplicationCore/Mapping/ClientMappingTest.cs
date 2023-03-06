// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Mapping.Test
{
  [TestClass]
  public sealed class ClientMappingTest : MappingTestBase
  {
    [TestMethod]
    public void Map_Should_Create_GetClientsResponseDto()
    {
      var clientEntityCollection = new[]
      {
        new ClientEntity
        {
          ClientName = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
        new ClientEntity
        {
          ClientName = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
        new ClientEntity
        {
          ClientName = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
      };

      var getClientsResponseDto = Mapper.Map<GetClientsResponseDto>(clientEntityCollection);

      Assert.IsNotNull(getClientsResponseDto);

      Assert.IsNotNull(getClientsResponseDto.Clients);
      Assert.AreEqual(clientEntityCollection.Length, getClientsResponseDto.Clients!.Length);

      var clientDtoCollection = getClientsResponseDto.Clients.ToArray();

      Assert.AreEqual(clientEntityCollection[0].ClientName, clientDtoCollection[0].ClientName);
      Assert.AreEqual(clientEntityCollection[0].DisplayName, clientDtoCollection[0].DisplayName);

      Assert.AreEqual(clientEntityCollection[1].ClientName, clientDtoCollection[1].ClientName);
      Assert.AreEqual(clientEntityCollection[1].DisplayName, clientDtoCollection[1].DisplayName);

      Assert.AreEqual(clientEntityCollection[2].ClientName, clientDtoCollection[2].ClientName);
      Assert.AreEqual(clientEntityCollection[2].DisplayName, clientDtoCollection[2].DisplayName);
    }

    [TestMethod]
    public void Map_Should_Create_GetClientResponseDto()
    {
      var controlClientEntity = new ClientEntity
      {
        ClientName = Guid.NewGuid().ToString(),
        DisplayName = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Scopes = new List<string>
        {
          Guid.NewGuid().ToString(),
        },
        RedirectUris = new List<string>
        {
          Guid.NewGuid().ToString(),
        },
        PostRedirectUris = new List<string>
        {
          Guid.NewGuid().ToString(),
        },
        CorsOrigins = new List<string>
        {
          Guid.NewGuid().ToString(),
        },
      };

      var actualGetClientResponseDto = Mapper.Map<GetClientResponseDto>(controlClientEntity);

      Assert.IsNotNull(actualGetClientResponseDto);

      Assert.AreEqual(controlClientEntity.ClientName, actualGetClientResponseDto.ClientName);
      Assert.AreEqual(controlClientEntity.DisplayName, actualGetClientResponseDto.DisplayName);
      Assert.AreEqual(controlClientEntity.Description, actualGetClientResponseDto.Description);

      ClientMappingTest.AreEqual(controlClientEntity.Scopes, actualGetClientResponseDto.Scopes);
      ClientMappingTest.AreEqual(controlClientEntity.RedirectUris, actualGetClientResponseDto.RedirectUris);
      ClientMappingTest.AreEqual(controlClientEntity.PostRedirectUris, actualGetClientResponseDto.PostRedirectUris);
      ClientMappingTest.AreEqual(controlClientEntity.CorsOrigins, actualGetClientResponseDto.CorsOrigins);
    }

    private static void AreEqual(IReadOnlyList<string> control, IReadOnlyList<string>? actual)
    {
      Assert.IsNotNull(actual);
      Assert.AreEqual(control.Count, actual.Count);

      for (var i = 0; i < control.Count; i++)
      {
        Assert.AreEqual(control[i], actual[i]);
      }
    }
  }
}
