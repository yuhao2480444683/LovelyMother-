using MotherLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotherLibrary;
using Task = System.Threading.Tasks.Task;


namespace LovelyMother.Services
{
    /// <summary>
    /// 数据服务接口
    /// </summary>
    public interface IMotherService
    {

        /// <summary>
        /// 新建一个用户到数据库。
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        Task<Boolean> NewUserAsync(User User);
        /// <summary>
        /// 新建一个任务到数据库
        /// </summary>
        /// <param name="Task"></param>
        /// <returns></returns>
        System.Threading.Tasks.Task<Boolean> NewTaskAsync(MotherLibrary.Task Task, User User);
        /// <summary>
        /// 新建一个进程到数据库。
        /// </summary>
        /// <param name="Progress"></param>
        /// <returns></returns>
        Task<Boolean> NewProgressAsync(Progress Progress);



        /// <summary>
        /// 列出所有用户。
        /// </summary>
        /// <returns></returns>
        Task<List<User>> ListUserAsync();
        /// <summary>
        /// 列出所有进程。
        /// </summary>
        /// <returns></returns>
        Task<List<Progress>> ListProgressAsync();
        /// <summary>
        /// 列出所有任务。
        /// </summary>
        /// <returns></returns>
        Task<List<MotherLibrary.Task>> ListTaskAsync(User User);

        Task<List<MotherLibrary.Task>> ListTasksAsync();


        /// <summary>
        /// 更新当前项。
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        Task<Boolean> UpdateUserAsync(User User);
        Task<Boolean> UpdateTaskAsync(MotherLibrary.Task Task,User User);
        Task<Boolean> UpdateProgressAsync(Progress Progress);

        Task<Boolean> DeleteUserAsync(User User);
        Task<Boolean> DeleteTaskAsync(MotherLibrary.Task Task,User User);
        Task<Boolean> DeleteProgressAsync(Progress Progress);







    }
}
