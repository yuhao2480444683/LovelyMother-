using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MotherLibrary;
using Newtonsoft.Json;

namespace LovelyMother.Services
{
    /// <summary>
    /// 用户服务。
    /// </summary>
    public class UserService:IUserService
    {
        /// <summary>
        ///     身份服务。
        /// </summary>
        private readonly IIdentityService _identityService;


        /// <summary>
        ///     构造函数。
        /// </summary>
        /// <param name="identityService">身份服务。</param>
        public UserService(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        /// <summary>
        ///  根据用户名获得用户。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <returns></returns>
        public async Task<ServiceResult<User>> GetUserByUserNameAsync(
            string userName)
        {
            var identifiedHttpMessageHandler =
                _identityService.GetIdentifiedHttpMessageHandler();
            using (var httpClient =
                new HttpClient(identifiedHttpMessageHandler))
            {
                HttpResponseMessage response;
                try
                {
                    response = await httpClient.GetAsync(
                        App.ServerEndpoint + "/api/User?userName=" +
                        HttpUtility.UrlEncode(userName));
                }
                catch (Exception e)
                {
                    return new ServiceResult<User>
                    {
                        Status = ServiceResultStatus.Exception,
                        Message = e.Message
                    };
                }

                var serviceResult = new ServiceResult<User>
                {
                    Status =
                        ServiceResultStatusHelper.FromHttpStatusCode(
                            response.StatusCode)
                };

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        break;
                    case HttpStatusCode.OK:
                        var json = await response.Content.ReadAsStringAsync();
                        serviceResult.Result =
                            JsonConvert.DeserializeObject<User>(json);
                        break;
                    case HttpStatusCode.NotFound:
                        break;
                    default:
                        serviceResult.Message = response.ReasonPhrase;
                        break;
                }

                return serviceResult;
            }
        }



        /// <summary>
        /// 绑定用户名。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <returns></returns>

        public async Task<ServiceResult> BindAccountAsync(string userName)
        {
            var identifiedHttpMessageHandler =
                _identityService.GetIdentifiedHttpMessageHandler();
            using (var httpClient =
                new HttpClient(identifiedHttpMessageHandler))
            {
                HttpResponseMessage response;
                try
                {
                    response = await httpClient.PutAsync(
                        App.ServerEndpoint + "/api/User?userName=" +
                        HttpUtility.UrlEncode(userName),
                        new StringContent(""));
                }
                catch (Exception e)
                {
                    return new ServiceResult
                    {
                        Status = ServiceResultStatus.Exception,
                        Message = e.Message
                    };
                }

                var serviceResult = new ServiceResult
                {
                    Status =
                        ServiceResultStatusHelper.FromHttpStatusCode(
                            response.StatusCode)
                };

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        break;
                    case HttpStatusCode.NoContent:
                        break;
                    case HttpStatusCode.BadRequest:
                        serviceResult.Message =
                            await response.Content.ReadAsStringAsync();
                        break;
                    default:
                        serviceResult.Message = response.ReasonPhrase;
                        break;
                }

                return serviceResult;
            }
        }



        /// <summary>
        /// 获得我。
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<User>> GetMeAsync()
        {
            var identifiedHttpMessageHandler =
                _identityService.GetIdentifiedHttpMessageHandler();
            using (var httpClient =
                new HttpClient(identifiedHttpMessageHandler))
            {
                HttpResponseMessage response;
                try
                {
                    response =
                        await httpClient.GetAsync(
                            App.ServerEndpoint + "/api/Me");
                }
                catch (Exception e)
                {
                    return new ServiceResult<User>
                    {
                        Status = ServiceResultStatus.Exception,
                        Message = e.Message
                    };
                }

                var serviceResult = new ServiceResult<User>
                {
                    Status =
                        ServiceResultStatusHelper.FromHttpStatusCode(
                            response.StatusCode)
                };

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.Forbidden:
                        break;
                    case HttpStatusCode.OK:
                        var json = await response.Content.ReadAsStringAsync();
                        serviceResult.Result =
                            JsonConvert.DeserializeObject<User>(json);
                        break;
                    default:
                        serviceResult.Message = response.ReasonPhrase;
                        break;
                }

                return serviceResult;
            }
        }


    }
}
