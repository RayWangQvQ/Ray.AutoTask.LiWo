using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Attributes;

namespace Ray.AutoTask.LiWo.Domain.SignDomain
{
    public class Sign
    {
        private readonly ILogger<Sign> _logger;
        private readonly ISignApi _signApi;

        public Sign(
            ILogger<Sign> logger
            , ISignApi signApi
            )
        {
            _logger = logger;
            _signApi = signApi;
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

            //开启新一轮签到
            if (!response.Status && response.Error.Code == "39004")
            {
                _logger.LogInformation("自动开启新一轮签到");

                _signApi.ResetSign(new ResetSignRequest(), new SignBodyAto())
                    .GetAwaiter().GetResult();

                LogSignResponse(response);
            }
        }

        private void LogSignResponse(LiWoResponse<SignResponse> response)
        {
            if (response.Status)
                _logger.LogInformation("{title}，{msg}", response.Data?.Title, response.Data?.Message);

            _logger.LogError("{msg}", response.Error?.Message);
        }
    }
}
