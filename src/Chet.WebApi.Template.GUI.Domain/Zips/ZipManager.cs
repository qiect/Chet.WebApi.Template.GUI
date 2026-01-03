using Chet.WebApi.Template.GUI.Domain.Exceptions;
using System;
using System.IO;
using System.IO.Compression;

namespace Chet.WebApi.Template.GUI.Domain.Zips
{
    /// <summary>
    /// ZIP管理器
    /// <para>负责解压下载的ZIP文件到指定目录</para>
    /// </summary>
    public class ZipManager
    {
        /// <summary>
        /// 解压ZIP文件
        /// <para>将下载的模板ZIP文件解压到指定目录</para>
        /// </summary>
        /// <param name="sourceZipFullPath">源ZIP文件路径</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="projectName">项目名称</param>
        /// <returns>解压后的目录路径</returns>
        /// <exception cref="ZipException">解压失败时抛出</exception>
        public string ExtractZips(string sourceZipFullPath, string companyName, string projectName)
        {
            try
            {
                // 构建解压目标目录路径
                var path = Path.Combine(Directory.GetCurrentDirectory(), "code", companyName + projectName);

                // 创建目录（如果不存在）
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // 构建解压路径
                var decompressionPath = Path.Combine(path, Path.GetFileNameWithoutExtension(sourceZipFullPath));

                // 如果目录已存在，直接返回路径
                if (Directory.Exists(decompressionPath)) return decompressionPath;

                // 解压ZIP文件
                ZipFile.ExtractToDirectory(sourceZipFullPath, path);

                return decompressionPath;
            }
            catch (Exception ex)
            {
                throw new ZipException($"{DateTime.Now.ToString()}解压源码失败:{ex.Message}");
            }
        }
    }
}
