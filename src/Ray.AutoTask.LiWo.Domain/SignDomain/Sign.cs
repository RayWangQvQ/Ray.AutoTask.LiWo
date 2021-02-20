using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Attributes;

namespace Ray.AutoTask.LiWo.Domain.SignDomain
{
    public class Sign
    {
        private readonly ILogger<Sign> _logger;
        private readonly ISignApi _signApi;
        private readonly IConfiguration _configuration;

        public Sign(
            ILogger<Sign> logger
            , ISignApi signApi
            , IConfiguration configuration
            )
        {
            _logger = logger;
            _signApi = signApi;
            _configuration = configuration;
        }

        /// <summary>
        /// 每日签到
        /// </summary>
        [TaskLogInterceptor("每日签到")]
        public void DoSignTask()
        {
            LiWoResponse<SignResponse> response = _signApi.DoSign(new SignRequest(), new SignBodyAto())
                .GetAwaiter().GetResult();
            LogSignResponse(response);
            if (response.Status) return;

            List<string> reSignStatus = _configuration.GetSection("Tasks:Sign:NeedResetSignStatus").Get<List<string>>();
            if (reSignStatus.Contains(response.Error.Code))
            {
                ReSign();
            }
        }

        /// <summary>
        /// 重新签到
        /// </summary>
        private void ReSign()
        {
            _logger.LogInformation("开始重新签到");

            LiWoResponse<SignResponse> resignResult = _signApi.ResetSign(new ResetSignRequest(), new SignBodyAto())
                .GetAwaiter().GetResult();

            LogSignResponse(resignResult);
        }

        /// <summary>
        /// 日志记录签到返回内容
        /// </summary>
        /// <param name="response"></param>
        private void LogSignResponse(LiWoResponse<SignResponse> response)
        {
            if (response.Status)
                _logger.LogInformation("{title}，{msg}", response.Data?.Title, response.Data?.Message);

            _logger.LogError("{msg}", response.Error?.Message);
        }
    }
}
