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
    public async Task GetClientAsync_Should_Return_With_Defined_Name()
    {
      var allClientEntityCollection = await CreateNewClientsAsync(10);
      var controlClientEntity = allClientEntityCollection[2];

      var clientName = controlClientEntity.Name!;

      var clientEntity =
        await _clientRepository.GetClientAsync(clientName, CancellationToken);

      Assert.IsNotNull(clientEntity);

      AreEqual(controlClientEntity, clientEntity);
      IsDetached(clientEntity);
    }

    [TestMethod]
    public async Task GetClientAsync_Should_Return_Null()
    {
      var allClientEntityCollection = await CreateNewClientsAsync(10);

      var clientName = Guid.NewGuid().ToString();

      var clientEntity =
        await _clientRepository.GetClientAsync(clientName, CancellationToken);

      Assert.IsNull(clientEntity);
    }

    [TestMethod]
    public async Task GetClientsAsync_Should_Return_All_Clients()
    {
      var controlClientEntityCollection = await CreateNewClientsAsync(10);

      var testClientEntityCollection =
        await _clientRepository.GetClientsAsync(CancellationToken);

      Assert.AreEqual(controlClientEntityCollection.Length, testClientEntityCollection.Length);

      for (int i = 0; i < controlClientEntityCollection.Length; i++)
      {
        AreEqual(controlClientEntityCollection[i], testClientEntityCollection[i]);
      }

      AreDetached(testClientEntityCollection);
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

    private void AreEqual(ClientEntity control, ClientEntity test)
    {
      Assert.AreEqual(control.Name, test.Name);
      Assert.AreEqual(control.DisplayName, test.DisplayName);
      Assert.AreEqual(control.Description, test.Description);

      Assert.IsNotNull(test.Scopes);
      Assert.AreEqual(control.Scopes!.Count, test.Scopes.Count);

      for (int i = 0; i < control.Scopes.Count; i++)
      {
        Assert.AreEqual(control.Scopes[i], test.Scopes[i]);
      }

      Assert.IsNotNull(test.RedirectUris);
      Assert.AreEqual(control.RedirectUris!.Count, test.RedirectUris.Count);

      for (int i = 0; i < control.RedirectUris.Count; i++)
      {
        Assert.AreEqual(control.RedirectUris[i], test.RedirectUris[i]);
      }

      Assert.IsNotNull(test.PostRedirectUris);
      Assert.AreEqual(control.PostRedirectUris!.Count, test.PostRedirectUris.Count);

      for (int i = 0; i < control.PostRedirectUris.Count; i++)
      {
        Assert.AreEqual(control.PostRedirectUris[i], test.PostRedirectUris[i]);
      }
    }

    private void IsDetached(ClientEntity clientEntity)
      => Assert.AreEqual(EntityState.Detached, DbContext.Entry(clientEntity).State);

    private void AreDetached(ClientEntity[] clientEntityCollection)
    {
      foreach (var clientEntity in clientEntityCollection)
      {
        IsDetached(clientEntity);
      }
    }
  }
}
