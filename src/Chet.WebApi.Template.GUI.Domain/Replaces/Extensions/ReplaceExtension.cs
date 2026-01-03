using System;

namespace Chet.WebApi.Template.GUI.Domain.Replaces.Extensions
{
    /// <summary>
    /// 替换扩展类
    /// <para>提供字符串替换的扩展方法</para>
    /// </summary>
    public static class ReplaceExtension
    {
        /// <summary>
        /// 自定义替换
        /// <para>替换字符串中的旧公司名和旧项目名为新的公司名和项目名</para>
        /// </summary>
        /// <param name="content">要替换的内容</param>
        /// <param name="companyName">新公司名称</param>
        /// <param name="projectName">新项目名称</param>
        /// <returns>替换后的字符串</returns>
        public static string CustomReplace(this string content, string companyName, string projectName)
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }
            
            // 使用正则表达式实现不区分大小写的替换，更可靠
            var result = content
                    .Replace(ReplaceConsts.OldCompanyName, companyName, StringComparison.OrdinalIgnoreCase)
                    .Replace(ReplaceConsts.OldProjectName, projectName, StringComparison.OrdinalIgnoreCase)
                ;

            return result;
        }
    }
}
