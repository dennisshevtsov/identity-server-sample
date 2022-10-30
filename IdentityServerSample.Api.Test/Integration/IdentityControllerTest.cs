using IdentityModel.Client;
using IdentityServerSample.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerSample.Api.Test.Integration
{
  [TestClass]
  public sealed class IdentityControllerTest
  {
#pragma warning disable CS8618
    private HttpClient _httpClient;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _httpClient = new HttpClient();
    }

    [TestMethod]
    public async Task Test()
    {
      var discovery = await _httpClient.GetDiscoveryDocumentAsync("http://localhost:5214");

      Assert.IsFalse(discovery.IsError);
    }
  }
}
