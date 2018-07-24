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
        /// 当前用户是否存在
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>

        Task<Boolean> RightPairAsync(String username,String password);


        /// <summary>
        /// 新建一个用户到数据库。
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        Task<Boolean> NewUserAsync(String username, String password);
        /// <summary>
        /// 新建一个任务到数据库
        /// </summary>
        /// <param name="Task"></param>
        /// <returns></returns>
        System.Threading.Tasks.Task<Boolean> NewTaskAsync(String username, int date, String begin, int defaulttime, String introduction);
        /// <summary>
        /// 新建一个进程到数据库。
        /// </summary>
        /// <param name="Progress"></param>
        /// <returns></returns>
        Task<Boolean> NewProgressAsync(String progressName, String defaultName);



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
        Task<List<MotherLibrary.Task>> ListTaskAsync(String userName);

        Task<List<MotherLibrary.Task>> ListTasksAsync();


        /// <summary>
        /// 更新当前项。
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        Task<Boolean> UpdateUserAsync(String userName, String passWord, int totalTime);
        Task<Boolean> UpdateTaskAsync(String userName, int date, String begin, String end, int defaultTime, int finish, int totalTime, String introduction);
        Task<Boolean> UpdateProgressAsync(String progressName, String defaultTime);

        Task<Boolean> DeleteUserAsync(String userName);
        Task<Boolean> DeleteTaskAsync(String userName, int date, String begin);
        Task<Boolean> DeleteProgressAsync(String progressName);







    }
}
