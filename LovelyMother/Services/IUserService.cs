
using System.Threading.Tasks;
using MotherLibrary;

namespace LovelyMother.Services
{
    /// <summary>
    /// 用户服务。
    /// </summary>
    public interface IUserService
    {

        /// <summary>
        ///     根据用户名获得用户。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <returns>服务结果。</returns>
        Task<ServiceResult<User>> GetUserByUserNameAsync(
            string userName);


        /// <summary>
        ///     绑定账号。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <returns>服务结果。</returns>
        Task<ServiceResult> BindAccountAsync(string userName);



        /// <summary>
        ///     获得我。
        /// </summary>
        /// <returns>服务结果。</returns>
        Task<ServiceResult<User>> GetMeAsync();

    }
}
