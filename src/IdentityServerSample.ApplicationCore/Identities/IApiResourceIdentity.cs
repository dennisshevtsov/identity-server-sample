﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Identities
{
  /// <summary>Represents an identity of an API resource.</summary>
  public interface IApiResourceIdentity
  {
    /// <summary>Gets/sets an object that reprsents a name of an API resource.</summary>
    public string Name? { get; set; }
  }
}
