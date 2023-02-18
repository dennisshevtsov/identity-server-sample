// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Mapping
{
  using AutoMapper;

  using IdentityServerSample.ApplicationCore.Dtos;
  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Provides a named configuration for maps.</summary>
  public sealed class ClientMappingProfile : Profile
  {
    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.ApplicationCore.Mapping.ClientMappingProfile"/> class.</summary>
    public ClientMappingProfile()
    {
      ClientMappingProfile.ConfigureGetClientsMapping(this);
    }

    private static void ConfigureGetClientsMapping(IProfileExpression expression)
    {
      expression.CreateMap<IEnumerable<ClientEntity>, GetClientsResponseDto>()
                .ForMember(dto => dto.Clients, options => options.MapFrom(entities => entities));
      expression.CreateMap<ClientEntity, GetClientsResponseDto.ClientDto>();
    }
  }
}
