using Chet.WebApi.Template.GUI.Domain.Exceptions;
using Chet.WebApi.Template.GUI.Domain.Replaces.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Chet.WebApi.Template.GUI.Domain.Replaces
{
    /// <summary>
    /// 替换管理器
    /// <para>负责替换模板中的公司名称和项目名称，包括文件夹、文件名和文件内容</para>
    /// </summary>
    public class ReplaceManager
    {
        /// <summary>
        /// 替换模板
        /// <para>执行模板替换的主方法，包括重命名文件夹和替换文件内容</para>
        /// </summary>
        /// <param name="sourcePath">源模板路径</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="projectName">项目名称</param>
        /// <exception cref="GenerateTemplateException">生成模板失败时抛出</exception>
        public void ReplaceTemplates(string sourcePath, string companyName, string projectName)
        {
            try
            {
                // 执行模板重命名和替换
                RenameTemplate(sourcePath, companyName, projectName);
            }
            catch (Exception ex)
            {
                throw new GenerateTemplateException($"{DateTime.Now.ToString()}生成模板失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 重命名模板
        /// <para>重命名所有文件夹和文件，并替换文件内容</para>
        /// </summary>
        /// <param name="sourcePath">源模板路径</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="projectName">项目名称</param>
        private void RenameTemplate(string sourcePath, string companyName, string projectName)
        {
            // 重命名所有文件夹
            RenameAllDirectories(sourcePath, companyName, projectName);

            // 重命名所有文件名并替换文件内容
            RenameAllFileNameAndContent(sourcePath, companyName, projectName);
        }

        /// <summary>
        /// 重命名所有目录
        /// <para>递归遍历并重命名所有包含旧公司名或项目名的目录</para>
        /// </summary>
        /// <param name="sourcePath">源模板路径</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="projectName">项目名称</param>
        private void RenameAllDirectories(string sourcePath, string companyName, string projectName)
        {
            // 获取所有子目录
            var directories = Directory.GetDirectories(sourcePath);
            
            // 递归处理每个子目录
            foreach (var subDirectory in directories)
            {
                RenameAllDirectories(subDirectory, companyName, projectName);

                // 获取目录信息
                var directoryInfo = new DirectoryInfo(subDirectory);
                
                // 检查目录名是否包含旧公司名或旧项目名（不区分大小写）
                if (directoryInfo.Name.Contains(ReplaceConsts.OldCompanyName, StringComparison.OrdinalIgnoreCase) ||
                    directoryInfo.Name.Contains(ReplaceConsts.OldProjectName, StringComparison.OrdinalIgnoreCase))
                {
                    // 生成新目录名
                    var oldDirectoryName = directoryInfo.Name;
                    var newDirectoryName = oldDirectoryName.CustomReplace(companyName, projectName);

                    // 构建新目录路径
                    var newDirectoryPath = Path.Combine(directoryInfo.Parent?.FullName, newDirectoryName);

                    // 只有当目录名发生变化时才重命名
                    if (directoryInfo.FullName != newDirectoryPath)
                    {
                        directoryInfo.MoveTo(newDirectoryPath);
                    }
                }
            }
        }

        /// <summary>
        /// 重命名所有文件名并替换内容
        /// <para>递归遍历并处理所有文件：重命名文件名，替换文件内容</para>
        /// </summary>
        /// <param name="sourcePath">源模板路径</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="projectName">项目名称</param>
        private void RenameAllFileNameAndContent(string sourcePath, string companyName, string projectName)
        {
            // 获取所有需要处理的文件
            var fileFilters = ReplaceConsts.FileFilter.Split(',')
                .Select(filter => filter.Trim())
                .ToList();

            // 使用UTF-8编码（无BOM）处理文件
            var encoding = new UTF8Encoding(false);

            // 处理当前目录的文件
            var directoryInfo = new DirectoryInfo(sourcePath);
            var files = directoryInfo.GetFiles();

            foreach (var fileInfo in files)
            {
                // 检查文件是否需要处理
                if (fileFilters.Contains(fileInfo.Extension, StringComparer.OrdinalIgnoreCase) ||
                    fileFilters.Contains($".{fileInfo.Name}", StringComparer.OrdinalIgnoreCase))
                {
                    try
                    {
                        // 读取并替换文件内容
                        var oldContents = File.ReadAllText(fileInfo.FullName, encoding);
                        var newContents = oldContents.CustomReplace(companyName, projectName);

                        // 检查文件名是否需要替换
                        if (fileInfo.Name.Contains(ReplaceConsts.OldCompanyName, StringComparison.OrdinalIgnoreCase) ||
                            fileInfo.Name.Contains(ReplaceConsts.OldProjectName, StringComparison.OrdinalIgnoreCase))
                        {
                            // 生成新文件名
                            var oldFileName = fileInfo.Name;
                            var newFileName = oldFileName.CustomReplace(companyName, projectName);

                            // 构建新文件路径
                            var newFilePath = Path.Combine(fileInfo.DirectoryName, newFileName);

                            // 只有当文件名发生变化时才删除旧文件
                            if (newFilePath != fileInfo.FullName)
                            {
                                File.Delete(fileInfo.FullName);
                            }

                            // 写入新内容
                            File.WriteAllText(newFilePath, newContents, encoding);
                        }
                        else
                        {
                            // 文件名无需替换，直接写入新内容
                            File.WriteAllText(fileInfo.FullName, newContents, encoding);
                        }
                    }
                    catch (Exception ex)
                    {
                        // 记录错误信息，继续处理其他文件
                        Console.WriteLine($"处理文件 {fileInfo.FullName} 时发生错误: {ex.Message}");
                    }
                }
            }

            // 递归处理子目录
            var subDirectories = Directory.GetDirectories(sourcePath);
            foreach (var subDirectory in subDirectories)
            {
                RenameAllFileNameAndContent(subDirectory, companyName, projectName);
            }
        }
    }
}
