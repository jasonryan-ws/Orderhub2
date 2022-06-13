using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WS.OrderHub.ViewModels.Tests
{
    [TestClass]
    public class UpdateOrdersTests
    {
        [TestMethod]
        public void StartTest()
        {
            var vm = new UpdateOrdersViewModel();
            vm.StartCommand.Execute(null);
            Assert.IsNotNull(vm);
        }
    }
}