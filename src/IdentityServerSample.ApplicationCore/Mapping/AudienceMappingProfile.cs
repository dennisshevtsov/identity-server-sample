// Copyright (c) Dennis Shevtsov. All rights reserved.
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
      AudienceMappingProfile.ConfigureGetAudienceMapping(this);
    }

    private static void ConfigureGetAudiencesMapping(IProfileExpression expression)
    {
      expression.CreateMap<IEnumerable<AudienceEntity>, GetAudiencesResponseDto>()
                .ForMember(dto => dto.Audiences, options => options.MapFrom(entity => entity));
      expression.CreateMap<AudienceEntity, GetAudiencesResponseDto.AudienceDto>();
    }

    private static void ConfigureGetAudienceMapping(IProfileExpression expression)
    {
      expression.CreateMap<AudienceEntity, GetAudienceResponseDto>();
    }
  }
}
