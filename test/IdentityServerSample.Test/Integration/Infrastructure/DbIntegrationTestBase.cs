// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Test
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

  public abstract class DbIntegrationTestBase
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private IServiceScope _scope;

    private DbContext _dbContext;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                    .Build();

      _scope = new ServiceCollection().SetUpDatabase(configuration)
                                      .BuildServiceProvider()
                                      .CreateScope();

      _dbContext = _scope.ServiceProvider.GetRequiredService<DbContext>();
      _dbContext.Database.EnsureCreated();

      InitializeInternal();
    }

    [TestCleanup]
    public void Cleanup()
    {
      _dbContext?.Database?.EnsureDeleted();
      _scope?.Dispose();
    }

    protected CancellationToken CancellationToken { get { return _cancellationToken; } }

    protected IServiceProvider ServiceProvider { get { return _scope.ServiceProvider; } }

    protected DbContext DbContext { get { return _dbContext; } }

    protected virtual void InitializeInternal() { }
  }
}
