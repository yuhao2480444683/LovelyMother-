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
           


            var tasks = await motherService.ListTasksAsync();  //所有Tasks
            Assert.AreEqual(0,tasks.Count);


            var user1 = new User { UserName = "testuser", Password = "1"};    //增加1个测试用户
            await motherService.NewUserAsync(user1.UserName,user1.Password);

            users = await motherService.ListUserAsync();
            Assert.AreEqual(1, users.Count);
            


            var task1 = new MotherLibrary.Task { UserId = users[0].Id, Date = 20180720, Begin = "10:59", DefaultTime = 60, Introduction = "test1" };
            var task2 = new MotherLibrary.Task { UserId = users[0].Id, Date = 20180721, Begin = "10:59", DefaultTime = 60, Introduction = "test2" };
            var task3 = new MotherLibrary.Task { UserId = users[0].Id, Date = 20180722, Begin = "10:59", DefaultTime = 60, Introduction = "test3" };


            var result1 = await motherService.NewTaskAsync(users[0].UserName, task1.Date, task1.Begin, task1.DefaultTime, task1.Introduction); //新建3个任务。
            var result2 = await motherService.NewTaskAsync(users[0].UserName, task2.Date, task2.Begin, task2.DefaultTime, task2.Introduction);
            var result3 = await motherService.NewTaskAsync(users[0].UserName, task3.Date, task3.Begin, task3.DefaultTime, task3.Introduction);

            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(true, result3);



            tasks = await motherService.ListTaskAsync(users[0].UserName);               //查看任务。
            Assert.AreEqual(3, tasks.Count);
            Assert.AreEqual(20180720, tasks[0].Date);
            Assert.AreEqual(20180721, tasks[1].Date);
            Assert.AreEqual(20180722, tasks[2].Date);


            await motherService.DeleteTaskAsync(users[0].UserName, tasks[0].Date, tasks[0].Begin);
            await motherService.DeleteTaskAsync(users[0].UserName, tasks[1].Date, tasks[1].Begin);
            await motherService.DeleteTaskAsync(users[0].UserName, tasks[2].Date, tasks[2].Begin);


            tasks = await motherService.ListTaskAsync(users[0].UserName);
            Assert.AreEqual(0, tasks.Count);

            users = await motherService.ListUserAsync();
            Assert.AreEqual(1, users.Count);

            await motherService.DeleteUserAsync(users[0].UserName);
            users = await motherService.ListUserAsync();
            Assert.AreEqual(0, users.Count);




        }

    }
}
