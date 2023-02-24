// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Provides a simple API to execute audience queries and commands.</summary>
  public interface IAudienceScopeService
  {
    /// <summary>Gets a dictionary that contains collections of scope names per an audience name.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<Dictionary<string, List<string>>> GetAudienceScopesAsync(CancellationToken cancellationToken);

    /// <summary>Gets a dictionary that contains collections of scope names per an audience name.</summary>
    /// <param name="identities">An object that represents a collection of the <see cref="IdentityServerSample.ApplicationCore.Identities.IScopeIdentity"/>.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<Dictionary<string, List<string>>> GetAudienceScopesAsync(
      IEnumerable<IScopeIdentity> identities, CancellationToken cancellationToken);

    /// <summary>Gets a dictionary that contains collections of scope names per an audience name.</summary>
    /// <param name="identities">An object that represents a collection of the <see cref="IdentityServerSample.ApplicationCore.Identities.IAudienceIdentity"/>.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<Dictionary<string, List<string>>> GetAudienceScopesAsync(
      IEnumerable<IAudienceIdentity> identities, CancellationToken cancellationToken);
  }
}
