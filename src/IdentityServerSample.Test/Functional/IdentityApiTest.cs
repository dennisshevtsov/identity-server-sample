namespace IdentityServerSample.IdentityApi.Test.Functional
{
  using IdentityModel.Client;
  using Microsoft.Extensions.Configuration;

  [TestClass]
  public sealed class IdentityApiTest
  {
#pragma warning disable CS8618
    private CancellationToken _cancellationToken;

    private IConfiguration _configuration;

    private HttpClient _identityHttpClient;
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
    }

    [TestMethod]
    public async Task RequestClientCredentialsTokenAsync_Should_Return_Error()
    {
      var discoveryResponse = await _identityHttpClient.GetDiscoveryDocumentAsync(
        null, _cancellationToken);

      Assert.IsNotNull(discoveryResponse);
      Assert.IsFalse(discoveryResponse.IsError, discoveryResponse.Error);

      var tokenResponse = await _identityHttpClient.RequestClientCredentialsTokenAsync(
        new ClientCredentialsTokenRequest
        {
          Address = discoveryResponse.TokenEndpoint,
          ClientId = Guid.NewGuid().ToString(),
          ClientSecret = Guid.NewGuid().ToString(),
          Scope = Guid.NewGuid().ToString(),
        },
        _cancellationToken);

      Assert.IsNotNull(tokenResponse);
      Assert.IsTrue(tokenResponse.IsError);
      Assert.AreEqual("invalid_client", tokenResponse.Json["error"]);
    }

    [TestMethod]
    public async Task RequestClientCredentialsTokenAsync_Should_Return_Token()
    {
      var discoveryResponse = await _identityHttpClient.GetDiscoveryDocumentAsync(
        null, _cancellationToken);

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
      Assert.IsNotNull(tokenResponse.AccessToken);
    }
  }
}
