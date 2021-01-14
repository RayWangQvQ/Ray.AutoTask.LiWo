using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ray.AutoTask.LiWo.Domain.SignDomain;
using Ray.Infrastructure;
using Ray.Infrastructure.Extensions;
using Xunit;

namespace Ray.AutoTask.LiWo.Test
{
    public class LogExceptionTest
    {
        public LogExceptionTest()
        {
            Program.Init(null);
        }

        [Fact]
        public void TestSign()
        {
            using var scope = Global.ServiceProviderRoot.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<LogExceptionTest>>();
            string str = null;

            try
            {
                var l = str.Length;
            }
            catch (Exception e)
            {
                logger.LogError("{ex}", e);
            }
        }
    }
}
