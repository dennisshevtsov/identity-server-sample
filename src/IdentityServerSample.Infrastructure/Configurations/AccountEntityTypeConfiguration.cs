// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Allows configuration for an entity type.</summary>
  public sealed class AccountEntityTypeConfiguration : IEntityTypeConfiguration<AccountEntity>
  {
    private readonly string _containerName;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.Infrastructure.Configurations.AccountEntityTypeConfiguration"/> class.</summary>
    /// <param name="containerName">An object that represents a name of a container.</param>
    public AccountEntityTypeConfiguration(string containerName)
    {
      _containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
    }

    /// <summary>Configures the entity of the <see cref="IdentityServerSample.ApplicationCore.Entities.AccountEntity"/> type.</summary>
    /// <param name="builder">An object that provides a simple API to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
      builder.ToContainer(_containerName);

      builder.HasKey(entity => entity.AccountId);
      builder.HasPartitionKey(entity => entity.AccountId);

      builder.HasNoDiscriminator();

      builder.Property(entity => entity.AccountId).ToJsonProperty("accountId");
      builder.Property(entity => entity.Name).ToJsonProperty("name");
      builder.Property(entity => entity.Email).ToJsonProperty("email");
      builder.Property(entity => entity.PasswordHash).ToJsonProperty("passwordHash");
    }
  }
}
