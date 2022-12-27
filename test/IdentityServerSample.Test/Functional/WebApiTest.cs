// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.WebApi.Test.Functional
{
  using System.Net;
  using System.Net.Http.Json;
  using System.Text.Json;

  using IdentityModel.Client;
  using Microsoft.Extensions.Configuration;

  using IdentityServerSample.ApplicationCore.Dtos;

  [TestClass]
  public sealed class WebApiTest
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
        BaseAddress = new Uri(_configuration["IdentityApi_Url"]!),
      };
    }

    [TestMethod]
    public async Task Get_Scopes_Should_Return_Unauthorized()
    {
      var userResponse = await _apiHttpClient.GetAsync($"api/scope");

      Assert.IsNotNull(userResponse);
      Assert.AreEqual(HttpStatusCode.Unauthorized, userResponse.StatusCode);
    }

    [TestMethod]
    public async Task Get_Scopes_Should_Return_Scopes()
    {
      var discoveryResponse = await _identityHttpClient.GetDiscoveryDocumentAsync();

      Assert.IsNotNull(discoveryResponse);
      Assert.IsFalse(discoveryResponse.IsError, discoveryResponse.Error);

      var tokenResponse = await _identityHttpClient.RequestClientCredentialsTokenAsync(
        new ClientCredentialsTokenRequest
        {
          Address = discoveryResponse.TokenEndpoint,
          ClientId = _configuration["Client_Id_0"]!,
          ClientSecret = _configuration["Client_Secret_0"]!,
          Scope = _configuration["ApiScope_Name"]!,
        });

      Assert.IsNotNull(tokenResponse);
      Assert.IsFalse(tokenResponse.IsError, tokenResponse.Error);

      _apiHttpClient.SetBearerToken(tokenResponse.AccessToken);

      var userResponse = await _apiHttpClient.GetAsync($"api/scope");

      Assert.IsNotNull(userResponse);
      Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode);

      var userDto = await userResponse.Content.ReadFromJsonAsync<GetScopesResponseDto>(
        new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        },
        _cancellationToken);

      Assert.IsNotNull(userDto);

      Assert.IsNotNull(userDto.Scopes);
      Assert.IsTrue(userDto.Scopes.Length > 0);
    }
  }
}
