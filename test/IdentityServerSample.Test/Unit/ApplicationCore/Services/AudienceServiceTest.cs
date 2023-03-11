// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using IdentityServerSample.ApplicationCore.Identities;

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

      _audienceScopeServiceMock.Verify(service => service.GetAudienceScopesAsync(_cancellationToken));
      _audienceScopeServiceMock.VerifyNoOtherCalls();

      _mapperMock.Verify(mapper => mapper.Map<GetAudiencesResponseDto>(audienceEntityCollection));
      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetAudiencesAsync_Should_Return_All_Audiences()
    {
      var controlAudienceEntityCollection = new List<AudienceEntity>();

      _audienceRepositoryMock.Setup(repository => repository.GetAudiencesAsync(It.IsAny<CancellationToken>()))
                             .ReturnsAsync(controlAudienceEntityCollection)
                             .Verifiable();

      var audienceScopeDictionary = new Dictionary<string, List<string>>();

      _audienceScopeServiceMock.Setup(service => service.GetAudienceScopesAsync(It.IsAny<CancellationToken>()))
                               .ReturnsAsync(audienceScopeDictionary)
                               .Verifiable();

      var actualAudienceEntityCollection =
        await _audienceService.GetAudiencesAsync(_cancellationToken);

      Assert.IsNotNull(actualAudienceEntityCollection);
      Assert.AreEqual(controlAudienceEntityCollection, actualAudienceEntityCollection);

      _audienceRepositoryMock.Verify(repository => repository.GetAudiencesAsync(_cancellationToken));
      _audienceRepositoryMock.VerifyNoOtherCalls();

      _audienceScopeServiceMock.Verify(service => service.GetAudienceScopesAsync(_cancellationToken));
      _audienceScopeServiceMock.VerifyNoOtherCalls();

      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetAudiencesByScopesAsync_Should_Return_Audiences_For_Scopes()
    {
      var controlAudienceEntityCollection = new List<AudienceEntity>();

      _audienceRepositoryMock.Setup(repository => repository.GetAudiencesAsync(It.IsAny<IEnumerable<IAudienceIdentity>>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(controlAudienceEntityCollection)
                             .Verifiable();

      var audienceName = Guid.NewGuid().ToString();

      var audienceScopeDictionary = new Dictionary<string, List<string>>
      {
        { audienceName, new List<string>() },
      };

      _audienceScopeServiceMock.Setup(service => service.GetAudienceScopesAsync(It.IsAny<IEnumerable<IScopeIdentity>>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(audienceScopeDictionary)
                               .Verifiable();

      var scopeNameCollection = new List<string>
      {
        Guid.NewGuid().ToString(),
      };

      var actualAudienceEntityCollection =
        await _audienceService.GetAudiencesByScopesAsync(
          scopeNameCollection, _cancellationToken);

      Assert.IsNotNull(actualAudienceEntityCollection);
      Assert.AreEqual(controlAudienceEntityCollection, actualAudienceEntityCollection);

      _audienceRepositoryMock.Verify(repository => repository.GetAudiencesAsync(new[] { audienceName.ToAudienceIdentity() }, _cancellationToken));
      _audienceRepositoryMock.VerifyNoOtherCalls();

      _audienceScopeServiceMock.Verify(service => service.GetAudienceScopesAsync(scopeNameCollection.ToScopeIdentities(), _cancellationToken));
      _audienceScopeServiceMock.VerifyNoOtherCalls();

      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetAudiencesByScopesAsync_Should_Return_Audiences()
    {
      var audienceName = Guid.NewGuid().ToString();

      var controlAudienceEntityCollection = new List<AudienceEntity>
      {
        new AudienceEntity
        {
          AudienceName = audienceName,
        },
      };

      _audienceRepositoryMock.Setup(repository => repository.GetAudiencesAsync(It.IsAny<IEnumerable<IAudienceIdentity>>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(controlAudienceEntityCollection)
                             .Verifiable();

      var audienceScopeDictionary = new Dictionary<string, List<string>>();

      _audienceScopeServiceMock.Setup(service => service.GetAudienceScopesAsync(It.IsAny<IEnumerable<IAudienceIdentity>>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(audienceScopeDictionary)
                               .Verifiable();

      var audienceNameCollection = new List<string>
      {
        audienceName,
      };

      var actualAudienceEntityCollection =
        await _audienceService.GetAudiencesByNamesAsync(
          audienceNameCollection, _cancellationToken);

      Assert.IsNotNull(actualAudienceEntityCollection);
      Assert.AreEqual(controlAudienceEntityCollection, actualAudienceEntityCollection);

      var audienceIdentityCollection = audienceNameCollection.ToAudienceIdentities();

      _audienceRepositoryMock.Verify(repository => repository.GetAudiencesAsync(audienceIdentityCollection, _cancellationToken));
      _audienceRepositoryMock.VerifyNoOtherCalls();

      _audienceScopeServiceMock.Verify(service => service.GetAudienceScopesAsync(controlAudienceEntityCollection, _cancellationToken));
      _audienceScopeServiceMock.VerifyNoOtherCalls();

      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetAudienceAsync_Should_Return_Audiences()
    {
      var audienceName = Guid.NewGuid().ToString();

      var controlAudienceEntity = new AudienceEntity
      {
        AudienceName = audienceName,
      };

      _audienceRepositoryMock.Setup(repository => repository.GetAudienceAsync(It.IsAny<IAudienceIdentity>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(controlAudienceEntity)
                             .Verifiable();

      var identity = Guid.NewGuid().ToString().ToAudienceIdentity();

      var actualAudienceEntity =
        await _audienceService.GetAudienceAsync(
          identity, _cancellationToken);

      Assert.IsNotNull(actualAudienceEntity);
      Assert.AreEqual(controlAudienceEntity, actualAudienceEntity);

      _audienceRepositoryMock.Verify(repository => repository.GetAudienceAsync(identity, _cancellationToken));
      _audienceRepositoryMock.VerifyNoOtherCalls();

      _audienceScopeServiceMock.VerifyNoOtherCalls();

      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddAudienceAsync_Should_Add_Audience()
    {
      _audienceRepositoryMock.Setup(repository => repository.AddAudienceAsync(It.IsAny<AudienceEntity>(), It.IsAny<CancellationToken>()))
                             .Returns(Task.CompletedTask)
                             .Verifiable();

      var controlAudienceEntity = new AudienceEntity();

      _mapperMock.Setup(mapper => mapper.Map<AudienceEntity>(It.IsAny<AddAudienceRequestDto>()))
                 .Returns(controlAudienceEntity)
                 .Verifiable();

      var controlAddAudienceRequestDto = new AddAudienceRequestDto();

      await _audienceService.AddAudienceAsync(
          controlAddAudienceRequestDto, _cancellationToken);

      _audienceRepositoryMock.Verify(repository => repository.AddAudienceAsync(controlAudienceEntity, _cancellationToken));
      _audienceRepositoryMock.VerifyNoOtherCalls();

      _audienceScopeServiceMock.VerifyNoOtherCalls();

      _mapperMock.Verify(mapper => mapper.Map<AudienceEntity>(controlAddAudienceRequestDto));
      _mapperMock.VerifyNoOtherCalls();
    }
  }
}
