// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApi.IdenittyServer.Test
{
  using System.Security.Claims;

  using IdentityModel;
  using IdentityServer4.Models;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  [TestClass]
  public sealed class ProfileServiceTest
  {
#pragma warning disable CS8618
    private Mock<UserManager<UserEntity>> _userManagerMock;
    private Mock<IUserClaimsPrincipalFactory<UserEntity>> _userClaimsPrincipalFactory;

    private ProfileService _profileService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
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

      _userClaimsPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<UserEntity>>();

      _profileService = new ProfileService(
        _userManagerMock.Object, _userClaimsPrincipalFactory.Object);

      _userManagerMock.Reset();
    }

    [TestMethod]
    public async Task GetProfileDataAsync_Should_Not_Add_Claims()
    {
      var subjectId = Guid.NewGuid().ToString();

      var context = new ProfileDataRequestContext
      {
        Subject = ProfileServiceTest.CreateSubject(subjectId),
      };

      _userManagerMock.Setup(manager => manager.FindByIdAsync(It.IsAny<string>()))
                      .ReturnsAsync(default(UserEntity))
                      .Verifiable();

      await _profileService.GetProfileDataAsync(context);

      _userManagerMock.Verify(manager => manager.FindByIdAsync(subjectId));
      _userManagerMock.VerifyNoOtherCalls();

      _userClaimsPrincipalFactory.VerifyNoOtherCalls();
    }

    private static ClaimsPrincipal CreateSubject(string subjectId)
    {
      var claims = new[]
      {
        new Claim(JwtClaimTypes.Subject, subjectId),
      };
      var identity = new ClaimsIdentity(claims);
      var principal = new ClaimsPrincipal(identity);

      return principal;
    }
  }
}
