using Windows.System;
using CommonServiceLocator;
using LovelyMother.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MotherLibrary;
using Task = System.Threading.Tasks.Task;
using User = Windows.System.User;

namespace TestServices.TestMotherService
{
    [TestClass]
    public class TestMotherService
    {
        //[TestMethod]      //Success
        public void TestMigration()
         {
             using (var db = new MyDatabaseContext())
             {
                 db.Database.Migrate();
             }
         }

 

        /* Assert.AreEqual( 4, users[0].Id);
           Assert.AreEqual("1", users[0].UserName);
           Assert.AreEqual("1", users[0].Password);*/

        [TestMethod]
        public async Task TestUserServicesAsync()
        {
           

            var motherService = new MotherService();                 //声明服务

             var users = await motherService.ListUserAsync();         //初始时
             Assert.AreEqual(0,users.Count);
            
             var user1 = new MotherLibrary.User { UserName = "Test1", Password = "1", TotalTime = 0 };
             var user2 = new MotherLibrary.User { UserName = "Test2", Password = "2", TotalTime = 0 };
             var user3 = new MotherLibrary.User { UserName = "Test3", Password = "3", TotalTime = 0 };
             await motherService.NewUserAsync(user1);
             await motherService.NewUserAsync(user2);
             await motherService.NewUserAsync(user3);              //插入3个测试用户

             users = await motherService.ListUserAsync();         
             Assert.AreEqual(3,users.Count);                       //应有3个用户
             Assert.AreEqual("Test1",users[0].UserName);
                        Assert.AreEqual("1", users[0].Password);
                        Assert.AreEqual(0,users[0].TotalTime);

                        Assert.AreEqual("Test2", users[1].UserName);
                        Assert.AreEqual("2", users[1].Password);
                        Assert.AreEqual(0, users[1].TotalTime);

                        Assert.AreEqual("Test3", users[2].UserName);
                        Assert.AreEqual("3", users[2].Password);
                        Assert.AreEqual(0, users[2].TotalTime);


                        users[0].Password = "111";                 //修改用户数据
                        users[0].TotalTime = 10;
                        users[1].Password = "222";
                        users[1].TotalTime = 20;
                        users[2].Password = "333";
                        users[2].TotalTime = 30;

                        await motherService.UpdateUserAsync(users[0]);
                        await motherService.UpdateUserAsync(users[1]);
                        await motherService.UpdateUserAsync(users[2]);

                        users = await motherService.ListUserAsync();       //确认修改的数据保存成功
                        Assert.AreEqual("111",users[0].Password);
                        Assert.AreEqual(10,users[0].TotalTime);
                        Assert.AreEqual("222", users[1].Password);
                        Assert.AreEqual(20, users[1].TotalTime);
                        Assert.AreEqual("333", users[2].Password);
                        Assert.AreEqual(30, users[2].TotalTime);


                        Assert.AreEqual(3,users.Count);
                        await motherService.DeleteUserAsync(users[0]);          //删除测试用户
                        await motherService.DeleteUserAsync(users[1]);
                        await motherService.DeleteUserAsync(users[2]);


                        users = await motherService.ListUserAsync();       //确认数量
                        Assert.AreEqual(0,users.Count);



        }


    }
}
