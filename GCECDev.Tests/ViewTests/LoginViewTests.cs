using System;
using NUnit.Framework;
using GCECDev.Views;
using FakeItEasy;
using Xamarin.Forms;

namespace GCECDev.Tests.ViewTests
{
    [TestFixture]
    public class LoginViewTests
    {
        // Mock platform to overcome “You MUST call Xamarin.Forms.Init()”
        public LoginViewTests()
        {
            var platformServicesFake = A.Fake<Xamarin.Forms.Internals.IPlatformServices>();
            Device.PlatformServices = platformServicesFake;
        }

        [Test]
        public void NewLoginTest()
        {
            var login = new Login();
            Assert.NotNull(login);
        }
    }
}
