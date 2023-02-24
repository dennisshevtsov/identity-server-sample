// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services
{
  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Identities;
  using IdentityServerSample.ApplicationCore.Repositories;

  /// <summary>Provides a simple API to execute audience queries and commands.</summary>
  public sealed class AudienceScopeService : IAudienceScopeService
  {
    private readonly IAudienceScopeRepository _audienceScopeRepository;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.ApplicationCore.Services.AudienceScopeService"/> class.</summary>
    /// <param name="audienceScopeRepository">An object that provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.AudienceScopeEntity"/> class.</param>
    public AudienceScopeService(IAudienceScopeRepository audienceScopeRepository)
    {
      _audienceScopeRepository = audienceScopeRepository ??
        throw new ArgumentNullException(nameof(audienceScopeRepository));
    }

    /// <summary>Gets a dictionary that contains collections of scope names per an audience name.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<Dictionary<string, List<string>>> GetAudienceScopesAsync(CancellationToken cancellationToken)
    {
      var audienceScopeEntityCollection =
        await _audienceScopeRepository.GetAudienceScopesAsync(cancellationToken);

      var audienceScopeDictionary =
        AudienceScopeService.GetAudienceScopeDictionary(
          audienceScopeEntityCollection);

      return audienceScopeDictionary;
    }

    /// <summary>Gets a dictionary that contains collections of scope names per an audience name.</summary>
    /// <param name="identities">An object that represents a collection of the <see cref="IdentityServerSample.ApplicationCore.Identities.IScopeIdentity"/>.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<Dictionary<string, List<string>>> GetAudienceScopesAsync(
      IEnumerable<IScopeIdentity> identities, CancellationToken cancellationToken)
    {
      var audienceScopeEntityCollection =
        await _audienceScopeRepository.GetAudienceScopesAsync(
          identities, cancellationToken);

      var audienceScopeDictionary =
        AudienceScopeService.GetAudienceScopeDictionary(
          audienceScopeEntityCollection);

      return audienceScopeDictionary;
    }

    /// <summary>Gets a dictionary that contains collections of scope names per an audience name.</summary>
    /// <param name="identities">An object that represents a collection of the <see cref="IdentityServerSample.ApplicationCore.Identities.IAudienceIdentity"/>.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<Dictionary<string, List<string>>> GetAudienceScopesAsync(
      IEnumerable<IAudienceIdentity> identities, CancellationToken cancellationToken)
    {
      var audienceScopeEntityCollection =
        await _audienceScopeRepository.GetAudienceScopesAsync(
          identities, cancellationToken);

      var audienceScopeDictionary =
        AudienceScopeService.GetAudienceScopeDictionary(
          audienceScopeEntityCollection);

      return audienceScopeDictionary;
    }

    private static void AddAudienceScope(
      Dictionary<string, List<string>> audienceScopeDictionary,
      AudienceScopeEntity audienceScopeEntity)
    {
      if (!audienceScopeDictionary.TryGetValue(
        audienceScopeEntity.AudienceName!, out var audienceScopeEntityCollection))
      {
        audienceScopeEntityCollection = new List<string>();

        audienceScopeDictionary.Add(
          audienceScopeEntity.AudienceName!,
          audienceScopeEntityCollection);
      }

      audienceScopeEntityCollection.Add(audienceScopeEntity.ScopeName!);
    }

    private static Dictionary<string, List<string>> GetAudienceScopeDictionary(
      List<AudienceScopeEntity> audienceScopeEntityCollection)
    {
      var audienceScopeDictionary = new Dictionary<string, List<string>>();

      for (int i = 0; i < audienceScopeEntityCollection.Count; i++)
      {
        AudienceScopeService.AddAudienceScope(
          audienceScopeDictionary,
          audienceScopeEntityCollection[i]);
      }

      return audienceScopeDictionary;
    }
  }
}
