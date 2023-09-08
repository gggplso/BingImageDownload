using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BingImageDownload.Classes
{
    /// <summary>
    /// 【版权】仅限于壁纸使用。它们是受版权保护的图像，因此您不应将其用于其他目的，但可以将其用作桌面壁纸。
    /// </summary>
    internal class HttpHelper
    {
        //public static string Get(string Url)
        //{
        //    string retString = string.Empty;
        //    try
        //    {
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
        //        request.Proxy = null;
        //        request.KeepAlive = false;
        //        request.Method = "GET";
        //        request.ContentType = "application/json;charset=UTF-8";
        //        request.AutomaticDecompression = DecompressionMethods.GZip;

        //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //        Stream myResponseStream = response.GetResponseStream();
        //        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
        //        retString = myStreamReader.ReadToEnd();

        //        myStreamReader.Close();
        //        myResponseStream.Close();

        //        if (response != null)
        //        {
        //            response.Close();
        //        }
        //        if (request != null)
        //        {
        //            request.Abort();
        //        }
        //    }
        //    catch (Exception e1)
        //    {
        //        LogHelper.GetExceptionLocation(e1);
        //    }
        //    return retString;
        //}
        //public static string Post(string url, string Data, string Referer)
        //{
        //    string retString = string.Empty;
        //    try
        //    {
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //        request.Method = "POST";
        //        request.Referer = Referer;
        //        byte[] bytes = Encoding.UTF8.GetBytes(Data);
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.ContentLength = bytes.Length;
        //        Stream myResponseStream = request.GetRequestStream();
        //        myResponseStream.Write(bytes, 0, bytes.Length);

        //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //        StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
        //        retString = myStreamReader.ReadToEnd();

        //        myStreamReader.Close();
        //        myResponseStream.Close();

        //        if (response != null)
        //        {
        //            response.Close();
        //        }
        //        if (request != null)
        //        {
        //            request.Abort();
        //        }
        //    }
        //    catch (Exception e1)
        //    {
        //        LogHelper.GetExceptionLocation(e1);
        //    }
        //    return retString;
        //}
    }
}
