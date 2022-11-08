// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Test.Integration
{
  using System.Net;
  using System.Net.Http.Json;
  using System.Text.Json;

  using IdentityModel.Client;
  using Microsoft.Extensions.Configuration;

  using IdentityServerSample.WebApi.Dtos;

  [TestClass]
  public sealed class UserControllerTest
  {
#pragma warning disable CS8618
    private CancellationToken _cancellationToken;

    private IConfiguration _configuration;

    private HttpClient _identityHttpClient;
    private HttpClient _apiHttpClient;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                 .Build();

      _identityHttpClient = new HttpClient
      {
        BaseAddress = new Uri(_configuration["IdentityApi_Url"]!),
      };

      _apiHttpClient = new HttpClient
      {
        BaseAddress = new Uri(_configuration["WebApi_Url"]!),
      };
    }

    [TestMethod]
    public async Task RequestClientCredentialsTokenAsync_Should_Return_Error()
    {
      var discoveryResponse = await _identityHttpClient.GetDiscoveryDocumentAsync();

      Assert.IsNotNull(discoveryResponse);
      Assert.IsFalse(discoveryResponse.IsError, discoveryResponse.Error);

      var tokenResponse = await _identityHttpClient.RequestClientCredentialsTokenAsync(
        new ClientCredentialsTokenRequest
        {
          Address = discoveryResponse.TokenEndpoint,
          ClientId = Guid.NewGuid().ToString(),
          ClientSecret = Guid.NewGuid().ToString(),
          Scope = Guid.NewGuid().ToString(),
        });

      Assert.IsNotNull(tokenResponse);
      Assert.IsTrue(tokenResponse.IsError);
      Assert.AreEqual("invalid_client", tokenResponse.Json["error"]);
    }

    [TestMethod]
    public async Task Get_User_Authenticated_Should_Return_Unauthorized()
    {
      var userResponse = await _apiHttpClient.GetAsync("api/user/authenticated");

      Assert.IsNotNull(userResponse);
      Assert.AreEqual(HttpStatusCode.Unauthorized, userResponse.StatusCode);
    }

    [TestMethod]
    public async Task Get_User_Authenticated_Should_Return_User()
    {
      var discoveryResponse = await _identityHttpClient.GetDiscoveryDocumentAsync();

      Assert.IsNotNull(discoveryResponse);
      Assert.IsFalse(discoveryResponse.IsError, discoveryResponse.Error);

      var tokenResponse = await _identityHttpClient.RequestClientCredentialsTokenAsync(
        new ClientCredentialsTokenRequest
        {
          Address = discoveryResponse.TokenEndpoint,
          ClientId = _configuration["Client_Id"]!,
          ClientSecret = _configuration["Client_Secret"]!,
          Scope = _configuration["ApiScope_Name"]!,
        });

      Assert.IsNotNull(tokenResponse);
      Assert.IsFalse(tokenResponse.IsError, tokenResponse.Error);

      _apiHttpClient.SetBearerToken(tokenResponse.AccessToken);

      var userResponse = await _apiHttpClient.GetAsync("api/user/authenticated");

      Assert.IsNotNull(userResponse);
      Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode);

      var userDto = await userResponse.Content.ReadFromJsonAsync<UserDto>(
        new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        },
        _cancellationToken);

      Assert.IsNotNull(userDto);
      Assert.IsNull(userDto.Name);

      Assert.IsNotNull(userDto.Claims);
      Assert.IsTrue(userDto.Claims.Length > 0);
    }
  }
}
