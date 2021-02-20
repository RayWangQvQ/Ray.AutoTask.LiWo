using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Ray.AutoTask.LiWo.Domain.SignDomain;
using Ray.Infrastructure;
using Ray.Infrastructure.Extensions;
using Xunit;

namespace Ray.AutoTask.LiWo.Test
{
    public class SignTest
    {
        public SignTest()
        {
            Environment.SetEnvironmentVariable("Cookie", "");
            Program.Init(null);
        }

        [Fact]
        public void TestSign()
        {
            using var scope = Global.ServiceProviderRoot.CreateScope();

            var api = scope.ServiceProvider.GetRequiredService<ISignApi>();

            var result = api.DoSign(new SignRequest(), new SignBodyAto()).GetAwaiter().GetResult();

            Debug.WriteLine(result.ToJson());
        }

        [Fact]
        public void TestResetSign()
        {
            using var scope = Global.ServiceProviderRoot.CreateScope();

            var api = scope.ServiceProvider.GetRequiredService<ISignApi>();

            var result = api.ResetSign(new ResetSignRequest(), new SignBodyAto()).GetAwaiter().GetResult();

            Debug.WriteLine(result.ToJson());
        }
    }
}
