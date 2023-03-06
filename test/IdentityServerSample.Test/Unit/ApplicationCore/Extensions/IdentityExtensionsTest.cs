// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.ApplicationCore.Identities.Test
{
  [TestClass]
  public sealed class IdentityExtensionsTest
  {
    [TestMethod]
    public void ToAudienceIdentity_Should_Return_Audience_Identity()
    {
      var control = Guid.NewGuid().ToString();
      var test = control.ToAudienceIdentity();

      Assert.IsNotNull(test);
      Assert.AreEqual(control, test.AudienceName);
    }

    [TestMethod]
    public void ToAudienceIdentities_Should_Return_Collection_Of_Audience_Identity()
    {
      var control = new List<string>
      {
        Guid.NewGuid().ToString(),
        Guid.NewGuid().ToString(),
        Guid.NewGuid().ToString(),
      };

      var test = control.ToAudienceIdentities();

      Assert.IsNotNull(test);

      var testAudienceIdentityCollection = test.ToList();

      Assert.AreEqual(control.Count, testAudienceIdentityCollection.Count);

      for (int i = 0; i < control.Count; i++)
      {
        Assert.AreEqual(control[i], testAudienceIdentityCollection[i].AudienceName);
      }
    }

    [TestMethod]
    public void ToClientIdentity_Should_Return_Client_Identity()
    {
      var control = Guid.NewGuid().ToString();
      var test = control.ToClientIdentity();

      Assert.IsNotNull(test);
      Assert.AreEqual(control, test.ClientName);
    }

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

    [TestMethod]
    public void ToUserIdentity_Should_Return_Null()
    {
      var control = "test";
      var test = control.ToUserIdentity();

      Assert.IsNull(test);
    }

    [TestMethod]
    public void ToScopeIdentity_Should_Return_Scope_Identity()
    {
      var control = Guid.NewGuid().ToString();
      var test = control.ToScopeIdentity();

      Assert.IsNotNull(test);
      Assert.AreEqual(control, test.ScopeName);
    }

    [TestMethod]
    public void ToScopeIdentities_Should_Return_Collection_Of_Scope_Identity()
    {
      var control = new List<string>
      {
        Guid.NewGuid().ToString(),
        Guid.NewGuid().ToString(),
        Guid.NewGuid().ToString(),
      };

      var test = control.ToScopeIdentities();

      Assert.IsNotNull(test);

      var testScopeIdentityCollection = test.ToList();

      Assert.AreEqual(control.Count, testScopeIdentityCollection.Count);

      for (int i = 0; i < control.Count; i++)
      {
        Assert.AreEqual(control[i], testScopeIdentityCollection[i].ScopeName);
      }
    }
  }
}
