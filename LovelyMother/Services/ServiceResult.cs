using System.Collections.Generic;
using System.Net;

namespace LovelyMother.Services
{
    /// <summary>
    ///     服务结果。
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        ///     服务结果状态。
        /// </summary>
        public ServiceResultStatus Status { get; set; }

        /// <summary>
        ///     信息。
        /// </summary>
        public string Message { get; set; }
    }


    public class ServiceResult<T>
    {
        /// <summary>
        ///     结果。
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        ///     服务结果状态。
        /// </summary>
        public ServiceResultStatus Status { get; set; }

        /// <summary>
        ///     信息。
        /// </summary>
        public string Message { get; set; }
    }



    /// <summary>
    ///     服务结果状态。
    /// </summary>
    public enum ServiceResultStatus
    {
        /// <summary>
        ///     未知。
        /// </summary>
        Unknown = -100,

        /// <summary>
        ///     异常。
        /// </summary>
        Exception = -200,

        /// <summary>
        ///     OK。
        /// </summary>
        OK = HttpStatusCode.OK,

        /// <summary>
        ///     无内容。
        /// </summary>
        NoContent = HttpStatusCode.NoContent,

        /// <summary>
        ///     错误的请求。
        /// </summary>
        BadRequest = HttpStatusCode.BadRequest,

        /// <summary>
        ///     未授权。
        /// </summary>
        Unauthorized = HttpStatusCode.Unauthorized,


        /// <summary>
        ///     未找到。
        /// </summary>
        NotFound = HttpStatusCode.NotFound
    }


    /// <summary>
    ///     服务结果状态助手。
    /// </summary>
    public class ServiceResultStatusHelper
    {
        /// <summary>
        ///     服务结果状态字典。
        /// </summary>
        private static readonly Dictionary<HttpStatusCode, ServiceResultStatus>
            ServiceResultStatusDictionary =
                new Dictionary<HttpStatusCode, ServiceResultStatus>();



        /// <summary>
        ///     静态构造函数。
        /// </summary>
        static ServiceResultStatusHelper()
        {
            ServiceResultStatusDictionary[HttpStatusCode.OK] =
                ServiceResultStatus.OK;
            ServiceResultStatusDictionary[HttpStatusCode.NoContent] =
                ServiceResultStatus.NoContent;
            ServiceResultStatusDictionary[HttpStatusCode.BadRequest] =
                ServiceResultStatus.BadRequest;
            ServiceResultStatusDictionary[HttpStatusCode.Unauthorized] =
                ServiceResultStatus.Unauthorized;
            ServiceResultStatusDictionary[HttpStatusCode.NotFound] =
                ServiceResultStatus.NotFound;
        }

        /// <summary>
        ///     从HttpStatusCode创建服务结果状态。
        /// </summary>
        /// <param name="httpStatusCode">HttpStatusCode。</param>
        /// <returns>从HttpStatusCode创建的服务结果状态。</returns>
        public static ServiceResultStatus FromHttpStatusCode(
            HttpStatusCode httpStatusCode)
        {
            return ServiceResultStatusDictionary.ContainsKey(httpStatusCode)
                ? ServiceResultStatusDictionary[httpStatusCode]
                : ServiceResultStatus.Unknown;
        }
    }



}
