// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Allows configuration for an entity type.</summary>
  public sealed class ClientEntityTypeConfiguration : IEntityTypeConfiguration<ClientEntity>
  {
    private readonly string _containerName;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.Infrastructure.Configurations.ClientEntityTypeConfiguration"/> class.</summary>
    /// <param name="containerName">An object that represents a name of a container.</param>
    public ClientEntityTypeConfiguration(string containerName)
    {
      _containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
    }

    /// <summary>Configures the entity of the <see cref="IdentityServerSample.ApplicationCore.Entities.ClientEntity"/> type.</summary>
    /// <param name="builder">An object that provides a simple API to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<ClientEntity> builder)
    {
      builder.ToContainer(_containerName);

      builder.HasKey(entity => entity.ClientId);
      builder.HasPartitionKey(entity => entity.ClientId);

      builder.HasNoDiscriminator();

      builder.Property(entity => entity.ClientId).ToJsonProperty("id");
      builder.Property(entity => entity.Name).ToJsonProperty("name");

      builder.OwnsMany(entity => entity.Scopes).ToJsonProperty("scopes");
      builder.OwnsMany(entity => entity.RedirectUris).ToJsonProperty("redirectUris");
      builder.OwnsMany(entity => entity.PostRedirectUris).ToJsonProperty("postRedirectUris");
    }
  }
}
