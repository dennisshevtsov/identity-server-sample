// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.Mapping
{
  using AutoMapper;
  using IdentityServer4.Models;

  using IdentityServerSample.ApplicationCore.Entities;

  /// <summary>Provides a named configuration for maps.</summary>
  public sealed class ClientMappingProfile : Profile
  {
    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.IdentityApi.Mapping.ClientMappingProfile"/> class.</summary>
    public ClientMappingProfile()
    {
      ClientMappingProfile.ConfigureMapping(this);
    }

    private static void ConfigureMapping(IProfileExpression expression)
    {
      expression.CreateMap<ClientEntity, Client>()
                .ForMember(model => model.ClientId, options => options.MapFrom(entity => entity.Name))
                .ForMember(model => model.ClientName, options => options.MapFrom(entity => entity.DisplayName))
                .ForMember(model => model.RequireClientSecret, options => options.MapFrom(entity => true))
                .ForMember(model => model.AllowedGrantTypes, options => options.MapFrom(entity => GrantTypes.Code))
                .ForMember(model => model.AllowedScopes, options => options.MapFrom(entity => entity.Scopes))
                .ForMember(model => model.RedirectUris, options => options.MapFrom(entity => entity.RedirectUris))
                .ForMember(model => model.PostLogoutRedirectUris, options => options.MapFrom(entity => entity.PostRedirectUris));
    }
  }
}
