@echo off
@echo=
@echo  **************************************
@echo  *                                    *
@echo  *      ��װBingImageAutoDownload   *
@echo  *                                    *
@echo  **************************************
@echo=

%1 start "" mshta vbscript:createobject("shell.application").shellexecute("""%~0""","::",,"runas",1)(window.close)&exit
cd /d %~dp0

if exist "%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe" (
    %SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe "WindowsServiceBingImageAutoDownload.exe"
    if %errorlevel% neq 0 (
        echo ��װ WindowsServiceBingImageAutoDownload ����ʱ������鿴�����־��
        pause
        exit /B %errorlevel%
    )
    sc config BingImageAutoDownload start= AUTO
    net Start BingImageAutoDownload
    
    echo WindowsServiceBingImageAutoDownload �����ѳɹ���װ��������
) else (
    echo ��⵽ϵͳ��δ��װ .NET Framework 4.0 ���� InstallUtil.exe �ļ������ڣ���ȷ����ȷ��װ .NET Framework 4.0 �������д˽ű���
    pause
    exit /B 1
)

@timeout /T 10 /NOBREAK
::@pause