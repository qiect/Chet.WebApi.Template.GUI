using Chet.WebApi.Template.GUI.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Octokit;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chet.WebApi.Template.GUI.Domain.Githubs
{
    /// <summary>
    /// GitHub管理器
    /// <para>负责从GitHub下载模板代码</para>
    /// </summary>
    public class GithubManager
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置服务</param>
        public GithubManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 获取源代码
        /// <para>从GitHub下载指定模板的最新发布版本</para>
        /// </summary>
        /// <param name="type">模板类型</param>
        /// <returns>下载的ZIP文件路径</returns>
        /// <exception cref="DownSourceCodeException">下载源码失败时抛出</exception>
        public async Task<string> GetSourceCodeAsync(string type)
        {
            try
            {
                // 从配置中获取GitHub仓库信息
                var repositoryName = _configuration.GetValue<string>("Github:Chet.WebApi.Template:RepsotiryName");
                var author = _configuration.GetValue<string>("Github:Chet.WebApi.Template:Author");

                // 获取最新发布信息
                var release = await GetLastReleaseInfoAsync(repositoryName, author);

                // 创建下载目录
                var path = Path.Combine(Directory.GetCurrentDirectory(), "source");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // 构建输出文件路径
                var outputFullPath = Path.Combine(path, $"{repositoryName}-{release.TagName}.zip");

                // 如果文件已存在，直接返回路径
                if (File.Exists(outputFullPath)) return outputFullPath;

                // 构建下载URL
                var uri = new Uri($"https://github.com/{author}/{repositoryName}/archive/refs/tags/{release.TagName}.zip");

                // 下载发布文件
                await DownloadReleaseAsync(uri, outputFullPath);

                return outputFullPath;
            }
            catch (Exception ex)
            {
                throw new DownSourceCodeException($"{DateTime.Now.ToString()}下载源码失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 获取最后一个Release信息
        /// </summary>
        /// <param name="repositoryName">仓库名称</param>
        /// <param name="author">作者名称</param>
        /// <returns>最新发布信息</returns>
        private async Task<Release> GetLastReleaseInfoAsync(string repositoryName, string author)
        {
            // 创建GitHub客户端
            var github = new GitHubClient(new ProductHeaderValue(repositoryName));

            // 获取最新发布
            return await github.Repository.Release.GetLatest(author, repositoryName);
        }

        /// <summary>
        /// 下载Release文件
        /// </summary>
        /// <param name="uri">下载URL</param>
        /// <param name="outputFullPath">输出文件路径</param>
        /// <returns>异步任务</returns>
        private async Task DownloadReleaseAsync(Uri uri, string outputFullPath)
        {
            using (var httpClient = new HttpClient())
            {
                // 发送GET请求下载文件
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                // 将响应内容写入文件
                using (FileStream fileStream = new FileStream(outputFullPath, System.IO.FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fileStream);
                }
            }
        }
    }
}
