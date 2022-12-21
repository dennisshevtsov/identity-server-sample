// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Test.Integration.Infrastructure
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

  using IdentityServerSample.ApplicationCore.Entities;

  [TestClass]
  public sealed class DbContextTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private IDisposable _disposable;

    private DbContext _dbContext;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                    .Build();

      var scope = new ServiceCollection().AddDatabase(configuration)
                                         .BuildServiceProvider()
                                         .CreateScope();

      _disposable = scope;

      _dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
      _dbContext.Database.EnsureCreated();
    }

    [TestCleanup]
    public void Cleanup()
    {
      _dbContext?.Database?.EnsureDeleted();
      _disposable?.Dispose();
    }

    [TestMethod]
    public async Task Add_Should_Create_Scope()
    {
      var scopeName = Guid.NewGuid().ToString();
      var scopeDesciption = Guid.NewGuid().ToString();
      var scopeDisplayName = Guid.NewGuid().ToString();

      var scopeEntity = new ScopeEntity
      {
        Name = scopeName,
        Description = scopeDesciption,
        DisplayName = scopeDisplayName,
      };

      var scopeEntityEntry = _dbContext.Add(scopeEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      scopeEntityEntry.State = EntityState.Detached;

      var dbScopeEntity =
        await _dbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == scopeName)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(dbScopeEntity);

      Assert.AreEqual(scopeName, dbScopeEntity!.Name);
      Assert.AreEqual(scopeDesciption, dbScopeEntity!.Description);
      Assert.AreEqual(scopeDisplayName, dbScopeEntity!.DisplayName);
    }

    [TestMethod]
    public async Task Update_Should_Update_Scope()
    {
      var scopeName = Guid.NewGuid().ToString();
      var creatingScopeDesciption = Guid.NewGuid().ToString();
      var creatingScopeDisplayName = Guid.NewGuid().ToString();

      var creatingScopeEntity = new ScopeEntity
      {
        Name = scopeName,
        Description = creatingScopeDesciption,
        DisplayName = creatingScopeDisplayName,
      };

      var creatingScopeEntityEntry = _dbContext.Add(creatingScopeEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      creatingScopeEntityEntry.State = EntityState.Detached;

      var updatingScopeDesciption = Guid.NewGuid().ToString();
      var updatingScopeDisplayName = Guid.NewGuid().ToString();

      var updatingScopeEntity = new ScopeEntity
      {
        Name = scopeName,
        Description = updatingScopeDesciption,
        DisplayName = updatingScopeDisplayName,
      };

      var updatingScopeEntityEntry = _dbContext.Attach(updatingScopeEntity);

      updatingScopeEntityEntry.State = EntityState.Modified;

      await _dbContext.SaveChangesAsync(_cancellationToken);

      updatingScopeEntityEntry.State = EntityState.Detached;

      var dbScopeEntity =
        await _dbContext.Set<ScopeEntity>()
                        .Where(entity => entity.Name == scopeName)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(dbScopeEntity);

      Assert.AreEqual(scopeName, dbScopeEntity!.Name);
      Assert.AreEqual(updatingScopeDesciption, dbScopeEntity!.Description);
      Assert.AreEqual(updatingScopeDisplayName, dbScopeEntity!.DisplayName);
    }
  }
}
