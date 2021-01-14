using System;
using System.Collections.Generic;
using System.Text;
using Ray.Infrastructure.Attributes;

namespace Ray.AutoTask.LiWo.Domain.SignDomain
{
    public class Sign
    {
        private readonly ISignApi _signApi;
        private readonly IResignApi _resignApi;

        public Sign(
            ISignApi signApi
            //, IResignApi resignApi
            )
        {
            _signApi = signApi;
            //_resignApi = resignApi;
        }

        /// <summary>
        /// 每日签到
        /// </summary>
        [TaskLogInterceptor("每日签到")]
        public void DoSignTask()
        {
            LiWoResponse<SignResponse> response = _signApi.DoSign(new SignRequest(), new SignBodyAto())
                .GetAwaiter().GetResult();

            //todo:resign
        }
    }
}
