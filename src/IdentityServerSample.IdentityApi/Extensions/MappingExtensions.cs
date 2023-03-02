﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  /// <summary>Provides methods to extend the API of the <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.</summary>
  public static class MappingExtensions
  {
    /// <summary>Registers the mapping profiles in the DI container.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection SetUpMapping(this IServiceCollection services)
    {
      services.SetUpIdentityServerMapping();
      services.AddAutoMapper(_ => { });

      return services;
    }
  }
}
