using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.AutoTask.LiWo.Domain.LikeDomain
{
    public class Like
    {
        private readonly IVideoApi _videoApi;

        public Like(IVideoApi videoApi)
        {
            _videoApi = videoApi;
        }

        public void Create()
        {
            _videoApi.GetVideoList(new GetVideoListRequest(), new GetVideoBodyAto());

            /*
             * sign签名算不出来，放弃了
             */
        }
    }
}
