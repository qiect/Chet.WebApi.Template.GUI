namespace Chet.WebApi.Template.GUI.Domain.Replaces
{
    /// <summary>
    /// 替换常量类
    /// <para>定义模板替换过程中使用的常量值</para>
    /// </summary>
    public class ReplaceConsts
    {
        /// <summary>
        /// 旧公司名称
        /// <para>模板中需要替换的旧公司名称</para>
        /// </summary>
        public const string OldCompanyName = "Chet";

        /// <summary>
        /// 旧项目名称
        /// <para>模板中需要替换的旧项目名称</para>
        /// </summary>
        public const string OldProjectName = "WebApi.Template";

        /// <summary>
        /// 文件过滤器
        /// <para>需要进行替换处理的文件后缀列表，使用逗号分隔</para>
        /// </summary>
        public const string FileFilter = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env";
    }
}
