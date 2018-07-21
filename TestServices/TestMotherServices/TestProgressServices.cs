using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LovelyMother.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestServices.TestMotherServices
{
    [TestClass]
    public class TestProgressServices
    {
        [TestMethod]          //Success
        public async Task TestProgressServicesAsync()
        {

            var motherService = new MotherService();                 //声明服务

            var progresses = await motherService.ListProgressAsync();         //初始时
            Assert.AreEqual(0, progresses.Count);

            var progress1 = new MotherLibrary.Progress { ProgressName = "TestProgress1",DefaultName = "Test1"};
            var progress2 = new MotherLibrary.Progress { ProgressName = "TestProgress2",DefaultName = "Test2"};
            var progress3 = new MotherLibrary.Progress { ProgressName = "TestProgress3",DefaultName = "Test3" };
            await motherService.NewProgressAsync(progress1.ProgressName, progress1.DefaultName); //插入3个测试进程
            await motherService.NewProgressAsync(progress2.ProgressName, progress2.DefaultName);
            await motherService.NewProgressAsync(progress3.ProgressName, progress3.DefaultName);


            progresses = await motherService.ListProgressAsync();
            Assert.AreEqual(3, progresses.Count);                       //应有3个进程

            Assert.AreEqual("TestProgress1", progresses[0].ProgressName);
            Assert.AreEqual("Test1", progresses[0].DefaultName);

            Assert.AreEqual("TestProgress2", progresses[1].ProgressName);
            Assert.AreEqual("Test2", progresses[1].DefaultName);

            Assert.AreEqual("TestProgress3", progresses[2].ProgressName);
            Assert.AreEqual("Test3", progresses[2].DefaultName);


            
            progresses[0].DefaultName = "test111";                 //修改进程数据
            progresses[1].DefaultName = "test222";
            progresses[2].DefaultName = "test333";

            await motherService.UpdateProgressAsync(progresses[0].ProgressName, progresses[0].DefaultName);
            await motherService.UpdateProgressAsync(progresses[1].ProgressName, progresses[1].DefaultName);
            await motherService.UpdateProgressAsync(progresses[2].ProgressName, progresses[2].DefaultName);


            progresses = await motherService.ListProgressAsync();       //确认修改的数据保存成功
            Assert.AreEqual("test111", progresses[0].DefaultName);
            Assert.AreEqual("test222", progresses[1].DefaultName);
            Assert.AreEqual("test333", progresses[2].DefaultName);
            


            
            Assert.AreEqual(3, progresses.Count);
            await motherService.DeleteProgressAsync(progresses[0].ProgressName);          //删除测试进程
            await motherService.DeleteProgressAsync(progresses[1].ProgressName);
            await motherService.DeleteProgressAsync(progresses[2].ProgressName);


            progresses = await motherService.ListProgressAsync();       //确认数量
            Assert.AreEqual(0, progresses.Count);



        }


    }//class
}
