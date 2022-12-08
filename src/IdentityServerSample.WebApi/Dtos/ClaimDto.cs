// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Dtos
{
  /// <summary>Represents details of a claim.</summary>
  public sealed class ClaimDto
  {
    /// <summary>Gets/sets an object that represents a type of a claim.</summary>
    public string? Type { get; set; }

    /// <summary>Gets/sets an object that represents a value of a claim.</summary>
    public string? Value { get; set; }
  }
}
