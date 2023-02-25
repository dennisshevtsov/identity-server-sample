// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Allows configuration for an entity type.</summary>
  public sealed class ScopeEntityTypeConfiguration : IEntityTypeConfiguration<ScopeEntity>
  {
    private readonly string _containerName;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.Infrastructure.Configurations.ClientEntityTypeConfiguration"/> class.</summary>
    /// <param name="containerName">An object that represents a name of a container.</param>
    public ScopeEntityTypeConfiguration(string containerName)
    {
      _containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
    }

    /// <summary>Configures the entity of the <see cref="IdentityServerSample.ApplicationCore.Entities.ScopeEntity"/> type.</summary>
    /// <param name="builder">An object that provides a simple API to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<ScopeEntity> builder)
    {
      builder.ToContainer(_containerName);

      builder.HasKey(entity => entity.ScopeName);
      builder.HasPartitionKey(entity => entity.ScopeName);

      builder.HasNoDiscriminator();

      builder.Property(entity => entity.ScopeName).ToJsonProperty("id");
      builder.Property(entity => entity.DisplayName).ToJsonProperty("displayName");
      builder.Property(entity => entity.Description).ToJsonProperty("description");
      builder.Property(entity => entity.Standard).ToJsonProperty("standard");
    }
  }
}
