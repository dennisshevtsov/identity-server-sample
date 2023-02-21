// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.ValueGeneration;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Allows configuration for an entity type.</summary>
  public sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
  {
    private const string DescriminatorPropertyName = "_type";

    private readonly string _containerName;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.Infrastructure.Configurations.UserEntityTypeConfiguration"/> class.</summary>
    /// <param name="containerName">An object that represents a name of a container.</param>
    public UserEntityTypeConfiguration(string containerName)
    {
      _containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
    }

    /// <summary>Configures the entity of the <see cref="IdentityServerSample.ApplicationCore.Entities.UserEntity"/> type.</summary>
    /// <param name="builder">An object that provides a simple API to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
      builder.ToContainer(_containerName);

      builder.HasKey(entity => entity.UserId);
      builder.HasPartitionKey(entity => entity.UserId);

      builder.Property(typeof(string), UserEntityTypeConfiguration.DescriminatorPropertyName);
      builder.HasDiscriminator(UserEntityTypeConfiguration.DescriminatorPropertyName, typeof(string));

      builder.Property(entity => entity.UserId).ToJsonProperty("userId").HasValueGenerator<GuidValueGenerator>();
      builder.Property(entity => entity.Name).ToJsonProperty("name");
      builder.Property(entity => entity.Email).ToJsonProperty("email");
      builder.Property(entity => entity.PasswordHash).ToJsonProperty("passwordHash");

      builder.Ignore(entity => entity.Scopes);
    }
  }
}
