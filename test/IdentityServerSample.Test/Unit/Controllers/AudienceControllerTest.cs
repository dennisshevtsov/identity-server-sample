// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Test.Unit.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.WebApp.Controllers;

  [TestClass]
  public sealed class AudienceControllerTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IAudienceService> _audienceServiceMock;

    private AudienceController _audienceController;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _audienceServiceMock = new Mock<IAudienceService>();

      _audienceController = new AudienceController(_audienceServiceMock.Object);
    }

    [TestMethod]
    public async Task GetAudiences_Should_Collection_Of_Audiences()
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
  }
}
