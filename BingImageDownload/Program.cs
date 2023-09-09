using BingImageDownload.Classes;
using System.Diagnostics;
using System.Net.NetworkInformation;
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
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                if (!File.Exists(ShareClass._iniFilePath))
                {
                    Console.WriteLine("未找到配置文件，开始初始化程序……");
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("【版权】仅限于壁纸使用。它们是受版权保护的图像，因此您不应将其用于其他目的，但可以将其用作桌面壁纸。");
                    stringBuilder.AppendLine("【说明】\nurl6=&qlt=100表示下载同分辨率下的大文件，若不想下载大文件则留空。\nPixelResolution=UHD表示下载高分辨率的图片，比如3840x2160，其他值为默认的，比如1920x1080。\nFileNameLanguageIsEnglish=false表示文件名用中文，=true表示文件名用英文。\nDownloadPath=设置保存的路径，不配置则保存到程序所在目录。\nNetWaitTime=2000表示若网络中断尝试重新连接等待的时间为2秒。NetRetryCount=5表示连接网络最大重试次数为5次。\nAutoExit=true表示程序运行完自动退出。ExitTime=3000表示退出时等待时间为3秒。");
                    stringBuilder.AppendLine("【辅助】Win11系统添加到开机启动项：\n在本程序文件BingImageDownload.exe点击右键-发送到桌面快捷方式。\n在系统开始菜单上点击右键-运行，输入shell:startup回车确定系统自动打开一文件夹：开始菜单-程序-启动.\\Start Menu\\Programs\\Startup\n将刚才桌面上创建的快捷方式拖入到此文件夹中即可。");
                    stringBuilder.AppendLine("【额外】添加了复制Windows聚焦图片到指定的目录。");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine("[BingApiUrl]");
                    stringBuilder.AppendLine("url1=https://");
                    stringBuilder.AppendLine("url2=www.bing.com");
                    stringBuilder.AppendLine("url3=/HPImageArchive.aspx?format=js&cc=cn&idx=");
                    stringBuilder.AppendLine("url4=&n=");
                    stringBuilder.AppendLine("url5=&video=1");
                    stringBuilder.AppendLine("url6=&qlt=100");
                    stringBuilder.AppendLine("DaysAgo=0");
                    stringBuilder.AppendLine("AFewDays=1");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine("[DownloadSetting]");
                    stringBuilder.AppendLine("PixelResolution=UHD");
                    stringBuilder.AppendLine("FileNameLanguageIsEnglish=false");
                    stringBuilder.AppendLine("DownloadPath=");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine("[NetworkInformation]");
                    stringBuilder.AppendLine("NetWaitTime=2000");
                    stringBuilder.AppendLine("NetRetryCount=5");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine("[OtherSetting]");
                    stringBuilder.AppendLine("AutoExit=true");
                    stringBuilder.AppendLine("ExitTime=3000");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine("[WindowsSpotlight]");
                    stringBuilder.AppendLine("Switch=on");
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
                string strDomain = IniHelper.IniRead("BingApiUrl", "url2", ShareClass._iniFilePath, "www.bing.com");
                string strUrl = IniHelper.IniRead("BingApiUrl", "url1", ShareClass._iniFilePath, "https://")
                              + strDomain
                              + IniHelper.IniRead("BingApiUrl", "url3", ShareClass._iniFilePath, "/HPImageArchive.aspx?format=js&cc=cn&idx=")
                              + intIDX.ToString()
                              + IniHelper.IniRead("BingApiUrl", "url4", ShareClass._iniFilePath, "&n=")
                              + intN.ToString()
                              + IniHelper.IniRead("BingApiUrl", "url5", ShareClass._iniFilePath, "&video=1");
                bool isUHD = true;
                if (IniHelper.IniRead("DownloadSetting", "PixelResolution", ShareClass._iniFilePath, "UHD") != "UHD")
                {
                    isUHD = false;
                }
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
                int intNetWaitTime;
                intNetWaitTime = Int32.TryParse(IniHelper.IniRead("NetworkInformation", "NetWaitTime", ShareClass._iniFilePath, "2000"), out intNetWaitTime) ? intNetWaitTime : 2000;
                int intNetRetryCount;
                intNetRetryCount = Int32.TryParse(IniHelper.IniRead("NetworkInformation", "NetRetryCount", ShareClass._iniFilePath, "5"), out intNetRetryCount) ? intNetRetryCount : 5;
                int intExitTime;
                intExitTime = Int32.TryParse(IniHelper.IniRead("OtherSetting", "ExitTime", ShareClass._iniFilePath, "3000"), out intExitTime) ? intExitTime : 3000;
                string strUrl6 = IniHelper.IniRead("BingApiUrl", "url6", ShareClass._iniFilePath, "");
                string strSpotlight = IniHelper.IniRead("WindowsSpotlight", "Switch", ShareClass._iniFilePath);

                Console.WriteLine("配置文件读取完毕，开始测试网络连接……");
                bool isSuccess = false;
                int intCount = 0;
                while (!isSuccess && intCount < intNetRetryCount)
                {
                    Ping pingSender = new Ping();
                    PingReply pingReply = null;
                    try
                    {
                        pingReply = pingSender.Send(strDomain, 1000);
                    }
                    catch (Exception e1)
                    {
                        LogHelper.GetExceptionLocation(e1);
                    }
                    finally
                    {
                        if (pingReply == null || (pingReply != null && pingReply.Status != IPStatus.Success))
                        {
                            Console.WriteLine($"无法连接，请检查网络。重新尝试{(intCount + 1).ToString()}");
                            intCount++;
                            Thread.Sleep(intNetWaitTime);
                        }
                        else if (pingReply != null && pingReply.Status == IPStatus.Success)
                        {
                            Console.WriteLine("连接成功");
                            isSuccess = true;
                        }
                    }
                }
                if (intCount > 0 && intCount >= intNetRetryCount)
                {
                    Console.WriteLine($" {strDomain} 未响应，可能会影响文件下载，请检查网络。");
                    isSuccess = false;
                }
                else
                {
                    Console.WriteLine("网络连接通畅，开始下载文件……");
                }
                string strJson = ShareClass._client.GetStringAsync(strUrl).GetAwaiter().GetResult();
                Dictionary<string, List<string>> dicEveryDayInformation = ShareClass.GetJsonToBing(strJson, isUHD);
                string strStatus = string.Empty;
                isSuccess = dicEveryDayInformation.Count < 1 ? false : isSuccess;
                string strDownloadUrl, strNewFileName;
                DateTime dtStart, dtEnd;
                Stopwatch stopwatch = new Stopwatch();
                int intBingCount = 0;
                stopwatch.Start();
                foreach (List<string> item in dicEveryDayInformation.Values)
                {
                    try
                    {
                        strDownloadUrl = item[0] + strUrl6;
                        strNewFileName = item[3] + " " + item[intLanguage];
                        dtStart = DateTime.Now;
                        //Task.Run(async delegate () { await ShareClass.DownloadFile(strDownloadUrl, Path.Combine(strPath, strNewFileName)).ConfigureAwait(false); });
                        ShareClass.DownloadFile(strDownloadUrl, Path.Combine(strPath, strNewFileName));
                        dtEnd = DateTime.Now;
                        strStatus = string.Format($"{strNewFileName}\n下载完成." + strDownloadUrl + " 耗时：{0}秒", dtEnd.Subtract(dtStart).TotalSeconds);
                        LogHelper.Log(strStatus);
                        Console.WriteLine(strStatus);
                        isSuccess = true;
                    }
                    catch (Exception e1)
                    {
                        LogHelper.GetExceptionLocation(e1);
                        isSuccess = false;
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("文件下载出错，请到程序目录的Log文件中查看日志文件，分析出错原因。");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    intBingCount++;
                }
                stopwatch.Stop();
                strStatus = string.Format("共下载Bing壁纸{0}个文件，总耗时{1}秒", intBingCount, stopwatch.Elapsed.TotalSeconds);
                LogHelper.Log(strStatus);
                Console.WriteLine(strStatus);

                if (intBingCount > 0)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("【版权】仅限于壁纸使用。它们是受版权保护的图像，因此您不应将其用于其他目的，但可以将其用作桌面壁纸。");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("文件下载完毕，请到你指定的文件夹中查看。");
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("文件下载出错，请到程序目录的Log文件中查看日志文件，分析出错原因。");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (strSpotlight == "on")
                {
                    Console.WriteLine("开始复制Windows聚焦图片……");
                    int intSpotlightCount = 0;
                    string strMessage = string.Empty;
                    string strSourcePath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\AppData\Local\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets\");
                    if (Directory.Exists(strSourcePath))
                    {
                        Directory.GetFiles(strSourcePath, "*", SearchOption.TopDirectoryOnly).ToList().ForEach(x =>
                        {
                            string strFileName = Path.GetFileName(x);
                            if (new FileInfo(x).Length > (1024 * 50))
                            {
                                using (FileStream fsRead = new FileStream(x, FileMode.Open))
                                {
                                    byte[] buffer = new byte[1024 * 1024 * 1];
                                    using (FileStream fsWrite = new FileStream(Path.Combine(strPath, (strFileName + ".jpg")), FileMode.Create))
                                    {
                                        while (true)
                                        {
                                            int i = fsRead.Read(buffer, 0, buffer.Length);
                                            if (i > 0)
                                            {
                                                fsWrite.Write(buffer, 0, i);
                                            }
                                            else
                                            {
                                                Console.WriteLine("复制完成。");
                                                intSpotlightCount++;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        });
                    }
                    if (intSpotlightCount > 0)
                    {
                        strMessage = string.Format("Windows聚焦图片复制完成，共{0}个文件。", intSpotlightCount);
                    }
                    else
                    {
                        strMessage = "Windows聚焦目录图片不存在，请检查你的操作系统是否支持锁屏设置为Windows聚焦。";
                    }
                    LogHelper.Log(strMessage);
                    Console.WriteLine(strMessage);
                    Console.WriteLine();
                }
                Console.WriteLine("程序即将退出");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(intExitTime);
                if (IniHelper.IniRead("OtherSetting", "AutoExit", ShareClass._iniFilePath) == "true")
                {
                    Console.WriteLine("配置文件中设置了自动退出。");
                }
                else
                {
                    Console.WriteLine("\n(若想程序自动退出，可以在配置文件中将AutoExit的值设置为true)\n按任意键退出程序。");
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