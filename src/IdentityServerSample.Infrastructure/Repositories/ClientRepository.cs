﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Infrastructure.Repositories
{
  using System;

  using Microsoft.EntityFrameworkCore;

  using IdentityServerSample.ApplicationCore.Entities;
  using IdentityServerSample.ApplicationCore.Repositories;
  using IdentityServerSample.ApplicationCore.Identities;

  /// <summary>Provides a simple API to query and save instances of the <see cref="IdentityServerSample.ApplicationCore.Entities.ClientEntity"/> class.</summary>
  public sealed class ClientRepository : IClientRepository
  {
    private readonly DbContext _dbContext;

    /// <summary>Initializes a new instance of the <see cref="IdentityServerSample.Infrastructure.Repositories.ClientRepository"/> class.</summary>
    /// <param name="dbContext">An object that represents a session with the database and can be used to query and save instances of your entities.</param>
    public ClientRepository(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>Adds a new client.</summary>
    /// <param name="clientEntity">An object that represents details of a client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation.</returns>
    public async Task AddClientAsync(ClientEntity clientEntity, CancellationToken cancellationToken)
    {
      var clientEntityEntry = _dbContext.Add(clientEntity);

      await _dbContext.SaveChangesAsync(cancellationToken);

      clientEntityEntry.State = EntityState.Detached;
    }

    /// <summary>Gets a client by its name.</summary>
    /// <param name="identity">An object that represents an identity of a client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<ClientEntity?> GetClientAsync(IClientIdentity identity, CancellationToken cancellationToken)
      => _dbContext.Set<ClientEntity>()
                   .AsNoTracking()
                   .Where(entity => entity.ClientName == identity.ClientName)
                   .FirstOrDefaultAsync(cancellationToken);

    /// <summary>Gets a client by its name.</summary>
    /// <param name="name">An object that represents a name of a client.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<ClientEntity?> GetClientAsync(string name, CancellationToken cancellationToken)
    {
      return _dbContext.Set<ClientEntity>()
                       .AsNoTracking()
                       .Where(entity => entity.ClientName == name)
                       .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>Gets a collection of active clients.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public Task<ClientEntity[]> GetClientsAsync(CancellationToken cancellationToken)
    {
      return _dbContext.Set<ClientEntity>()
                       .AsNoTracking()
                       .OrderBy(entity => entity.ClientName)
                       .ToArrayAsync(cancellationToken);
    }

    /// <summary>Gets a first client with a defined origin.</summary>
    /// <param name="origin">An object that represents an origin.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that tepresents an asynchronous operation that produces a result at some time in the future.</returns>
    public async Task<ClientEntity?> GetFirstClientWithOriginAsync(string origin, CancellationToken cancellationToken)
    {
      var clientEntity =
        await _dbContext.Set<ClientEntity>()
                        .FromSqlRaw("SELECT * FROM c WHERE IS_DEFINED(c.corsOrigins) AND ARRAY_CONTAINS(c.corsOrigins, {0})", origin)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(cancellationToken);

      return clientEntity;
    }
  }
}
