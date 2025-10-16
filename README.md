# XMTJ 同成规划桌面应用

该版本使用 C# (.NET 7 WPF) 重新实现同成规划应用，实现数据自动加载、实时保存以及一键发布 Windows 可执行文件的能力。

## 主要特性

- **仪表盘式界面**：左侧展示项目日历、月度推进、提醒事项与工作小结，右侧覆盖工作看板、时间轴、任务清单和复盘分析等模块。
- **自动加载与实时保存**：应用启动时自动从默认数据目录读取 `planner.json`，任何界面上的变更都会在后台延迟写入，保证数据安全。
- **默认存储目录**：所有数据默认存放在 `我的文档/湘潭/data/planner.json`，符合“湘潭目录下的 data”要求。
- **自定义扩展**：支持添加任务、提醒、时间轴节点、复盘与看板卡片，布局及基础配色均可在 XAML 中快速调整。

## 项目结构

```
XiangtanPlanner.sln                解决方案入口
src/
  XiangtanPlanner/
    App.xaml                      应用入口与全局样式
    Models/                       数据模型定义
    Services/                     数据持久化与自动保存逻辑
    ViewModels/                   视图模型与命令
    Views/MainWindow.xaml         主界面布局
    Utilities/                    MVVM 辅助类
scripts/
  publish-win-x64.ps1             打包发布脚本（见下）
```

## 运行与调试

1. 安装 [.NET 7 SDK](https://dotnet.microsoft.com/zh-cn/download/dotnet/7.0)。
2. 在仓库根目录执行：

   ```bash
   dotnet restore
   dotnet build
   dotnet run --project src/XiangtanPlanner/XiangtanPlanner.csproj
   ```

   首次运行会在 `我的文档/湘潭/data` 下生成示例数据文件。

## 打包发布 exe

脚本 `scripts/publish-win-x64.ps1` 提供了一键打包命令：

```powershell
pwsh scripts/publish-win-x64.ps1
```

脚本内部调用：

```powershell
dotnet publish ../src/XiangtanPlanner/XiangtanPlanner.csproj -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true
```

发布结果位于 `src/XiangtanPlanner/bin/Release/net7.0-windows/win-x64/publish/`，可直接分发 `XiangtanPlanner.exe`。

## 数据文件

- 默认路径：`%USERPROFILE%/Documents/湘潭/data/planner.json`
- 数据格式：UTF-8 编码的 JSON。
- 可直接编辑 JSON 调整默认内容，应用启动会自动加载。

## 注意事项

- 项目面向 Windows 平台（使用 WPF），如果需要跨平台 UI，可考虑迁移至 Avalonia 等方案。
- 若需自定义颜色或字体，可在 `App.xaml` 中调整主题资源。
- 自动保存采用 1 秒延迟写入策略，避免频繁修改造成的磁盘压力。
