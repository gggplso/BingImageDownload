@echo off
@echo=
@echo  **************************************
@echo  *                                    *
@echo  *      –∂‘ÿ£∫BingImageAutoDownload   *
@echo  *                                    *
@echo  **************************************
@echo=

%1 start "" mshta vbscript:createobject("shell.application").shellexecute("""%~0""","::",,"runas",1)(window.close)&exit
cd /d %~dp0
net stop BingImageAutoDownload
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u "WindowsServiceBingImageAutoDownload.exe"
sc delete BingImageAutoDownload

@timeout /T 10 /NOBREAK
::@pause