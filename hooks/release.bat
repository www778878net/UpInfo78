@echo off
setlocal enabledelayedexpansion

echo 开始发布流程...

REM 获取当前目录下所有的 .csproj 文件
for %%F in (*.csproj) do (
    set "project_file=%%F"
    
    REM 获取当前版本号
    for /f "tokens=3 delims=<>" %%a in ('findstr "<Version>" "!project_file!"') do set "current_version=%%a"
    echo 当前版本: !current_version!

    REM 增加 patch 版本号
    for /f "tokens=1-3 delims=." %%a in ("!current_version!") do (
        set /a patch=%%c+1
        set "new_version=%%a.%%b.!patch!"
    )
    echo 新版本: !new_version!

    REM 更新项目文件中的版本号
    powershell -Command "(Get-Content '!project_file!') -replace '<Version>!current_version!</Version>', '<Version>!new_version!</Version>' | Set-Content '!project_file!'"

    REM 设置 Visual Studio 环境变量
    call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

    REM 编译项目
    echo 正在构建项目 !project_file! ...
    msbuild "!project_file!" /p:Configuration=Release /p:Platform=x64
    if !errorlevel! neq 0 (
        echo 构建失败
        exit /b 1
    )
    echo 构建成功

    REM 运行测试
    echo 运行测试...
    dotnet test "!project_file!"
    if !errorlevel! neq 0 (
        echo 测试失败
        exit /b 1
    )
    echo 所有测试通过

    echo 项目 !project_file! 的发布流程成功完成。新版本: !new_version!
    echo.
)

echo 所有项目的发布流程已完成。

endlocal