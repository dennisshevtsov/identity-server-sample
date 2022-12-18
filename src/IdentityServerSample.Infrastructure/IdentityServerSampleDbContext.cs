// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure
{
  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.Infrastructure.Configurations;

  public sealed class IdentityServerSampleDbContext : DbContext
  {
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new AudienceEntityTypeConfiguration("audiences"));
      modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration("clients"));
      modelBuilder.ApplyConfiguration(new ScopeEntityTypeConfiguration("scopes"));
    }
  }
}
