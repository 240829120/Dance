using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Common
{
    /// <summary>
    /// WebApi服务
    /// </summary>
    public class DanceWebApiServer : DanceObject
    {
        // ===========================================================================================
        // Field

        /// <summary>
        /// 执行线程
        /// </summary>
        private DanceThread? RunThread;

        /// <summary>
        /// Web应用程序
        /// </summary>
        private WebApplication? WebApp;

        // ===========================================================================================
        // Property

        /// <summary>
        /// 是否使用Swagger
        /// </summary>
        public bool IsUseSwagger { get; set; }

        /// <summary>
        /// 地址集合
        /// </summary>
        public List<string> Urls { get; private set; } = [];

        /// <summary>
        /// 控制器程序集
        /// </summary>
        public List<Assembly> Assemblies { get; private set; } = [];

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            if (this.WebApp != null)
                return;

            var builder = WebApplication.CreateBuilder([]);

            var controllerBuilder = builder.Services.AddControllers();
            foreach (Assembly assembly in this.Assemblies)
            {
                controllerBuilder = controllerBuilder.AddApplicationPart(assembly);
            }

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.WebHost.UseUrls([.. this.Urls]);

            this.WebApp = builder.Build();
            this.WebApp.UseCors("any");

            if (this.IsUseSwagger)
            {
                this.WebApp.UseSwagger();
                this.WebApp.UseSwaggerUI();
            }

            this.WebApp.UseAuthorization();
            this.WebApp.MapControllers();

            this.RunThread = new(context => { this.WebApp.Run(); });
            this.RunThread.Start();
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            this.WebApp?.DisposeAsync().AsTask().Wait(TimeSpan.FromSeconds(10));
            this.RunThread?.Dispose();

            this.WebApp = null;
            this.RunThread = null;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.Stop();
        }
    }
}
