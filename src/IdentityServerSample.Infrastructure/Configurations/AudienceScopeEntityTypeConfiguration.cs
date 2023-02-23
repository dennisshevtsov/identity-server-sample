// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Configurations
{
  using Microsoft.EntityFrameworkCore.Metadata.Builders;
  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Allows configuration for an entity type.</summary>
  public sealed class AudienceScopeEntityTypeConfiguration : IEntityTypeConfiguration<AudienceScopeEntity>
  {
    private const string DescriminatorPropertyName = "_type";

    private readonly string _containerName;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.Infrastructure.Configurations.AudienceScopeEntityTypeConfiguration"/> class.</summary>
    /// <param name="containerName">An object that represents a name of a container.</param>
    public AudienceScopeEntityTypeConfiguration(string containerName)
    {
      _containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
    }

    /// <summary>Configures the entity of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceScopeEntity"/> type.</summary>
    /// <param name="builder">An object that provides a simple API to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<AudienceScopeEntity> builder)
    {
      builder.ToContainer(_containerName);

      builder.HasKey(entity => entity.ScopeName);
      builder.HasPartitionKey(entity => entity.AudienceName);

      builder.Property(typeof(string), AudienceScopeEntityTypeConfiguration.DescriminatorPropertyName);
      builder.HasDiscriminator(AudienceScopeEntityTypeConfiguration.DescriminatorPropertyName, typeof(string));

      builder.Property(entity => entity.ScopeName).ToJsonProperty("id");
      builder.Property(entity => entity.AudienceName).ToJsonProperty("audienceName");
    }
  }
}
