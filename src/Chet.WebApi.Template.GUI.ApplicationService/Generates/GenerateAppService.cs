using Chet.WebApi.Template.GUI.Domain.Githubs;
using Chet.WebApi.Template.GUI.Domain.Replaces;
using Chet.WebApi.Template.GUI.Domain.Zips;
using System.Threading.Tasks;

namespace Chet.WebApi.Template.GUI.ApplicationService.Generates
{
    /// <summary>
    /// 生成应用服务
    /// <para>负责协调领域层服务，完成模板下载、解压和替换的完整流程</para>
    /// </summary>
    public class GenerateAppService
    {
        /// <summary>
        /// GitHub管理器
        /// <para>用于从GitHub下载模板代码</para>
        /// </summary>
        private readonly GithubManager _githubManager;

        /// <summary>
        /// ZIP管理器
        /// <para>用于解压下载的ZIP文件</para>
        /// </summary>
        private readonly ZipManager _zipManager;

        /// <summary>
        /// 替换管理器
        /// <para>用于替换模板中的公司名和项目名</para>
        /// </summary>
        private readonly ReplaceManager _replaceManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="githubManager">GitHub管理器</param>
        /// <param name="zipManager">ZIP管理器</param>
        /// <param name="replaceManager">替换管理器</param>
        public GenerateAppService(GithubManager githubManager, ZipManager zipManager, ReplaceManager replaceManager)
        {
            _githubManager = githubManager;
            _zipManager = zipManager;
            _replaceManager = replaceManager;
        }

        /// <summary>
        /// 下载源码
        /// <para>从GitHub下载指定模板的最新发布版本</para>
        /// </summary>
        /// <param name="type">模板类型</param>
        /// <returns>下载的ZIP文件路径</returns>
        public async Task<string> DownloadSourceAsync(string type)
        {
            return await _githubManager.GetSourceCodeAsync(type);
        }

        /// <summary>
        /// 解压ZIP文件
        /// <para>将下载的ZIP文件解压到指定目录</para>
        /// </summary>
        /// <param name="path">ZIP文件路径</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="projectName">项目名称</param>
        /// <returns>解压后的目录路径</returns>
        public string ExtractZips(string path, string companyName, string projectName)
        {
            return _zipManager.ExtractZips(path, companyName, projectName);
        }

        /// <summary>
        /// 生成模板
        /// <para>替换模板中的公司名和项目名，生成定制化模板</para>
        /// </summary>
        /// <param name="path">解压后的模板路径</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="projectName">项目名称</param>
        public void GenerateTemplate(string path, string companyName, string projectName)
        {
            _replaceManager.ReplaceTemplates(path, companyName, projectName);
        }
    }
}
