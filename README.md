# Chet.WebApi.Template.GUI

Chet.WebApi.Template.GUI 是一个基于 .NET 8 的图形用户界面工具，用于可视化创建和配置 Chet.WebApi.Template 项目。该工具旨在帮助开发者通过直观的界面快速生成符合团队规范的 Web API 项目模板，减少重复配置和入门成本。

主要功能

- 可视化配置项目选项（中间件、认证、数据库、日志等）
- 生成基于 Chet.WebApi.Template 的工程骨架
- 支持导出/导入配置模板（便于团队共享）
- 内置示例与向导，帮助快速上手

先决条件

- .NET 8 SDK
- Windows / macOS / Linux（取决于 GUI 框架支持的平台）

快速开始

1. 克隆仓库

   git clone https://github.com/qiect/Chet.WebApi.Template.GUI.git
   cd Chet.WebApi.Template.GUI

2. 构建并运行

   - 在项目根目录或 src 子目录中运行：
     dotnet build
     dotnet run --project ./src/<YourGuiProject>.csproj

   - 或使用 IDE（Visual Studio / Rider / VS Code）打开解决方案文件 Chet.WebApi.Template.GUI.sln 并直接运行。

使用示例

- 启动应用后按向导引导填写项目名称、命名空间、需要的中间件（如 Swagger、Cors）、目标数据库类型（如 SQLite/PostgreSQL/MSSQL）等。
- 配置完成后点击“生成”按钮，工具会输出一个可直接打开/编译的 Web API 模板工程到指定目录。
- 可选择将当前配置导出为模板文件（JSON），以便团队复用。

项目结构（概览）

- src/ — 源代码
- examples/ — 示例与演示配置（若存在）
- docs/ — 文档（若存在）
- Logs/ — 运行时生成的日志（默认忽略在版本库中）

贡献

非常欢迎贡献：

- 提交 Issue：报告 bug 或提出功能建议
- 提交 PR：改善功能、修复 Bug 或补充文档
- 提交模板：如果你有实用的项目配置模板，欢迎提交或在仓库中添加示例

在贡献前，请先在 Issue 中讨论较大或破坏性变更。

许可证

本仓库未在 README 中指定许可证。如果你希望使用明确的许可证（例如 MIT），可以在仓库中添加 LICENSE 文件。我可以为你生成并提交合适的 LICENSE 文件（例如 MIT、Apache-2.0 等）。

更多帮助

如果你希望我：

- 基于源码自动生成更精确的 README（包含各窗口、菜单、主要类与方法说明），请授权我读取并分析 src 目录的具体文件；
- 添加示例配置、示例输出工程或 CI（GitHub Actions）来自动构建并运行基本测试，我也可以继续为你实现。

---

(本 README 为初始改进版，基于仓库描述与目录结构所生成。如需我将此 README 直接提交到仓库的 main 分支，请回复确认，我将把文件写入 main 分支。)