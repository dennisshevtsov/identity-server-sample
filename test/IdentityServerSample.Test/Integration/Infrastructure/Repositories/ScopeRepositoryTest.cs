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
  public sealed class ScopeRepositoryTest : DbIntegrationTestBase
  {
#pragma warning disable CS8618
    private IScopeRepository _scopeRepository;
#pragma warning restore CS8618

    protected override void InitializeInternal()
    {
      _scopeRepository = ServiceProvider.GetRequiredService<IScopeRepository>();
    }
  }
}
