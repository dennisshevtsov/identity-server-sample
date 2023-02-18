// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Services.Test
{
  [TestClass]
  public sealed class AudienceServiceTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IMapper> _mapperMock;
    private Mock<IAudienceRepository> _audienceRepositoryMock;

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

    [TestMethod]
    public async Task GetAudiencesAsync_Should_Return_Mapped_Dtos()
    {
      var audienceEntityCollection = new AudienceEntity[0];

      _audienceRepositoryMock.Setup(repository => repository.GetAudiencesAsync(It.IsAny<CancellationToken>()))
                             .ReturnsAsync(audienceEntityCollection)
                             .Verifiable();

      var getAudiencesResponseDto = new GetAudiencesResponseDto();

      _mapperMock.Setup(mapper => mapper.Map<GetAudiencesResponseDto>(It.IsAny<AudienceEntity[]>()))
                 .Returns(getAudiencesResponseDto);

      var getAudiencesRequestDto = new GetAudiencesRequestDto();

      var actualGetAudienceResponseDto = await _audienceService.GetAudiencesAsync(
        getAudiencesRequestDto, _cancellationToken);

      Assert.IsNotNull(actualGetAudienceResponseDto);
      Assert.AreEqual(getAudiencesResponseDto, actualGetAudienceResponseDto);

      _audienceRepositoryMock.Verify(repository => repository.GetAudiencesAsync(_cancellationToken));
      _audienceRepositoryMock.VerifyNoOtherCalls();

      _mapperMock.Verify(mapper => mapper.Map<GetAudiencesResponseDto>(audienceEntityCollection));
      _mapperMock.VerifyNoOtherCalls();
    }
  }
}
