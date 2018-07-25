using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using UvpClient.Services;

namespace LovelyMother.Services
{
    /// <summary>
    ///     身份服务接口。
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        ///     获得带有身份的HttpMessageHandler。
        /// </summary>
        /// <returns>带有身份的HttpMessageHandler</returns>
        IdentifiedHttpMessageHandler GetIdentifiedHttpMessageHandler();

        /// <summary>
        ///     登录。
        /// </summary>
        /// <returns>服务结果。</returns>
        Task<ServiceResult> LoginAsync();

        /// <summary>
        ///     保存。
        /// </summary>
        void Save();

        /// <summary>
        /// 注销。
        /// </summary>
        void SignOut();
    }

    /// <summary>
    ///     带有身份的HttpMessageHandler。
    /// </summary>
    public class IdentifiedHttpMessageHandler : RefreshTokenHandler
    {
        /// <summary>
        ///     根导航服务。
        /// </summary>
        private readonly IRootNavigationService _rootNavigationService;

        /// <summary>
        ///     TokenRefreshed事件处理函数。
        /// </summary>
        private readonly EventHandler<TokenRefreshedEventArgs>
            _tokenRefreshedEventHandler;

        /// <summary>
        ///     构造函数。
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="innerHandler">The inner handler.</param>
        /// <param name="rootNavigationService">根导航服务。</param>
        /// <param name="tokenRefreshedEventHandler">TokenRefreshed事件处理函数。</param>
        public IdentifiedHttpMessageHandler(TokenClient client,
            string refreshToken, string accessToken,
            HttpMessageHandler innerHandler,
            IRootNavigationService rootNavigationService,
            EventHandler<TokenRefreshedEventArgs> tokenRefreshedEventHandler) :
            base(client, refreshToken, accessToken, innerHandler){
             _rootNavigationService = rootNavigationService;
            _tokenRefreshedEventHandler = tokenRefreshedEventHandler;
            TokenRefreshed += _tokenRefreshedEventHandler;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken)
                .ConfigureAwait(false);
        
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                _rootNavigationService.Navigate(typeof(SignIn), null,
                    NavigationTransition.EntranceNavigationTransition);
            /*todo 无需binding 若需要此处修改
             else if (response.StatusCode == HttpStatusCode.Forbidden)
                _rootNavigationService.Navigate(typeof(BindingPage), null,
                    NavigationTransition.EntranceNavigationTransition);
            */
            return response;
        }

        protected override void Dispose(bool disposing)
        {
            TokenRefreshed -= _tokenRefreshedEventHandler;
            base.Dispose(disposing);
        }
    }
}
