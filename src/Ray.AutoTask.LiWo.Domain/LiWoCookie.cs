using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Ray.Infrastructure;

namespace Ray.AutoTask.LiWo.Domain
{
    public class LiWoCookie : CookieInfo
    {
        public LiWoCookie(string cookieStr) : base(cookieStr)
        {
        }

        public LiWoCookie(IConfiguration configuration) : this(configuration["LiWoCookie"])
        {

        }
    }
}
