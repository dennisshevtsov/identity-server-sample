﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Test.Unit.Mapping
{
  using Microsoft.Extensions.DependencyInjection;

  [TestClass]
  public sealed class ClientMappingTest
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
      var clientEntityCollection = new[]
      {
        new ClientEntity
        {
          Name = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
        new ClientEntity
        {
          Name = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
        new ClientEntity
        {
          Name = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
      };

      var getClientsResponseDto = _mapper.Map<GetClientsResponseDto>(clientEntityCollection);

      Assert.IsNotNull(getClientsResponseDto);

      Assert.IsNotNull(getClientsResponseDto.Clients);
      Assert.AreEqual(clientEntityCollection.Length, getClientsResponseDto.Clients!.Length);

      var clientDtoCollection = getClientsResponseDto.Clients.ToArray();

      Assert.AreEqual(clientEntityCollection[0].Name, clientDtoCollection[0].Name);
      Assert.AreEqual(clientEntityCollection[0].DisplayName, clientDtoCollection[0].DisplayName);

      Assert.AreEqual(clientEntityCollection[1].Name, clientDtoCollection[1].Name);
      Assert.AreEqual(clientEntityCollection[1].DisplayName, clientDtoCollection[1].DisplayName);

      Assert.AreEqual(clientEntityCollection[2].Name, clientDtoCollection[2].Name);
      Assert.AreEqual(clientEntityCollection[2].DisplayName, clientDtoCollection[2].DisplayName);
    }
  }
}
