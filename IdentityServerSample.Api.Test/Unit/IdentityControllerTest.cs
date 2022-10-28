using IdentityServerSample.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerSample.Api.Test.Unit
{
  [TestClass]
  public sealed class IdentityControllerTest
  {
#pragma warning disable CS8618
    private IdentityController _controller;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _controller = new IdentityController();
    }

    [TestMethod]
    public void GetTest()
    {
      var actionResult = _controller.Get();

      Assert.IsNotNull(actionResult);
    }
  }
}
