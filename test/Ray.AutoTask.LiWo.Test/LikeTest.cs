using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Ray.AutoTask.LiWo.Domain.LikeDomain;
using Ray.AutoTask.LiWo.Domain.SignDomain;
using Ray.Infrastructure;
using Ray.Infrastructure.Extensions;
using Xunit;

namespace Ray.AutoTask.LiWo.Test
{
    public class LikeTest
    {
        public LikeTest()
        {
            Program.Init(null);
        }

        [Fact]
        public void TestGetVideoList()
        {
            using var scope = Global.ServiceProviderRoot.CreateScope();

            var api = scope.ServiceProvider.GetRequiredService<IVideoApi>();

            var result = api.GetVideoList(new GetVideoListRequest(), new GetVideoBodyAto()).GetAwaiter().GetResult();

            Debug.WriteLine(result.ToJson());
        }
    }
}
