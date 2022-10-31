﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.Api.Test.Integration
{
  using IdentityModel.Client;

  [TestClass]
  public sealed class IdentityControllerTest
  {
#pragma warning disable CS8618
    private HttpClient _identityHttpClient;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _identityHttpClient = new HttpClient
      {
        BaseAddress = new Uri("http://localhost:5214"),
      };
    }

    [TestMethod]
    public async Task Test()
    {
      var discovery = await _identityHttpClient.GetDiscoveryDocumentAsync();

      Assert.IsFalse(discovery.IsError);
    }
  }
}
