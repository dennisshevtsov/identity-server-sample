﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Configurations
{
  using Microsoft.EntityFrameworkCore.Metadata.Builders;
  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Allows configuration for an entity type.</summary>
  public sealed class IdentityResourceEntityTypeConfiguration : IEntityTypeConfiguration<IdentityResourceEntity>
  {
    private readonly string _containerName;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.Infrastructure.Configurations.IdentityResourceEntityTypeConfiguration"/> class.</summary>
    /// <param name="containerName">An object that represents a name of a container.</param>
    public IdentityResourceEntityTypeConfiguration(string containerName)
    {
      _containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
    }

    /// <summary>Configures the entity of the <see cref="IdentityServerSample.ApplicationCore.Entities.IdentityResourceEntity"/> type.</summary>
    /// <param name="builder">An object that provides a simple API to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<IdentityResourceEntity> builder)
    {
      builder.ToContainer(_containerName);

      builder.HasKey(entity => entity.Name);
      builder.HasPartitionKey(entity => entity.Name);

      builder.HasNoDiscriminator();

      builder.Property(entity => entity.Name).ToJsonProperty("name");
      builder.Property(entity => entity.DisplayName).ToJsonProperty("displayName");
      builder.Property(entity => entity.Description).ToJsonProperty("description");
    }
  }
}
