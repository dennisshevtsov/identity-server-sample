// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Options;

  using IdentityServerSample.Infrastructure.Configurations;

  public sealed class IdentityServerSampleDbContext : DbContext
  {
    private readonly IOptions<DatabaseOptions> _databaseOptions;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.Infrastructure.IdentityServerSampleDbContext"/> class.</summary>
    /// <param name="dbContextOptions">An object that represents the options to be used by a <see cref="Microsoft.EntityFrameworkCore.DbContext" />.</param>
    /// <param name="databaseOptions">An object that represents settings of a database.</param>
    public IdentityServerSampleDbContext(
      DbContextOptions dbContextOptions,
      IOptions<DatabaseOptions> databaseOptions)
      : base(dbContextOptions)
    {
      _databaseOptions = databaseOptions ?? throw new ArgumentNullException(nameof(databaseOptions));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new AudienceEntityTypeConfiguration(_databaseOptions.Value.AudienceContainerName));
      modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration(_databaseOptions.Value.ClientContainerName));
      modelBuilder.ApplyConfiguration(new ScopeEntityTypeConfiguration(_databaseOptions.Value.ScopeContainerName));
      modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration(_databaseOptions.Value.UserContainerName));
    }
  }
}
