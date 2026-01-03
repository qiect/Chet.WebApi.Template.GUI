using Chet.WebApi.Template.GUI.ApplicationService.Generates;
using Chet.WebApi.Template.GUI.Domain.Githubs;
using Chet.WebApi.Template.GUI.Domain.Replaces;
using Chet.WebApi.Template.GUI.Domain.Zips;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Windows;

namespace Chet.WebApi.Template.GUI
{
    /// <summary>
    /// 应用程序类
    /// <para>WPF应用程序的入口点，负责初始化应用程序和管理生命周期</para>
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 主机服务
        /// <para>用于管理应用程序的依赖注入和生命周期</para>
        /// </summary>
        private readonly IHost _host;

        /// <summary>
        /// 构造函数
        /// <para>初始化应用程序主机和依赖注入容器</para>
        /// </summary>
        public App()
        {
            // 配置Serilog日志
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()       // 调试模式下使用Debug级别
#else
                .MinimumLevel.Information() // 发布模式下使用Information级别
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information) // 覆盖Microsoft组件的日志级别
                .Enrich.FromLogContext()    // 从日志上下文中获取丰富信息
                .WriteTo.Async(c => c.File("Logs/logs.txt")) // 异步写入日志文件
                .CreateLogger();

            // 创建并配置主机
            _host = Host
                .CreateDefaultBuilder(null)
                .UseSerilog()          // 使用Serilog作为日志框架
                .ConfigureServices((hostContext, services) =>
                {
                    // 注册服务
                    RegisterServices(services);
                }).Build();
        }

        /// <summary>
        /// 注册服务
        /// <para>配置应用程序的依赖注入服务</para>
        /// </summary>
        /// <param name="services">服务集合</param>
        private void RegisterServices(IServiceCollection services)
        {
            // 注册域服务
            services.AddSingleton<GithubManager>();
            services.AddSingleton<ZipManager>();
            services.AddSingleton<ReplaceManager>();

            // 注册应用服务
            services.AddTransient<GenerateAppService>();

            // 注册WPF窗口
            services.AddSingleton<MainWindow>();
        }

        /// <summary>
        /// 应用程序启动事件处理
        /// <para>执行应用程序的初始化和启动逻辑</para>
        /// </summary>
        /// <param name="e">启动事件参数</param>
        protected async override void OnStartup(StartupEventArgs e)
        {
            try
            {
                Log.Information("Starting WPF host.");

                // 启动主机
                await _host.StartAsync();

                // 显示主窗口
                _host.Services.GetService<MainWindow>()?.Show();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                MessageBox.Show($"应用程序启动失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Shutdown();
            }
        }

        /// <summary>
        /// 应用程序退出事件处理
        /// <para>执行应用程序的清理和关闭逻辑</para>
        /// </summary>
        /// <param name="e">退出事件参数</param>
        protected async override void OnExit(ExitEventArgs e)
        {
            try
            {
                // 停止主机
                await _host.StopAsync();
            }
            finally
            {
                // 释放主机资源
                _host.Dispose();
                // 关闭并刷新日志
                Log.CloseAndFlush();
            }
        }
    }
}
