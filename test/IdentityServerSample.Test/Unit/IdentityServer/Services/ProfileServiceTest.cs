// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityServer.Services.Test
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
      _userManagerMock.Setup(manager => manager.FindByIdAsync(It.IsAny<string>()))
                      .ReturnsAsync(default(UserEntity))
                      .Verifiable();

      var subjectId = Guid.NewGuid().ToString();
      var context = ProfileServiceTest.CreateContext(subjectId, new string[0]);

      await _profileService.GetProfileDataAsync(context);

      _userManagerMock.Verify(manager => manager.FindByIdAsync(subjectId));
      _userManagerMock.VerifyNoOtherCalls();

      _userClaimsPrincipalFactory.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetProfileDataAsync_Should_Add_Claims()
    {
      var subjectId = Guid.NewGuid().ToString();
      var claimTypeCollection = new[]
      {
        Guid.NewGuid().ToString(),
        Guid.NewGuid().ToString(),
      };

      var userEntity = new UserEntity();

      _userManagerMock.Setup(manager => manager.FindByIdAsync(It.IsAny<string>()))
                      .ReturnsAsync(userEntity)
                      .Verifiable();

      var principal = ProfileServiceTest.CreatePrincipal(subjectId, claimTypeCollection);

      _userClaimsPrincipalFactory.Setup(factory => factory.CreateAsync(It.IsAny<UserEntity>()))
                                 .ReturnsAsync(principal)
                                 .Verifiable();

      var context = ProfileServiceTest.CreateContext(subjectId, claimTypeCollection);

      await _profileService.GetProfileDataAsync(context);

      Assert.AreEqual(claimTypeCollection.Length, context.IssuedClaims.Count);

      foreach (var type in claimTypeCollection)
      {
        Assert.IsTrue(context.IssuedClaims.Any(claim => claim.Type == type));
      }

      _userManagerMock.Verify(manager => manager.FindByIdAsync(subjectId));
      _userManagerMock.VerifyNoOtherCalls();

      _userClaimsPrincipalFactory.Verify(factory => factory.CreateAsync(userEntity));
      _userClaimsPrincipalFactory.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task IsActiveAsync_Should_Set_True()
    {
      var context = new IsActiveContext(
        ProfileServiceTest.CreatePrincipal(Guid.NewGuid().ToString(), new string[0]),
        new Client(),
        Guid.NewGuid().ToString());

      await _profileService.IsActiveAsync(context);

      Assert.IsTrue(context.IsActive);
    }

    private static ClaimsPrincipal CreatePrincipal(string subjectId, string[] claimTypeCollection)
    {
      var claimCollection =
        claimTypeCollection.Select(type => new Claim(type, Guid.NewGuid().ToString()))
                           .ToList();

      claimCollection.Add(new Claim(JwtClaimTypes.Subject, subjectId));

      var identity = new ClaimsIdentity(claimCollection);
      var principal = new ClaimsPrincipal(identity);

      return principal;
    }

    private static ProfileDataRequestContext CreateContext(
      string subjectId, string[] claimTypeCollection)
    {
      return new ProfileDataRequestContext
      {
        Subject = ProfileServiceTest.CreatePrincipal(subjectId, new string[0]),
        RequestedClaimTypes = claimTypeCollection,
      };
    }
  }
}
