using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ray.AutoTask.Infrastructure.Options;
using Ray.AutoTask.LiWo.Domain.Extensions;
using Ray.AutoTask.LiWo.Domain.SignDomain;
using Ray.Infrastructure;
using Ray.Infrastructure.Config;
using Serilog;

namespace Ray.AutoTask.LiWo
{
    public class Program
    {
        static void Main(string[] args)
        {
            Init(args);

            LogAppInfo();

            StartRun();

            if (Global.ConfigurationRoot["CloseConsoleWhenEnd"] == "1") return;
            System.Console.ReadLine();
        }

        /// <summary>
        /// 初始化系统
        /// </summary>
        /// <param name="args"></param>
        public static void Init(string[] args)
        {
            IHostBuilder hostBuilder = new HostBuilder();

            //承载系统自身的配置：
            hostBuilder.ConfigureHostConfiguration(hostConfigurationBuilder =>
            {
                hostConfigurationBuilder.AddJsonFile("commandLineMappings.json", false, false);

                Environment.SetEnvironmentVariable(HostDefaults.EnvironmentKey, Environment.GetEnvironmentVariable(Global.EnvironmentKey));
                hostConfigurationBuilder.AddEnvironmentVariables();
            });

            //应用配置:
            hostBuilder.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
            {
                Global.HostingEnvironment = hostBuilderContext.HostingEnvironment;
                configurationBuilder
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile($"appsettings.{hostBuilderContext.HostingEnvironment.EnvironmentName}.json", true, true);
                if (hostBuilderContext.HostingEnvironment.IsDevelopment())
                {
                    Assembly assembly = Assembly.Load(new AssemblyName(hostBuilderContext.HostingEnvironment.ApplicationName));
                    if (assembly != null)
                        configurationBuilder.AddUserSecrets(assembly, true);
                }
                configurationBuilder.AddExcludeEmptyEnvironmentVariables("Ray_");
                if (args != null && args.Length > 0)
                {
                    configurationBuilder.AddCommandLine(args, hostBuilderContext.Configuration
                        .GetSection("CommandLineMappings")
                        .Get<Dictionary<string, string>>());
                }
            });

            //日志:
            hostBuilder.ConfigureLogging((hostBuilderContext, loggingBuilder) =>
            {
                Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(hostBuilderContext.Configuration)
                .CreateLogger();
            }).UseSerilog();

            //DI容器:
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                Global.ConfigurationRoot = (IConfigurationRoot)hostContext.Configuration;

                services.AddOptions()
                    .Configure<SecurityOptions>(Global.ConfigurationRoot.GetSection("Security"));

                services.AddAgentApis(hostContext.Configuration);
            });

            IHost host = hostBuilder.UseConsoleLifetime().Build();

            Global.ServiceProviderRoot = host.Services;
        }

        /// <summary>
        /// 打印应用信息
        /// </summary>
        private static void LogAppInfo()
        {
            using var scope = Global.ServiceProviderRoot.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogInformation(
                "版本号：Ray.AutoTask.LiWo-v{version}",
                typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "未知");
            logger.LogInformation("当前环境：{env}", Global.HostingEnvironment.EnvironmentName ?? "无");
            logger.LogInformation("开源地址：{url} \r\n", "***");
        }

        /// <summary>
        /// 开始运行
        /// </summary>
        private static void StartRun()
        {
            using var scope = Global.ServiceProviderRoot.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            var securityOptions = scope.ServiceProvider.GetRequiredService<IOptionsMonitor<SecurityOptions>>().CurrentValue;

            if (securityOptions.IsSkipRun)
            {
                logger.LogInformation("已配置为跳过任务");
                logger.LogInformation("开始推送");
                return;
            }

            int randomMin = new Random().Next(0, securityOptions.RandomSleepMaxMin);
            if (randomMin > 0)
            {
                logger.LogInformation("随机休眠{min}分钟 \r\n", randomMin);
                Thread.Sleep(randomMin * 1000 * 60);
            }

            try
            {
                var sign = scope.ServiceProvider.GetRequiredService<Sign>();
                sign.DoSignTask();
            }
            catch (Exception e)
            {
                logger.LogError("{ex}", e);
                throw;
            }
            finally
            {
                logger.LogInformation("开始推送");
            }
        }
    }
}
