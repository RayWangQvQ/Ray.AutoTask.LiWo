using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ray.AutoTask.Infrastructure.Attributes;
using WebApiClientCore.Attributes;

namespace Ray.AutoTask.LiWo.Domain.LikeDomain
{
    [HttpHost("https://api.m.jd.com/")]
    [Header("Accept-Encoding", "gzip")]
    [LogFilter]
    public interface IVideoApi : ILiWoApi
    {
        Task<object> GetVideoList([Required][FormContent] GetVideoListRequest request, [JsonFormField] GetVideoBodyAto body);
    }

    public class GetVideoListRequest : LiWoRequest
    {
        public GetVideoListRequest()
        {
            Appid = "yocial";
            LoginType = 4;
            FunctionId = "v3_square_feeds";
        }

        public string Client { get; set; } = "yocial_android";

        public string ClientVersion { get; set; } = "5.3.3";

        public string Build { get; set; } = "748";

        public string OsVersion { get; set; } = "10.0.0";

        public string NetworkType { get; set; } = "wifi";

        public string Uuid { get; set; } = "00000000-55f0-cb88-0000-0000347f63bc";

        public string Partner { get; set; } = "vivo";

        public string CityId { get; set; } = "904";

        public string Sign { get; set; } = "d6e0d3793536bb211c2bc707b27c28dafc8c0081e203b5ef4d4dd6ab287030a7";
    }

    public class GetVideoBodyAto
    {
        public int Size { get; set; } = 10;

        public int CurrentPage { get; set; } = 1;

        public string CityId { get; set; } = "904";

        public int Type { get; set; } = 2;
    }
}
