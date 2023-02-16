// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Extensions.Test
{
  [TestClass]
  public sealed class IdentityExtensionsTest
  {
    [TestMethod]
    public void ToUserIdentity_Should_Return_User_Identity_For_Guid()
    {
      var control = Guid.NewGuid();
      var test = control.ToUserIdentity();

      Assert.IsNotNull(test);
      Assert.AreEqual(control, test.UserId);
    }

    [TestMethod]
    public void ToUserIdentity_Should_Return_User_Identity_For_String()
    {
      var control = Guid.NewGuid();
      var test = control.ToString().ToUserIdentity();

      Assert.IsNotNull(test);
      Assert.AreEqual(control, test.UserId);
    }
  }
}
