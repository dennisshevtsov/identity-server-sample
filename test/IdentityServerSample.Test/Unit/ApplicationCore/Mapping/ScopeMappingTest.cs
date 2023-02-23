// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Mapping.Test
{
  using Microsoft.Extensions.DependencyInjection;

  [TestClass]
  public sealed class ScopeMappingTest
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
    public void Map_Should_Create_GetScopesResponseDto()
    {
      var scopeEntityCollection = new[]
      {
        new ScopeEntity
        {
          ScopeName = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
        new ScopeEntity
        {
          ScopeName = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
        new ScopeEntity
        {
          ScopeName = Guid.NewGuid().ToString(),
          DisplayName = Guid.NewGuid().ToString(),
        },
      };

      var getScopesResponseDto = _mapper.Map<GetScopesResponseDto>(scopeEntityCollection);

      Assert.IsNotNull(getScopesResponseDto);

      Assert.IsNotNull(getScopesResponseDto.Scopes);
      Assert.AreEqual(scopeEntityCollection.Length, getScopesResponseDto.Scopes!.Length);

      var scopeDtoCollection = getScopesResponseDto.Scopes.ToArray();

      Assert.AreEqual(scopeEntityCollection[0].ScopeName, scopeDtoCollection[0].ScopeName);
      Assert.AreEqual(scopeEntityCollection[0].DisplayName, scopeDtoCollection[0].DisplayName);

      Assert.AreEqual(scopeEntityCollection[1].ScopeName, scopeDtoCollection[1].ScopeName);
      Assert.AreEqual(scopeEntityCollection[1].DisplayName, scopeDtoCollection[1].DisplayName);

      Assert.AreEqual(scopeEntityCollection[2].ScopeName, scopeDtoCollection[2].ScopeName);
      Assert.AreEqual(scopeEntityCollection[2].DisplayName, scopeDtoCollection[2].DisplayName);
    }
  }
}
