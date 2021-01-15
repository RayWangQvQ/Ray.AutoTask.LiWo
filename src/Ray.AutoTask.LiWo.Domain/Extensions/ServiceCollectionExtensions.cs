using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ray.AutoTask.Infrastructure.HttpClientDelegatingHandlers;
using Ray.AutoTask.LiWo.Domain.LikeDomain;
using Ray.AutoTask.LiWo.Domain.SignDomain;
using Ray.Infrastructure.Extensions;

namespace Ray.AutoTask.LiWo.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAgentApis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<LiWoCookie>();
            services.AddSingleton<GetVideoCookie>();

            services.Scan(scan =>
            {
                scan.FromAssemblyOf<IntervalDelegatingHandler>()
                    .AddClasses(c => c.AssignableTo<DelegatingHandler>())
                    .AsSelf()
                    .WithTransientLifetime();
            });

            services.AddSingleton<Sign>();

            string host = "https://api.m.jd.com/";
            var uri = new Uri(host);
            services
                .AddHttpApi<ISignApi>(o =>
                {
                    o.HttpHost = uri;
                    o.UseDefaultUserAgent = false;
                })
                .ConfigurePrimaryHttpMessageHandler(sp =>
                {
                    var handler = new HttpClientHandler
                    {
                        CookieContainer = sp.GetRequiredService<LiWoCookie>().CreateCookieContainer(uri)
                    };
                    return handler;
                })
                .AddHttpMessageHandler<IntervalDelegatingHandler>();

            services
                .AddHttpApi<IVideoApi>(o =>
                {
                    o.HttpHost = uri;
                    o.UseDefaultUserAgent = false;
                })
                .ConfigurePrimaryHttpMessageHandler(sp =>
                {
                    var handler = new HttpClientHandler
                    {
                        CookieContainer = sp.GetRequiredService<GetVideoCookie>().CreateCookieContainer(uri)
                    };
                    return handler;
                })
                .AddHttpMessageHandler<IntervalDelegatingHandler>();

            return services;
        }
    }
}
