using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        public static void DownloadFile(string strUrl, string strFile)
        {
            try
            {
                Task.Run(async delegate ()
                {
                    using (Stream stream = await ShareClass._client.GetStreamAsync(strUrl))
                    {
                        using (FileStream fs = File.Create(string.IsNullOrEmpty(strFile) ? DateTime.Now.ToString("yyyy-MM-dd.jpg") : strFile))
                        {
                            await stream.CopyToAsync(fs);
                        }
                    }
                });
            }
            catch (Exception e1)
            {
                LogHelper.GetExceptionLocation(e1);
            }
        }
    }
}
