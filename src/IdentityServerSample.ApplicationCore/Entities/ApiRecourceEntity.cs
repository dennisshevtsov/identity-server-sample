// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Entities
{
  /// <summary>Represents details of an API resource.</summary>
  public sealed class ApiRecourceEntity
  {
    /// <summary>Gets/sets an object that represents a name of an API resource.</summary>
    public string? Name { get; set; }

    /// <summary>Gets/sets an object that represents a display name of an API resource.</summary>
    public string? DisplayName { get; set; }

    /// <summary>Gets/sets an object that represents a description of an API resource.</summary>
    public string? Description { get; set; }
  }
}
