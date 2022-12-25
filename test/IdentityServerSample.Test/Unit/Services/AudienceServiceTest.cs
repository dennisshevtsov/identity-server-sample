// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Test.Unit.Services
{
  using AutoMapper;
  using Moq;

  using IdentityServerSample.ApplicationCore.Repositories;
  using IdentityServerSample.ApplicationCore.Services;
  
  [TestClass]
  public sealed class AudienceServiceTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private IMock<IMapper> _mapperMock;
    private IMock<IAudienceRepository> _audienceRepositoryMock;

    private AudienceService _audienceService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _mapperMock = new Mock<IMapper>();
      _audienceRepositoryMock= new Mock<IAudienceRepository>();

      _audienceService = new AudienceService(
        _mapperMock.Object, _audienceRepositoryMock.Object);
    }
  }
}
