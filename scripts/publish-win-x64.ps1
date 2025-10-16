param(
    [string]$Configuration = "Release",
    [string]$Runtime = "win-x64"
)

$projectPath = Join-Path $PSScriptRoot "../src/XiangtanPlanner/XiangtanPlanner.csproj"

if (-not (Test-Path $projectPath)) {
    throw "未找到项目文件: $projectPath"
}

Write-Host "开始发布 XiangtanPlanner ($Configuration|$Runtime)..." -ForegroundColor Cyan

dotnet publish $projectPath -c $Configuration -r $Runtime --self-contained true /p:PublishSingleFile=true

Write-Host "发布完成。可执行文件位于 src/XiangtanPlanner/bin/$Configuration/net7.0-windows/$Runtime/publish" -ForegroundColor Green
