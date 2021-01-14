using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ray.Infrastructure.Extensions;

namespace Ray.Infrastructure
{
    public class CookieInfo
    {
        private readonly string _cookieStr;

        public CookieInfo(string cookieStr)
        {
            _cookieStr = cookieStr;

            CookieStrList = _cookieStr.Split(";")
                .Select(x => x.Trim())
                .Where(x => x.IsNotNullOrEmpty())
                .ToList();

            CookieDictionary = CookieStrList.ToDictionary(k => k.Split('=')[0], v => v.Split('=')[1]);
        }

        public List<string> CookieStrList { get; set; }

        public Dictionary<string, string> CookieDictionary { get; set; }

        public virtual CookieContainer CreateCookieContainer(Uri uri)
        {
            var cookieContainer = new CookieContainer();
            foreach (var item in CookieStrList)
            {
                cookieContainer.SetCookies(uri, item);
            }

            return cookieContainer;
        }
    }
}
