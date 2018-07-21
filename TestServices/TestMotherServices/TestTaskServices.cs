using LovelyMother.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MotherLibrary;
using Task = System.Threading.Tasks.Task;

namespace TestServices.TestMotherServices
{
    [TestClass]
    public class TestTaskServices
    {
        
        //[TestMethod]         
        public async Task TestTaskServicesAsync()
        {
            var motherService = new MotherService();
            var users = await motherService.ListUserAsync();


            /* var tasks = await motherService.ListTasksAsync();
             Assert.AreEqual(0,tasks.Count);*/


            /*Assert.AreEqual(1, users.Count);
            await motherService.DeleteUserAsync(users[0]);
            users = await motherService.ListUserAsync();


            Assert.AreEqual(0, users.Count);*/





            Assert.AreEqual(0, users.Count);

            var user1 = new User{ UserName = "testuser",Password = "1",TotalTime = 0};    //增加1个测试用户
            await motherService.NewUserAsync(user1);
            users = await motherService.ListUserAsync();
            Assert.AreEqual(1,users.Count);


            var tasks = await motherService.ListTaskAsync(user1);      //初始时无任务
            Assert.AreEqual(0, tasks.Count);


            var task1 = new MotherLibrary.Task { UserId = user1.Id, Date = 20180720, Begin = "10:59", DefaultTime = 60, Introduction = "test1" };
            var task2 = new MotherLibrary.Task { UserId = user1.Id, Date = 20180721, Begin = "10:59", DefaultTime = 60, Introduction = "test2" };
            var task3 = new MotherLibrary.Task { UserId = user1.Id, Date = 20180722, Begin = "10:59", DefaultTime = 60, Introduction = "test3" };
            var result1 =  await motherService.NewTaskAsync(task1, user1);
            var result2 = await motherService.NewTaskAsync(task2, user1);                  //新建3个任务。
            var result3 = await motherService.NewTaskAsync(task3, user1);

            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(true, result3);


            tasks = await motherService.ListTaskAsync(user1);               //查看任务。
            Assert.AreEqual(3, tasks.Count);
            Assert.AreEqual(20180720, tasks[0].Date);
            Assert.AreEqual(20180721, tasks[1].Date);
            Assert.AreEqual(20180722, tasks[2].Date);


            await motherService.DeleteTaskAsync(tasks[0],user1);
            await motherService.DeleteTaskAsync(tasks[1], user1);
            await motherService.DeleteTaskAsync(tasks[2], user1);


            tasks = await motherService.ListTaskAsync(user1);
            Assert.AreEqual(0,tasks.Count);

            users = await motherService.ListUserAsync();
            Assert.AreEqual(1, users.Count);

            await motherService.DeleteUserAsync(user1);
            users = await motherService.ListUserAsync();
            Assert.AreEqual(0, users.Count);
            



        }

        [TestMethod]
        public async Task TestNewAsync()
        {
            var motherService = new MotherService();
            var users = await motherService.ListUserAsync();

            /* var tasks = await motherService.ListTasksAsync();
            Assert.AreEqual(0,tasks.Count);*/


            Assert.AreEqual(1, users.Count);
            await motherService.DeleteUserAsync(users[0]);
            users = await motherService.ListUserAsync();


            Assert.AreEqual(0, users.Count);


            /*Assert.AreEqual(0, users.Count);

            var user1 = new User { UserName = "testuser", Password = "1", TotalTime = 0 };    //增加1个测试用户
            await motherService.NewUserAsync(user1);
            users = await motherService.ListUserAsync();
            Assert.AreEqual(1, users.Count);

            var task1 = new MotherLibrary.Task { UserId = user1.Id, Date = 20180720, Begin = "10:59", DefaultTime = 60, Introduction = "test1" };
            var task2 = new MotherLibrary.Task { UserId = user1.Id, Date = 20180721, Begin = "10:59", DefaultTime = 60, Introduction = "test2" };
            var task3 = new MotherLibrary.Task { UserId = user1.Id, Date = 20180722, Begin = "10:59", DefaultTime = 60, Introduction = "test3" };
            var result1 = await motherService.NewTaskAsync(task1, user1);
            var result2 = await motherService.NewTaskAsync(task2, user1);                  //新建3个任务。
            var result3 = await motherService.NewTaskAsync(task3, user1);

            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(true, result3);*/



        }

    }
}
