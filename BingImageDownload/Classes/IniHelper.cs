using System.Runtime.InteropServices;
using System.Text;

namespace BingImageDownload.Classes
{
    /// <summary>
    /// 【版权】仅限于壁纸使用。它们是受版权保护的图像，因此您不应将其用于其他目的，但可以将其用作桌面壁纸。
    /// </summary>
    internal class IniHelper
    {
        [DllImport("kernel32.dll")]
        private static extern long GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        public static string IniRead(string section, string key, string filePath, string def = "")
        {
            string strResult = string.Empty;
            try
            {
                if (File.Exists(filePath))
                {
                    StringBuilder stringBuilder = new StringBuilder(1024);
                    GetPrivateProfileString(section, key, def, stringBuilder, 1024, filePath);
                    strResult = stringBuilder.ToString();
                }
                else
                {
                    strResult = null;
                }
            }
            catch (Exception e1)
            {
                LogHelper.GetExceptionLocation(e1);
            }
            return strResult;
        }
    }
}
