// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Api.Test.Integration
{
  using IdentityModel.Client;

  [TestClass]
  public sealed class UserControllerTest
  {
#pragma warning disable CS8618
    private HttpClient _identityHttpClient;
    private HttpClient _apiHttpClient;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _identityHttpClient = new HttpClient
      {
        BaseAddress = new Uri("http://localhost:5214"),
      };

      _apiHttpClient = new HttpClient
      {
        BaseAddress = new Uri("http://localhost:5030/api/"),
      };
    }

    [TestMethod]
    public async Task Test()
    {
      var discoveryResponse = await _identityHttpClient.GetDiscoveryDocumentAsync();

      Assert.IsNotNull(discoveryResponse);
      Assert.IsFalse(discoveryResponse.IsError, discoveryResponse.Error);

      var tokenResponse = await _identityHttpClient.RequestClientCredentialsTokenAsync(
        new ClientCredentialsTokenRequest
        {
          Address = discoveryResponse.TokenEndpoint,
          ClientId = "testClient",
          ClientSecret = "testSecret",
          Scope = "testScope",
        });

      Assert.IsNotNull(tokenResponse);
      Assert.IsTrue(tokenResponse.IsError, tokenResponse.Error);

      _apiHttpClient.SetBearerToken(tokenResponse.AccessToken);

      var userResponse = await _apiHttpClient.GetAsync("user/current");

      Assert.IsNotNull(userResponse);
    }
  }
}
