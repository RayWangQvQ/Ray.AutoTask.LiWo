using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Ray.Infrastructure;
using Ray.Infrastructure.Extensions;

namespace Ray.AutoTask.LiWo.Domain
{
    public class LiWoCookie : CookieInfo
    {
        public LiWoCookie(string cookieStr) : base(cookieStr)
        {
            this.CookieDictionary.TryGetValue(this.Jdc.Description(), out string jdc);
            this.Jdc = jdc;

            this.CookieDictionary.TryGetValue(this.PtPin.Description(), out string ptPin);
            this.PtPin = ptPin;

            this.CookieDictionary.TryGetValue(this.PtKey.Description(), out string ptKey);
            this.PtKey = ptKey;
        }

        public LiWoCookie(IConfiguration configuration) : this(configuration["LiWoCookie"])
        {

        }

        [Description("__jdc")]
        public string Jdc { get; set; }

        [Description("pt_pin")]
        public string PtPin { get; set; }

        [Description("pt_key")]
        public string PtKey { get; set; }
    }
}
