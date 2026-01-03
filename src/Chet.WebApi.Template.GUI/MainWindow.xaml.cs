using Chet.WebApi.Template.GUI.ApplicationService.Generates;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Chet.WebApi.Template.GUI
{
    /// <summary>
    /// 主窗口类
    /// <para>WPF应用程序的主窗口，负责处理用户交互和显示操作结果</para>
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 生成应用服务
        /// <para>用于执行模板下载、解压和替换操作</para>
        /// </summary>
        private readonly GenerateAppService _generateAppService;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="generateAppService">生成应用服务</param>
        public MainWindow(GenerateAppService generateAppService)
        {
            InitializeComponent();
            _generateAppService = generateAppService;
        }

        /// <summary>
        /// 内容渲染完成事件处理
        /// <para>当窗口内容渲染完成后执行初始化操作</para>
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnContentRendered(EventArgs e)
        {
            // 设置默认模板源
            var defaultSelected = "Chet.WebApi.Template";
            this.Source.Items.Add(defaultSelected);
            this.Source.SelectedItem = defaultSelected;
            // 初始化日志显示
            this.Logs.Text += $"{DateTime.Now.ToString()} 解决方案名称：CompanyName.ProjectName \r\n";
        }

        /// <summary>
        /// 生成按钮点击事件处理
        /// <para>执行模板生成的完整流程：验证输入、下载源码、解压文件、生成模板</para>
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Logs.Text += $"{DateTime.Now.ToString()} 代码已经生成在 {Directory.GetCurrentDirectory()} \r\n";
                if (string.IsNullOrWhiteSpace(this.CompanyName.Text))
                {
                    this.Logs.Text += $"{DateTime.Now.ToString()} 请输入CompanyName....... \r\n";

                    return;
                }
                if (string.IsNullOrWhiteSpace(this.ProjectName.Text))
                {
                    this.Logs.Text += $"{DateTime.Now.ToString()} 请输入ProjectName....... \r\n";
                    return;
                }


                this.Logs.Text += $"{DateTime.Now.ToString()} 开始下载 {this.Source.Text}....... \r\n";
                var sourcePath = await _generateAppService.DownloadSourceAsync(this.Source.Text);
                this.Logs.Text += $"{DateTime.Now.ToString()} Abp-Vnext-Pro下载完成. \r\n";

                this.Logs.Text += $"{DateTime.Now.ToString()} 开始解压 {this.Source.Text}....... \r\n";
                var zipPath = _generateAppService.ExtractZips(sourcePath, this.CompanyName.Text.Trim(), this.ProjectName.Text.Trim());
                this.Logs.Text += $"{DateTime.Now.ToString()} 解压 {this.Source.Text} 完成. \r\n";

                this.Logs.Text += $"{DateTime.Now.ToString()} 开始生成 {this.Source.Text} 模板....... \r\n";
                _generateAppService.GenerateTemplate(zipPath, this.CompanyName.Text.Trim(), this.ProjectName.Text.Trim());
                this.Logs.Text += $"{DateTime.Now.ToString()} {this.Source.Text} 模板生成成功. \r\n";
                this.Logs.Text += $"{DateTime.Now.ToString()} 代码已经生成在 {Directory.GetCurrentDirectory()}\\code下 \r\n";

                Process.Start("explorer.exe", $"{Directory.GetCurrentDirectory()}\\code");

            }
            catch (Exception ex)
            {
                this.Logs.Text += ex.Message;
            }
        }
    }
}
