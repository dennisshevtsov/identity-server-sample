// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Controllers.Test
{
  using IdentityServerSample.ApplicationCore.Identities;
  using Microsoft.AspNetCore.Mvc;

  [TestClass]
  public sealed class AudienceControllerTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IAudienceService> _audienceServiceMock;
    private Mock<IMapper> _mapperMock;

    private AudienceController _audienceController;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _audienceServiceMock = new Mock<IAudienceService>();
      _mapperMock = new Mock<IMapper>();

      _audienceController = new AudienceController(
        _audienceServiceMock.Object, _mapperMock.Object);
    }

    [TestMethod]
    public async Task GetAudiences_Should_Return_Collection_Of_Audiences()
    {
      var getAudiencesResponseDto = new GetAudiencesResponseDto();

      _audienceServiceMock.Setup(service => service.GetAudiencesAsync(It.IsAny<GetAudiencesRequestDto>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(getAudiencesResponseDto)
                          .Verifiable();

      var getAudiencesRequestDto = new GetAudiencesRequestDto();
      var actionResult = await _audienceController.GetAudiences(getAudiencesRequestDto, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var okObjectResult = actionResult as OkObjectResult;

      Assert.IsNotNull(okObjectResult);
      Assert.AreEqual(getAudiencesResponseDto, okObjectResult!.Value);

      _audienceServiceMock.Verify(service => service.GetAudiencesAsync(getAudiencesRequestDto, _cancellationToken), Times.Once());
      _audienceServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetAudience_Should_Return_Audience()
    {
      var controlAudienceEntity = new AudienceEntity();

      _audienceServiceMock.Setup(service => service.GetAudienceAsync(It.IsAny<IAudienceIdentity>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(controlAudienceEntity)
                          .Verifiable();

      var controlGetAudienceResponseDto = new GetAudienceResponseDto();

      _mapperMock.Setup(mapper => mapper.Map<GetAudienceResponseDto>(It.IsAny<AudienceEntity>()))
                 .Returns(controlGetAudienceResponseDto);

      var getAudienceRequestDto = new GetAudienceRequestDto();
      var actionResult = await _audienceController.GetAudience(getAudienceRequestDto, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var okObjectResult = actionResult as OkObjectResult;

      Assert.IsNotNull(okObjectResult);
      Assert.AreEqual(controlGetAudienceResponseDto, okObjectResult!.Value);

      _audienceServiceMock.Verify(service => service.GetAudienceAsync(getAudienceRequestDto, _cancellationToken), Times.Once());
      _audienceServiceMock.VerifyNoOtherCalls();

      _mapperMock.Verify(mapper => mapper.Map<GetAudienceResponseDto>(controlAudienceEntity), Times.Once());
      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddAudience_Should_Return_Audience()
    {
      _audienceServiceMock.Setup(service => service.AddAudienceAsync(It.IsAny<AddAudienceRequestDto>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.CompletedTask)
                        .Verifiable();

      var audienceName = Guid.NewGuid().ToString();
      var addAudienceRequestDto = new AddAudienceRequestDto
      {
        AudienceName = audienceName,
      };
      var actionResult = await _audienceController.AddAudience(addAudienceRequestDto, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var createdResult = actionResult as CreatedAtRouteResult;

      Assert.IsNotNull(createdResult);
      Assert.AreEqual(nameof(AudienceController.GetAudience), createdResult.RouteName);
      Assert.IsNotNull(createdResult.RouteValues);

      Assert.IsTrue(createdResult.RouteValues.ContainsKey(nameof(GetAudienceRequestDto.AudienceName)));
      Assert.AreEqual(audienceName, createdResult.RouteValues[nameof(GetAudienceRequestDto.AudienceName)]);

      _audienceServiceMock.Verify(service => service.AddAudienceAsync(addAudienceRequestDto, _cancellationToken), Times.Once());
      _audienceServiceMock.VerifyNoOtherCalls();

      _mapperMock.VerifyNoOtherCalls();
    }
  }
}
