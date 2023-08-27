using BingImageDownload.Classes;
using System.Text;

namespace BingImageDownload
{
    internal class Program
    {
        /// <summary>
        /// 【版权】仅限于壁纸使用。它们是受版权保护的图像，因此您不应将其用于其他目的，但可以将其用作桌面壁纸。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Bing每日图片下载(仅限用作桌面壁纸)";
                if (!File.Exists(ShareClass._iniFilePath))
                {
                    Console.WriteLine("未找到配置文件，开始初始化程序……");
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("【版权】仅限于壁纸使用。它们是受版权保护的图像，因此您不应将其用于其他目的，但可以将其用作桌面壁纸。");
                    stringBuilder.AppendLine("【说明】\nPixelResolution=UHD表示下载高分辨率的图片，比如3840x2160，其他值为默认的，比如1920x1080。\nFileNameLanguageIsEnglish=false表示文件名用中文，=true表示文件名用英文。\nDownloadPath=设置保存的路径，不配置则保存到程序所在目录。\nAutoExit=1表示程序运行完自动退出。ExitTime=3000表示退出时等待时间为3秒。");
                    stringBuilder.AppendLine("【辅助】Win11系统添加到开机启动项：\n在本程序文件BingImageDownload.exe点击右键-发送到桌面快捷方式。\n在系统开始菜单上点击右键-运行，输入shell:startup回车确定系统自动打开一文件夹：开始菜单-程序-启动.\\Start Menu\\Programs\\Startup\n将刚才桌面上创建的快捷方式拖入到此文件夹中即可。");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine("[BingApiUrl]");
                    stringBuilder.AppendLine("url1=https://www.bing.com/HPImageArchive.aspx?format=js&cc=cn&idx=");
                    stringBuilder.AppendLine("url2=&n=");
                    stringBuilder.AppendLine("url3=&video=1");
                    stringBuilder.AppendLine("DaysAgo=0");
                    stringBuilder.AppendLine("AFewDays=1");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine("[DownloadSetting]");
                    stringBuilder.AppendLine("PixelResolution=UHD");
                    stringBuilder.AppendLine("FileNameLanguageIsEnglish=false");
                    stringBuilder.AppendLine("DownloadPath=");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine("[OtherSetting]");
                    stringBuilder.AppendLine("AutoExit=1");
                    stringBuilder.AppendLine("ExitTime=3000");
                    File.WriteAllText(ShareClass._iniFilePath, stringBuilder.ToString(), Encoding.UTF8);

                    if (File.Exists(ShareClass._iniFilePath))
                    {
                        Console.WriteLine("初始化完成。");
                    }
                    else
                    {
                        Console.WriteLine("初始化失败。");
                        return;
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Hello World!");
                Console.WriteLine();
                Console.WriteLine("配置文件读取中……");
                int intIDX;
                int intN;
                if (Int32.TryParse(IniHelper.IniRead("BingApiUrl", "DaysAgo", ShareClass._iniFilePath, "0"), out intIDX))
                {
                    intIDX = intIDX < 0 ? 0 : (intIDX > 7 ? 7 : intIDX);
                }
                else
                {
                    intIDX = 0;
                }
                if (Int32.TryParse(IniHelper.IniRead("BingApiUrl", "AFewDays", ShareClass._iniFilePath, "1"), out intN))
                {
                    intN = intN < 0 ? 0 : (intN > 8 ? 8 : intN);
                }
                else
                {
                    intN = 1;
                }
                string strUrl = IniHelper.IniRead("BingApiUrl", "url1", ShareClass._iniFilePath, "https://www.bing.com/HPImageArchive.aspx?format=js&cc=cn&idx=")
                              + intIDX.ToString()
                              + IniHelper.IniRead("BingApiUrl", "url2", ShareClass._iniFilePath, "&n=")
                              + intN.ToString()
                              + IniHelper.IniRead("BingApiUrl", "url3", ShareClass._iniFilePath, "&video=1");
                bool isUHD = true;
                if (IniHelper.IniRead("DownloadSetting", "PixelResolution", ShareClass._iniFilePath, "UHD") != "UHD")
                {
                    isUHD = false;
                }
                string strJson = HttpHelper.Get(strUrl);
                Dictionary<string, List<string>> dicEveryDayInformation = ShareClass.GetJsonToBing(strJson, isUHD);
                string fileNameLanguageIsEnglish = IniHelper.IniRead("DownloadSetting", "FileNameLanguageIsEnglish", ShareClass._iniFilePath, "true");
                int intLanguage;
                if (fileNameLanguageIsEnglish == "true")
                {
                    intLanguage = 1;
                }
                else
                {
                    intLanguage = 2;
                }
                string strPath = IniHelper.IniRead("DownloadSetting", "DownloadPath", ShareClass._iniFilePath, null);
                string strPathDefault = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Picture");
                strPath = string.IsNullOrEmpty(strPath) ? strPathDefault : strPath;
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                Console.WriteLine("配置文件读取完毕，开始下载文件……");
                string strStatus = string.Empty;
                foreach (List<string> item in dicEveryDayInformation.Values)
                {
                    Console.WriteLine(item[3]);
                    ShareClass.DownloadFile(item[0], Path.Combine(strPath, (item[3] + " " + item[intLanguage])));
                    if (dicEveryDayInformation.Count > 1)
                    {
                        Thread.Sleep(5000);
                    }
                    strStatus = $"文件 ：{item[intLanguage]}\n下载完成." + item[0];
                    LogHelper.Log(strStatus);
                    Console.WriteLine(strStatus);
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("【版权】仅限于壁纸使用。它们是受版权保护的图像，因此您不应将其用于其他目的，但可以将其用作桌面壁纸。");
                Console.ForegroundColor = default;
                Console.WriteLine();
                Console.WriteLine("文件下载完毕，请到你指定的文件夹中查看，程序即将退出。");
                int intWait;
                intWait = Int32.TryParse(IniHelper.IniRead("OtherSetting", "ExitTime", ShareClass._iniFilePath, "3000"), out intWait) ? intWait : 3000;
                Thread.Sleep(intWait);
                if (IniHelper.IniRead("OtherSetting", "AutoExit", ShareClass._iniFilePath) == "1")
                {
                    Console.WriteLine("配置文件中设置了自动退出。");
                }
                else
                {
                    Console.WriteLine("\n(若想程序自动退出，可以在配置文件中将AutoExit的值设置为1)\n按任意键退出程序。");
                    Console.ReadKey(true);
                }
            }
            catch (Exception e1)
            {
                LogHelper.GetExceptionLocation(e1);
            }
        }
    }
}