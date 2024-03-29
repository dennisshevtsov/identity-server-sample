﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Dtos
{
  /// <summary>Represents conditions to query error details.</summary>
  public sealed class GetErrorRequestDto
  {
    /// <summary>Gets/sets an object that represents an ID of an error.</summary>
    public string? ErrorId { get; set; }
  }
}
