using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LovelyMother.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MotherLibrary;

namespace TestServices.TestProgressViewModel
{
    [TestClass]
    public class TestProgressViewModel
    {
        [TestMethod]
        public void TestListProgressCommand()
        {
            var progresses = new Progress[]
            {
                new Progress {ProgressName = "testp1", DefaultName = "111"},
                new Progress {ProgressName = "testp2", DefaultName = "222"}

            };
            var stubIContactService = new StubIContactService();
            stubIContactService.ListProgressAsync(async () => progresses);
            var progressViewModel = new ProgressViewModel(stubIContactService);
            progressViewModel.ListProgressCommand.Execute(null);
            Assert.AreEqual(2, progressViewModel.ProgressCollection.Count);
        }

    }
}
