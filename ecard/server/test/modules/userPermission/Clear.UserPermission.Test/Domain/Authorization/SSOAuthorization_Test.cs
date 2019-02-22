using Clear.UserPermission.Domain.Authorization;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlatformService.BridgeComponent.Service.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Test.Domain.Authorization
{
    [TestClass]
    public class SSOAuthorization_Test
    {
        [TestMethod]
        public void ShouldNotGetException_WhenBindDataRight()
        {
            var foo = A.Fake<ISessionManager>();
            A.CallTo(() => foo.LoginName).Returns("Test");
            var service = new SSOAuthorization(foo, null);
            service.BindAccount("1", "6");
            Assert.Fail();
        }
    }
}
