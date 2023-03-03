// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.Initialization.Test
{
  using System.Threading;

  using Microsoft.AspNetCore.Identity;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using IdentityServerSample.ApplicationCore.Defaults;
  using IdentityServerSample.ApplicationCore.Identities;
  using IdentityServerSample.ApplicationCore.Entities;

  [TestClass]
  public sealed class DatabaseInitializerTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IConfiguration> _configurationMock;

    private Mock<UserManager<UserEntity>> _userManagerMock;

    private Mock<IClientService> _clientServiceMock;
    private Mock<IScopeService> _scopeServiceMock;

    private DatabaseInitializer _databaseInitializer;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _configurationMock = new Mock<IConfiguration>();

      var userStoreMock = new Mock<IUserStore<UserEntity>>();
      var passwordHasherMock = new Mock<IPasswordHasher<UserEntity>>();
      var userValidatorMock = new Mock<IUserValidator<UserEntity>>();
      var passwordValidatorMock = new Mock<IPasswordValidator<UserEntity>>();
      var keyNormalizerMock = new Mock<ILookupNormalizer>();
      var errorsMock = new Mock<IdentityErrorDescriber>();
      var servicesMock = new Mock<IServiceProvider>();
      var optionsAccessorMock = new Mock<IOptions<IdentityOptions>>();
      var userManagerLoggerMock = new Mock<ILogger<UserManager<UserEntity>>>();

      _userManagerMock = new Mock<UserManager<UserEntity>>(
        userStoreMock.Object,
        optionsAccessorMock.Object,
        passwordHasherMock.Object,
        new[] { userValidatorMock.Object }.AsEnumerable(),
        new[] { passwordValidatorMock.Object }.AsEnumerable(),
        keyNormalizerMock.Object,
        errorsMock.Object,
        servicesMock.Object,
        userManagerLoggerMock.Object);

      _clientServiceMock = new Mock<IClientService>();
      _scopeServiceMock = new Mock<IScopeService>();

      _databaseInitializer = new DatabaseInitializer(
        _configurationMock.Object,
        _userManagerMock.Object,
        _clientServiceMock.Object,
        _scopeServiceMock.Object);

      _userManagerMock.Reset();
    }

    [TestMethod]
    public async Task ExecuteAsync_Should_Not_Client_Scope_And_User()
    {
      _clientServiceMock.Setup(service => service.GetClientAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new ClientEntity())
                        .Verifiable();

      _scopeServiceMock.Setup(service => service.GetScopeAsync(It.IsAny<IScopeIdentity>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new ScopeEntity())
                       .Verifiable();

      _userManagerMock.Setup(manager => manager.FindByNameAsync(It.IsAny<string>()))
                      .ReturnsAsync(new UserEntity())
                      .Verifiable();

      var controlToken = Guid.NewGuid().ToString();

      _configurationMock.SetupGet(configuration => configuration[It.IsAny<string>()])
                        .Returns(controlToken)
                        .Verifiable();

      await _databaseInitializer.ExecuteAsync(_cancellationToken);

      _clientServiceMock.Verify(service => service.GetClientAsync(Clients.ApplicationClient, _cancellationToken));
      _clientServiceMock.VerifyNoOtherCalls();

      _scopeServiceMock.Verify(service => service.GetScopeAsync(Scopes.ApplicationScope.ToScopeIdentity(), _cancellationToken));
      _scopeServiceMock.VerifyNoOtherCalls();

      _userManagerMock.Verify(manager => manager.FindByNameAsync(controlToken));
      _userManagerMock.VerifyNoOtherCalls();

      _configurationMock.VerifyGet(configuration => configuration["WebApp_Url"]);
      _configurationMock.VerifyGet(configuration => configuration["TestUser_Email"]);
      _configurationMock.VerifyGet(configuration => configuration["TestUser_Name"]);
      _configurationMock.VerifyGet(configuration => configuration["TestUser_Password"]);
      _configurationMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task ExecuteAsync_Should_Add_Client_Scope_And_User()
    {
      _clientServiceMock.Setup(service => service.GetClientAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(default(ClientEntity))
                        .Verifiable();

      _clientServiceMock.Setup(service => service.AddClientAsync(It.IsAny<ClientEntity>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.CompletedTask)
                        .Verifiable();

      _scopeServiceMock.Setup(service => service.GetScopeAsync(It.IsAny<IScopeIdentity>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(default(ScopeEntity))
                       .Verifiable();

      _scopeServiceMock.Setup(service => service.AddScopeAsync(It.IsAny<ScopeEntity>(), It.IsAny<CancellationToken>()))
                       .Returns(Task.CompletedTask)
                       .Verifiable();

      _userManagerMock.Setup(manager => manager.FindByNameAsync(It.IsAny<string>()))
                      .ReturnsAsync(default(UserEntity))
                      .Verifiable();

      _userManagerMock.Setup(manager => manager.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
                      .Returns(Task.FromResult(IdentityResult.Success))
                      .Verifiable();

      var controlToken = Guid.NewGuid().ToString();

      _configurationMock.SetupGet(configuration => configuration[It.IsAny<string>()])
                        .Returns(controlToken)
                        .Verifiable();

      await _databaseInitializer.ExecuteAsync(_cancellationToken);

      _clientServiceMock.Verify();
      _clientServiceMock.VerifyNoOtherCalls();

      _scopeServiceMock.Verify();
      _scopeServiceMock.VerifyNoOtherCalls();

      _userManagerMock.Verify();
      _userManagerMock.VerifyNoOtherCalls();

      _configurationMock.Verify();
      _configurationMock.VerifyNoOtherCalls();
    }
  }
}
