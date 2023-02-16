﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure
{
  /// <summary>Represents settings of a database.</summary>
  public sealed class DatabaseOptions
  {
    /// <summary>Gets/sets an object that represents an account endpoint of a database.</summary>
    public string? AccountEndpoint { get; set; }

    /// <summary>Gets/sets an object that represents an account key of a database.</summary>
    public string? AccountKey { get; set; }

    /// <summary>Gets/sets an object that represents a name of a database.</summary>
    public string? DatabaseName { get; set; }

    /// <summary>Gets/sets an object that represents a name of an account container.</summary>
    public string UserContainerName { get; set; } = "users";

    /// <summary>Gets/sets an object that represents a name of an audience container.</summary>
    public string AudienceContainerName { get; set; } = "audiences";

    /// <summary>Gets/sets an object that represents a name of an audience container.</summary>
    public string ClientContainerName { get; set; } = "clients";

    /// <summary>Gets/sets an object that represents a name of an audience container.</summary>
    public string ScopeContainerName { get; set; } = "scopes";
  }
}
