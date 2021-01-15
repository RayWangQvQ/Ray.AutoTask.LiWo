using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace Ray.AutoTask.LiWo.Domain
{
    [Header("Origin", "https://2do.jd.com")]
    [Header("Sec-Fetch-Mode", "cors")]
    [Header("Sec-Fetch-Site", "same-site")]
    [Header("Host", "api.m.jd.com")]
    [Header("Connection", "keep-alive")]
    [Header("Cache-Control", "no-cache")]
    [Header("Pragma", "no-cache")]
    [Header("Accept", "application/json, text/plain, */*")]
    [Header("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7")]
    [Header("X-Requested-With", "com.jd.campus")]
    [Header("User-Agent", "Mozilla/5.0 (Linux; Android 10; V1824BA Build/QP1A.190711.020; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/78.0.3904.96 Mobile Safari/537.36 yocial/5.3.3(Android;10.0.0;vivoV1824BA;com.jd.campus)")]
    public interface ILiWoApi : IHttpApi
    {
    }
}
