using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Ray.Infrastructure;
using Ray.Infrastructure.Extensions;

namespace Ray.AutoTask.LiWo.Domain
{
    public class GetVideoCookie : LiWoCookie
    {
        public GetVideoCookie(string cookieStr) : base(cookieStr)
        {

        }

        public GetVideoCookie(IConfiguration configuration) : this(configuration["LiWoCookie"])
        {

        }

        public override CookieContainer CreateCookieContainer(Uri uri)
        {
            List<string> list = CookieStrList;

            list.Add($"pin={this.PtPin}");
            list.Add("wskey=AAJgAV1LAEBxaNCw8td1BWqfUSvbx0EP8ForyxA-Wv6Wvn_HJWtEuhmZ4f1e8ZTV-NP3KADFXFESu_wNSkOK3acK53xtnUP4");
            list.Add("uuid=00000000-55f0-cb88-0000-0000347f63bc");
            list.Add("channelId=vivo");

            var cookieContainer = new CookieContainer();
            foreach (var item in list)
            {
                cookieContainer.SetCookies(uri, item);
            }

            return cookieContainer;
        }
    }
}
