// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApp.ViewModels
{
  /// <summary>Represents details of an error.</summary>
  public sealed class ErrorViewModel
  {
    /// <summary>Gets/sets an object that represents an ID of an error.</summary>
    public string? ErrorId { get; set; }

    /// <summary>Gets/sets an object that represents a message of an error.</summary>
    public string? Message { get; set; }
  }
}
