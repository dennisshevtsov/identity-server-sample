// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Mapping
{
  using AutoMapper;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Provides a named configuration for maps.</summary>
  public sealed class ScopeMappingProfile : Profile
  {
    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.ApplicationCore.Mapping.ScopeMappingProfile"/> class.</summary>
    public ScopeMappingProfile()
    {
      ScopeMappingProfile.ConfigureGetScopesMapping(this);
      ScopeMappingProfile.ConfigureGetScopeMapping(this);
      ScopeMappingProfile.ConfigureAddScopeMapping(this);
    }

    private static void ConfigureGetScopesMapping(IProfileExpression expression)
    {
      expression.CreateMap<IEnumerable<ScopeEntity>, GetScopesResponseDto>()
                .ForMember(dto => dto.Scopes, opt => opt.MapFrom(entities => entities));
      expression.CreateMap<ScopeEntity, GetScopesResponseDto.ScopeDto>();
    }

    private static void ConfigureGetScopeMapping(IProfileExpression expression)
    {
      expression.CreateMap<ScopeEntity, GetScopeResponseDto>();
    }

    private static void ConfigureAddScopeMapping(IProfileExpression expression)
    {
      expression.CreateMap<AddScopeRequestDto, ScopeEntity>();
    }
  }
}
