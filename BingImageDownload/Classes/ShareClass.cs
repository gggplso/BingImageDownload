using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace BingImageDownload.Classes
{
    /// <summary>
    /// 【版权】仅限于壁纸使用。它们是受版权保护的图像，因此您不应将其用于其他目的，但可以将其用作桌面壁纸。
    /// </summary>
    internal class ShareClass
    {
        public static readonly string _iniFile = "bing.ini";
        public static readonly string _iniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ShareClass._iniFile);
        public static readonly HttpClient _client = new HttpClient();

        public static Dictionary<string, List<string>> GetJsonToBing(string strJson, bool isUHD = true)
        {
            Dictionary<string, List<string>> dicEveryDayInformation = new Dictionary<string, List<string>>();
            try
            {
                BingClass bingclass = JsonConvert.DeserializeObject<BingClass>(strJson);
                if (!(bingclass is null))
                {
                    for (int i = 0; i < bingclass.images.Count; i++)
                    {
                        List<string> listInformation = new List<string>();
                        string strStart = "/th?id=OHR.";
                        string strEnd = "&rf=";
                        string strUrl = string.Concat("https://www.bing.com", bingclass.images[i].url.AsSpan(0, bingclass.images[i].url.IndexOf(strEnd)));
                        string strEnglish = bingclass.images[i].url.Substring(strStart.Length, bingclass.images[i].url.IndexOf(strEnd) - strStart.Length);
                        if (isUHD)
                        {
                            strUrl = strUrl.Substring(0, strUrl.LastIndexOf('_')) + "_UHD.jpg";
                            strEnglish = strEnglish.Substring(0, strEnglish.LastIndexOf('_')) + "_UHD.jpg";
                        }
                        listInformation.Add(strUrl);
                        listInformation.Add(strEnglish);
                        string strChinese = bingclass.images[i].copyright.Substring(0, bingclass.images[i].copyright.IndexOf(" (© ")) + ".jpg";
                        listInformation.Add(strChinese);
                        //因为说明也会加入到文件名中，所以也需要去除非法字符
                        listInformation.Add(bingclass.images[i].title);
                        dicEveryDayInformation.Add(bingclass.images[i].enddate, listInformation);
                    }
                }
            }
            catch (Exception e1)
            {
                LogHelper.GetExceptionLocation(e1);
            }
            return dicEveryDayInformation;
        }
        #region 异步会导致文件下载不全 不知道怎么解决这个问题（图片下载了部分，打开后只显示了一截图片）
        //public static async Task DownloadFile(string strUrl, string filePath)
        //{
        //    try
        //    {
        //        filePath = string.IsNullOrEmpty(filePath) ? DateTime.Now.ToString("yyyy-MM-dd.jpg") : filePath;
        //        Stream stream = await _client.GetStreamAsync(strUrl);
        //        using (FileStream fsWrite = File.Create(filePath))
        //        {
        //            await stream.CopyToAsync(fsWrite);
        //        }
        //    }
        //    catch (Exception e1)
        //    {
        //        LogHelper.GetExceptionLocation(e1);
        //    }
        //} 
        #endregion
        public static void DownloadFile(string strUrl, string filePath)
        {
            try
            {
                System.IO.Stream stream = _client.GetStreamAsync(strUrl).GetAwaiter().GetResult();
                using (FileStream fsWrite = System.IO.File.Create(filePath))
                {
                    stream.CopyTo(fsWrite);
                }
            }
            catch (Exception e1)
            {
                LogHelper.GetExceptionLocation(e1);
            }
        }
        /// <summary>
        /// 去除不允许在路径、文件名中使用的字符
        /// </summary>
        /// <param name="strSource">需要去除的源字符串</param>
        /// <returns></returns>
        public static string RemoveInvalidChars(string strSource)
        {
            string regSearch = new string(Path.GetInvalidPathChars()) + new string(Path.GetInvalidFileNameChars());
            Regex rg = new Regex(string.Format("[{0}]", Regex.Escape(regSearch)));
            return rg.Replace(strSource, " ");
        }
    }
}
