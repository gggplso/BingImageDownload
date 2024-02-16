@echo off
@echo=
@echo  **************************************
@echo  *                                    *
@echo  *      安装BingImageAutoDownload   *
@echo  *                                    *
@echo  **************************************
@echo=

%1 start "" mshta vbscript:createobject("shell.application").shellexecute("""%~0""","::",,"runas",1)(window.close)&exit
cd /d %~dp0

if exist "%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe" (
    %SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe "WindowsServiceBingImageAutoDownload.exe"
    if %errorlevel% neq 0 (
        echo 安装 WindowsServiceBingImageAutoDownload 服务时出错，请查看相关日志。
        pause
        exit /B %errorlevel%
    )
    sc config BingImageAutoDownload start= AUTO
    net Start BingImageAutoDownload
    
    echo WindowsServiceBingImageAutoDownload 服务已成功安装并启动。
) else (
    echo 检测到系统中未安装 .NET Framework 4.0 或者 InstallUtil.exe 文件不存在，请确保正确安装 .NET Framework 4.0 后再运行此脚本。
    pause
    exit /B 1
)

@timeout /T 10 /NOBREAK
::@pause