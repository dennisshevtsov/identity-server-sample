// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApp.Controllers.Test
{
  using Microsoft.AspNetCore.Mvc;

  using IdentityServerSample.ApplicationCore.Identities;

  [TestClass]
  public sealed class ScopeControllerTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IMapper> _mapperMock;
    private Mock<IScopeService> _scopeServiceMock;

    private ScopeController _scopeController;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _mapperMock = new Mock<IMapper>();
      _scopeServiceMock = new Mock<IScopeService>();

      _scopeController = new ScopeController(
        _scopeServiceMock.Object, _mapperMock.Object);
    }

    [TestMethod]
    public async Task GetScopes_Should_Return_Collection_Of_Scopes()
    {
      var scopeEntityCollection = new List<ScopeEntity>();

      _scopeServiceMock.Setup(service => service.GetScopesAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(scopeEntityCollection)
                       .Verifiable();

      var getScopesResponseDto = new GetScopesResponseDto();

      _mapperMock.Setup(mapper => mapper.Map<GetScopesResponseDto>(It.IsAny<List<ScopeEntity>>()))
                 .Returns(getScopesResponseDto);

      var getScopesRequestDto = new GetScopesRequestDto();
      var actionResult = await _scopeController.GetScopes(getScopesRequestDto, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var okObjectResult = actionResult as OkObjectResult;

      Assert.IsNotNull(okObjectResult);
      Assert.AreEqual(getScopesResponseDto, okObjectResult!.Value);

      _scopeServiceMock.Verify(service => service.GetScopesAsync(_cancellationToken), Times.Once());
      _scopeServiceMock.VerifyNoOtherCalls();

      _mapperMock.Verify(mapper => mapper.Map<GetScopesResponseDto>(scopeEntityCollection), Times.Once());
      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetScope_Should_Return_Scope()
    {
      var controlScopeEntity = new ScopeEntity();

      _scopeServiceMock.Setup(service => service.GetScopeAsync(It.IsAny<IScopeIdentity>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(controlScopeEntity)
                       .Verifiable();

      var getScopeResponseDto = new GetScopeResponseDto();

      _mapperMock.Setup(mapper => mapper.Map<GetScopeResponseDto>(It.IsAny<ScopeEntity>()))
                 .Returns(getScopeResponseDto);

      var getScopeRequestDto = new GetScopeRequestDto();
      var actionResult = await _scopeController.GetScope(getScopeRequestDto, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var okObjectResult = actionResult as OkObjectResult;

      Assert.IsNotNull(okObjectResult);
      Assert.AreEqual(getScopeResponseDto, okObjectResult!.Value);

      _scopeServiceMock.Verify(service => service.GetScopeAsync(getScopeRequestDto, _cancellationToken), Times.Once());
      _scopeServiceMock.VerifyNoOtherCalls();

      _mapperMock.Verify(mapper => mapper.Map<GetScopeResponseDto>(controlScopeEntity), Times.Once());
      _mapperMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddScope_Should_Return_Scope()
    {
      _scopeServiceMock.Setup(service => service.AddScopeAsync(It.IsAny<AddScopeRequestDto>(), It.IsAny<CancellationToken>()))
                       .Returns(Task.CompletedTask)
                       .Verifiable();

      var scopeName = Guid.NewGuid().ToString();
      var addScopeRequestDto = new AddScopeRequestDto
      {
        ScopeName = scopeName,
      };
      var actionResult = await _scopeController.AddScope(addScopeRequestDto, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var createdResult = actionResult as CreatedAtActionResult;

      Assert.IsNotNull(createdResult);
      Assert.AreEqual(nameof(ScopeController.GetScope), createdResult.ActionName);
      Assert.IsNotNull(createdResult.Value);

      var getScopeRequestDto = createdResult.Value as GetScopeRequestDto;

      Assert.IsNotNull(getScopeRequestDto);
      Assert.AreEqual(scopeName, getScopeRequestDto.ScopeName);

      _scopeServiceMock.Verify(service => service.AddScopeAsync(addScopeRequestDto, _cancellationToken), Times.Once());
      _scopeServiceMock.VerifyNoOtherCalls();

      _mapperMock.VerifyNoOtherCalls();
    }
  }
}
