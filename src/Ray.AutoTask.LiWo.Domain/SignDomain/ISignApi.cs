using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Ray.AutoTask.Infrastructure.Attributes;
using Ray.Infrastructure;
using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace Ray.AutoTask.LiWo.Domain.SignDomain
{
    [HttpHost("https://api.m.jd.com/")]
    [Header("Referer", "https://2do.jd.com/events/7-day")]
    [LogFilter]
    public interface ISignApi : ILiWoApi
    {
        [HttpPost("api/v1/sign/doSign")]
        Task<LiWoResponse<SignResponse>> DoSign([Required][FormContent] SignRequest request, [JsonFormField] SignBodyAto body);

        [HttpPost("api/v1/sign/resetSign")]
        Task<LiWoResponse<SignResponse>> ResetSign([Required][FormContent] SignRequest request, [JsonFormField] SignBodyAto body);
    }

    public class SignRequest : LiWoRequest
    {
        public SignRequest()
        {
            Appid = "yocial-h5";
            FunctionId = "v1_sign_doSign";
            LoginType = 2;
        }
    }

    public class SignBodyAto
    {
        public int SdkTokenError { get; set; } = 0;

        public int AppType { get; set; } = 3;

        public string SdkToken => Global.ConfigurationRoot["SdkToken"];
    }

    public class SignResponse
    {
        /// <summary>
        /// 标题
        /// <example>连续签到5天</example>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息
        /// <example>您今日登陆签到到了0.35元</example>
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 内容
        /// <example>+0.35元</example>
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 本次获取金额
        /// <example>0.35</example>
        /// </summary>
        public decimal CurrentAmount { get; set; }

        public int ReceiveType { get; set; }

        public string Extra { get; set; }
    }
}
