using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignalR2D2.Controllers;

namespace SignalR2D2.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var homeController = new HomeController();

            var result = homeController.Index();

            Assert.IsNotNull(result);
        }
    }
}
