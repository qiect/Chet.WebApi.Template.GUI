# Chet.WebApi.Template.GUI

## 项目简介
Chet.WebApi.Template.GUI是一个基于ABP框架和WPF技术开发的图形用户界面工具，用于从GitHub下载模板代码，解压并替换其中的公司名和项目名，生成定制化的项目模板。

## 技术栈
- **框架**：ABP VNext
- **UI技术**：WPF (.NET 5.0)
- **依赖注入**：Autofac
- **日志**：Serilog
- **HTTP客户端**：Octokit (GitHub API)
- **压缩处理**：System.IO.Compression

## 项目结构
```
Chet.WebApi.Template.GUI
├── src
│   ├── Chet.WebApi.Template.GUI                # GUI层（WPF应用）
│   ├── Chet.WebApi.Template.GUI.ApplicationService  # 应用服务层
│   └── Chet.WebApi.Template.GUI.Domain         # 领域层
├── Chet.WebApi.Template.GUI.sln                # 解决方案文件
├── common.props                         # 共享项目属性
└── README.md                            # 项目说明文档
```

## 核心功能

### 1. 模板下载
从GitHub下载指定模板的最新发布版本。

### 2. 模板解压
将下载的ZIP文件解压到指定目录。

### 3. 模板定制
- 替换模板中的公司名称
- 替换模板中的项目名称
- 重命名文件夹和文件
- 更新文件内容中的命名空间和引用

## 使用指南

### 1. 运行应用程序
直接运行`Chet.WebApi.Template.GUI.exe`可执行文件。

### 2. 配置模板信息
- **Source**：选择要使用的模板源（默认：Chet.Template）
- **CompanyName**：输入您的公司名称
- **ProjectName**：输入您的项目名称

### 3. 生成模板
点击"生成"按钮，系统将自动执行以下操作：
1. 下载模板代码
2. 解压模板文件
3. 替换公司名和项目名
4. 生成定制化模板

### 4. 查看结果
生成完成后，系统将自动打开生成的模板目录，您可以在其中找到定制化的项目模板。

## 配置说明

### appsettings.json
```json
{
  "Github": {
    "Chet.Template": {
      "Author": "作者名称",
      "RepsotiryName": "仓库名称"
    }
  }
}
```

## 扩展开发

### 添加新的模板源
1. 在`appsettings.json`中添加新的GitHub仓库配置
2. 修改相关代码以支持新的模板源

### 自定义替换规则
1. 修改`ReplaceConsts.cs`中的常量定义
2. 更新`ReplaceExtension.cs`中的替换逻辑

## 故障排除

### 常见问题
1. **下载失败**：检查网络连接和GitHub仓库配置
2. **解压失败**：检查文件权限和磁盘空间
3. **替换失败**：检查公司名和项目名的输入格式

### 日志查看
日志文件位于应用程序目录下的`Logs/logs.txt`，可以查看详细的操作记录和错误信息。

## 许可证
本项目采用MIT许可证，详情请查看LICENSE文件。
