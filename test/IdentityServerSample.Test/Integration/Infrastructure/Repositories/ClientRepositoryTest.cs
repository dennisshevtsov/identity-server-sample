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
  public sealed class ClientRepositoryTest : DbIntegrationTestBase
  {
#pragma warning disable CS8618
    private IClientRepository _clientRepository;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _clientRepository = ServiceProvider.GetRequiredService<IClientRepository>();
    }

    [TestMethod]
    public async Task GetClientsAsync_Should_Return_All_Clients()
    {
      var controlClientEntityCollection = await CreateNewClientsAsync(10);

      var testClientEntityCollection =
        await _clientRepository.GetClientsAsync(CancellationToken);

      Assert.AreEqual(controlClientEntityCollection.Length, testClientEntityCollection.Length);
    }

    private async Task<ClientEntity> CreateNewClientAsync()
    {
      var clientEntity = new ClientEntity
      {
        Name = Guid.NewGuid().ToString(),
        DisplayName = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Scopes = new[]
        {
          Guid.NewGuid().ToString(),
          Guid.NewGuid().ToString(),
        },
        RedirectUris = new[]
        {
          Guid.NewGuid().ToString(),
          Guid.NewGuid().ToString(),
        },
        PostRedirectUris = new[]
        {
          Guid.NewGuid().ToString(),
          Guid.NewGuid().ToString(),
        },
      };

      var clientEntityEntry = DbContext.Add(clientEntity);

      await DbContext.SaveChangesAsync(CancellationToken);

      clientEntityEntry.State = EntityState.Detached;

      return clientEntity;
    }

    private async Task<ClientEntity[]> CreateNewClientsAsync(int audiences)
    {
      var clientEntityCollection = new List<ClientEntity>();

      for (int i = 0; i < audiences; i++)
      {
        clientEntityCollection.Add(await CreateNewClientAsync());
      }

      return clientEntityCollection.OrderBy(entity => entity.Name)
                                   .ToArray();
    }
  }
}
