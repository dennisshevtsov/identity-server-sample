// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.Mapping
{
  using AutoMapper;
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
    }
  }
}
