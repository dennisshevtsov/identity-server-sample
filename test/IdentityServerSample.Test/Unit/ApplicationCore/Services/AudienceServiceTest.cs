// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Threading;

namespace IdentityServerSample.ApplicationCore.Services.Test
{
  [TestClass]
  public sealed class AudienceServiceTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IMapper> _mapperMock;

    private Mock<IAudienceRepository> _audienceRepositoryMock;

    private Mock<IAudienceScopeService> _audienceScopeServiceMock;

    private AudienceService _audienceService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _mapperMock = new Mock<IMapper>();

      _audienceRepositoryMock = new Mock<IAudienceRepository>();
      _audienceScopeServiceMock = new Mock<IAudienceScopeService>();

      _audienceService = new AudienceService(
        _mapperMock.Object,
        _audienceRepositoryMock.Object,
        _audienceScopeServiceMock.Object);
    }

    [TestMethod]
    public async Task GetAudiencesAsync_Should_Return_Mapped_Dtos()
    {
      var audienceEntityCollection = new List<AudienceEntity>();

      _audienceRepositoryMock.Setup(repository => repository.GetAudiencesAsync(It.IsAny<CancellationToken>()))
                             .ReturnsAsync(audienceEntityCollection)
                             .Verifiable();

      var getAudiencesResponseDto = new GetAudiencesResponseDto();

      var audienceScopeDictionary = new Dictionary<string, List<string>>();

      _audienceScopeServiceMock.Setup(service => service.GetAudienceScopesAsync(It.IsAny<CancellationToken>()))
                               .ReturnsAsync(audienceScopeDictionary)
                               .Verifiable();

      _mapperMock.Setup(mapper => mapper.Map<GetAudiencesResponseDto>(It.IsAny<List<AudienceEntity>>()))
                 .Returns(getAudiencesResponseDto)
                 .Verifiable();

      var getAudiencesRequestDto = new GetAudiencesRequestDto();

      var actualGetAudienceResponseDto = await _audienceService.GetAudiencesAsync(
        getAudiencesRequestDto, _cancellationToken);

      Assert.IsNotNull(actualGetAudienceResponseDto);
      Assert.AreEqual(getAudiencesResponseDto, actualGetAudienceResponseDto);

      _audienceRepositoryMock.Verify(repository => repository.GetAudiencesAsync(_cancellationToken));
      _audienceRepositoryMock.VerifyNoOtherCalls();

      _audienceScopeServiceMock.Verify(service => service.GetAudienceScopesAsync(It.IsAny<CancellationToken>()));
      _audienceScopeServiceMock.VerifyNoOtherCalls();

      _mapperMock.Verify(mapper => mapper.Map<GetAudiencesResponseDto>(audienceEntityCollection));
      _mapperMock.VerifyNoOtherCalls();
    }
  }
}
