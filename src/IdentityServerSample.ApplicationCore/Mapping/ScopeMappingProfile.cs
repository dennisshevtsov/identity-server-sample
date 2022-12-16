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
    }

    private static void ConfigureGetScopesMapping(IProfileExpression expression)
    {
      expression.CreateMap<IEnumerable<ScopeEntity>, GetScopesResponseDto>();
    }
  }
}
