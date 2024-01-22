@echo off
@echo=
@echo  **************************************
@echo  *                                    *
@echo  *      °²×°£ºBingImageAutoDownload   *
@echo  *                                    *
@echo  **************************************
@echo=

%1 start "" mshta vbscript:createobject("shell.application").shellexecute("""%~0""","::",,"runas",1)(window.close)&exit
cd /d %~dp0
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe "WindowsServiceBingImageAutoDownload.exe"
sc config BingImageAutoDownload start= AUTO
net Start BingImageAutoDownload

@timeout /T 10 /NOBREAK
::@pause