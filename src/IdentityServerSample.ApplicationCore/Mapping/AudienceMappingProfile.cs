﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Mapping
{
  using AutoMapper;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Provides a named configuration for maps.</summary>
  public sealed class AudienceMappingProfile : Profile
  {
    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.ApplicationCore.Mapping.AudienceMappingProfile"/> class.</summary>
    public AudienceMappingProfile()
    {
      AudienceMappingProfile.ConfigureGetAudiencesMapping(this);
    }

    private static void ConfigureGetAudiencesMapping(IProfileExpression expression)
    {
      expression.CreateMap<AudienceEntity[], GetAudiencesResponseDto>()
                .ForMember(dst => dst.Audiences, opt => opt.MapFrom(src => src));
      expression.CreateMap<AudienceEntity, GetAudiencesResponseDto.AudienceDto>();
    }
  }
}
