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
    //[TestClass]
    public class TestProgressViewModel
    {
        /*[TestMethod]
        public void TestAddProgressCommand()
        {
            var progresses = new Progress[]
            {
                new Progress {ProgressName = "testp1", DefaultName = "111"},
                new Progress {ProgressName = "testp2", DefaultName = "222"}

            };
            var stubIMotherService = new StubIMotherService();
            stubIMotherService.NewProgressAsync(async () => progresses);
            var mainPageViewModel = new TestViewModel(stubIMotherService);
            mainPageViewModel.AddProgressCommand.Execute(null);
            Assert.AreEqual(2, mainPageViewModel.ProgressCollection.Count);
        }*/

    }
}
