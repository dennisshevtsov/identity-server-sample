// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.Mapping
{
  using AutoMapper;
  using IdentityServer4;
  using IdentityServer4.Models;

  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Provides a named configuration for maps.</summary>
  public sealed class ScopeMappingProfile : Profile
  {
    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.IdentityApi.Mapping.ScopeMappingProfile"/> class.</summary>
    public ScopeMappingProfile()
    {
      ScopeMappingProfile.ConfigureMapping(this);
    }

    private static void ConfigureMapping(IProfileExpression expression)
    {
      expression.CreateMap<ScopeEntity, ApiScope>()
                .ForMember(resource => resource.Name, options => options.MapFrom(entity => entity.ScopeName));

      expression.CreateMap<ScopeEntity, IdentityResource>()
                .ConstructUsing(ScopeMappingProfile.ConstructIdentityResource);
    }

    private static IdentityResource ConstructIdentityResource(ScopeEntity scopeEntity, ResolutionContext context)
      => scopeEntity.ScopeName switch
      {
        IdentityServerConstants.StandardScopes.OpenId => new IdentityResources.OpenId(),
        IdentityServerConstants.StandardScopes.Profile => new IdentityResources.Profile(),
        IdentityServerConstants.StandardScopes.Email => new IdentityResources.Email(),
        IdentityServerConstants.StandardScopes.Phone => new IdentityResources.Phone(),
        IdentityServerConstants.StandardScopes.Address => new IdentityResources.Address(),
        _ => throw new InvalidOperationException(),
      };
  }
}
