# BingImageDownload


Bing Image Download 必应每日壁纸下载   

【版权】仅限于壁纸使用。它们是受版权保护的图像，因此您不应将其用于其他目的，但可以将其用作桌面壁纸。  
本程序采用的是Bing官方公开的接口，是通过JSON数据提取下载路径，从Bing官网下载图片。不排除以后下载会因Bing的调整而失效。

## 配置文件说明
本程序为C#编写的Windows服务，通过加载Windows系统服务WindowsServiceBingImageAutoDownload.exe来定时运行下载图片，具体参数可以通过WinFormsAppSetting.exe来设定下载参数，具体如下：

【说明】  
详见`WinFormsAppSetting.exe`页面中的参数说明，包括了配置项后面的说明，以及鼠标悬浮提示。
![配置截图](https://gitee.com/gggplso/MarkdownPhotos/raw/master/Photos/BingImageDownloadServer/AppSetting02.png)

## 安装和卸载  
解压文件  
通过双击文件【安装.bat】来安装服务，前提是系统里有装`Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe`  
同样，若要卸载的话，可以双击文件【卸载.bat】来停止并删除服务  
默认的服务名称是：BingImageAutoDownload  
显示名称是：Bing Image Automatic Download Service  
本程序为绿色软件，提供源码在gitee和github上，正常卸载后，删除目录即可，无残留。  


## 辅助说明  
> 可以将下载文件直接配置到您电脑壁纸所在的目录中，每天它自动下载就好。  
 > * 具体下载的一些配置参数可以通过程序提供的设置文件`WinFormsAppSetting.exe`来配置  

【辅助】  
 > 若不想用Windows服务，也可以用另一个程序，通过命令控制台来下载图片，详见gitee或github我上传的另一个程序  


## 额外功能
【额外】  
添加了复制Windows聚焦图片到指定的目录。  
Win10系统之后，锁屏设置里有个功能是显示Window聚焦图片，系统会每天下载不同的图片到Windows目录中。因为系统目录藏的有点深，本功能只是将其中大于`50KB`的图片文件复制到你指定的目录，并不负责下载更新，一切以您的系统设置为基准。  
前提是准确设置系统聚焦所在目录\Assets\，设置文件`WinFormsAppSetting.exe`点击“浏览”会自动识别定位，若无法识别需人工干预。  


## 修改日志  
<details>
    <summary>
        2024-01-16：上传第一个版本，添加Windows服务  
    </summary>
</details>
<details>
    <summary>
        2024-01-17：添加WinForm窗体来可视化配置参数  
    </summary>
</details>
<details>
    <summary>
        2024-01-19：完善参数配置设定，试运行服务，开始下载Bing图片和复制Windows聚焦图片
    </summary>
</details>


## 其他  

之前做过的第一个版本是控制台应用程序，配置文件是写在ini文件中，因为ini文件格式简单可读性强，还可以添加注释信息，更适合非专业人员修改，比较亲民。  
程序源代码公开在Git上：  
> *  github.com/gggplso/BingImageDownload
> *  gitee.com/gggplso/BingImageDownload

本程序改用了json文件来记录参数设置，通过WinForm来配置参数，并添加了说明    

程序运行记录了日志，大家可以通过日志文件获取到图片的下载Bing官方源路径。  

2024-01-01：本人是刚接触C#的新手，看了基础入门教程和网上其他人的代码，学习之作，若有错漏之处，请指点斧正，不胜感激。  

## 配置截图说明  
![参数设置](https://gitee.com/gggplso/MarkdownPhotos/raw/master/Photos/BingImageDownloadServer/AppSetting01.png)
![参数设置](https://gitee.com/gggplso/MarkdownPhotos/raw/master/Photos/BingImageDownloadServer/AppSetting02.png)
![参数设置](https://gitee.com/gggplso/MarkdownPhotos/raw/master/Photos/BingImageDownloadServer/AppSetting03.png)

## 服务和进程
![服务](https://gitee.com/gggplso/MarkdownPhotos/raw/master/Photos/BingImageDownloadServer/service01.png)
![进程](https://gitee.com/gggplso/MarkdownPhotos/raw/master/Photos/BingImageDownloadServer/Taskmgr01.png)