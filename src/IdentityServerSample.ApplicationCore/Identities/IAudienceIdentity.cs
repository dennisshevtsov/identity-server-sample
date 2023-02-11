// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Identities
{
  /// <summary>Represents an identity of an audience.</summary>
  public interface IAudienceIdentity
  {
    /// <summary>Gets/sets an object that represents an ID of an audience.</summary>
    public string? Name { get; set; }
  }
}
