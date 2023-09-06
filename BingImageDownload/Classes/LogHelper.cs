using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingImageDownload.Classes
{
    /// <summary>
    /// 【版权】仅限于壁纸使用。它们是受版权保护的图像，因此您不应将其用于其他目的，但可以将其用作桌面壁纸。
    /// </summary>
    public class LogHelper
    {
        #region 日志类型(枚举)
        public enum LogType
        {
            Emergency = 0,
            Alert = 1,
            Critical = 2,
            Error = 3,
            Warning = 4,
            Notice = 5,
            Infomational = 6,
            Debug = 7,
        }
        #endregion

        #region 异常信息捕获(静态)
        public static string[] GetExceptionLocation(Exception ex)
        {
            string[] strings = new string[4];
            StackTrace stackTrace = new StackTrace(ex, true);
            StackFrame stackFrame = stackTrace.GetFrame(stackTrace.FrameCount - 1);
            strings[3] = stackFrame.GetFileColumnNumber().ToString();
            strings[2] = stackFrame.GetFileLineNumber().ToString();
            strings[1] = stackFrame.GetFileName();
            strings[0] = $"在文件{strings[1]}中的第{strings[2]}行第{strings[3]}列引发异常：{ex.Message}";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("".PadRight(30, '*'));
            sb.AppendLine("异常发生时间：" + DateTime.Now.ToString("yyyyMMddHHmmss.fffffff"));
            sb.AppendLine($"异常类型：{ex.HResult}");
            sb.AppendLine(string.Format("导致当前异常的 Exception 实例：{0}", ex.InnerException));
            sb.AppendLine(@"导致异常的应用程序或对象的名称：" + ex.Source);
            sb.Append("引发异常的方法：");
            sb.AppendLine(ex.TargetSite.ToString());
            sb.AppendFormat("异常堆栈信息：{0} \r\n", ex.StackTrace);
            sb.AppendFormat("异常消息：{0}", ex.Message);
            sb.AppendLine("\r\n 异常位置：" + strings[0]);
            sb.Append("".PadLeft(30, '*'));

            LogSplit(strContent: sb.ToString(), logType: LogType.Error);
            return strings;
        }
        #endregion

        #region 日志写入方法(静态)
        public static void Log(string strPath = "Logs\\", string strContent = "", LogType logType = LogType.Infomational)
        {
            string strInvalid = new string(Path.GetInvalidPathChars());
            foreach (char item in strInvalid)
            {
                strPath = strPath.Replace(item.ToString(), "");
            }
            if (string.IsNullOrEmpty(strPath) || strPath.Trim().Length < 1)
            {
                strPath = @"Logs\";
            }
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            string fileName = logType.ToString() + "Log" + DateTime.Now.ToString("yyyyMM");
            string[] fileOldLogs = Directory.GetFiles(strPath, fileName + "*.txt", SearchOption.TopDirectoryOnly);
            if (fileOldLogs.Length < 1)
            {
                fileName += ".txt";
            }
            else
            {
                FileInfo fileInfo;
                for (int i = 0; i < fileOldLogs.Length; i++)
                {
                    fileInfo = new FileInfo(fileOldLogs[i]);
                    if (fileInfo.Length >= 1024 * 1024 * 1 && i == fileOldLogs.Length - 1)
                    {
                        fileName += (i + 1).ToString() + ".txt";
                        break;
                    }
                    else if (fileInfo.Length >= 1024 * 1024 * 1)
                    {
                        continue;
                    }
                    else
                    {
                        fileName = fileInfo.Name;
                        break;
                    }
                }
            }
            try
            {
                using (FileStream fsWrite = new FileStream(Path.Combine(strPath.EndsWith("\\") ? strPath : strPath + "\\", fileName.EndsWith(".txt") ? fileName : fileName + ".txt"), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    strContent = DateTime.Now.ToString("yyyyMMddHHmmss.fffffff") + " ：" + strContent;
                    byte[] buffer = Encoding.UTF8.GetBytes(strContent + "\r\n");
                    fsWrite.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception e1)
            {
                GetExceptionLocation(e1);
            }
        }
        public static void LogSplit(string strPath = "Logs\\", string strContent = "", LogType logType = LogType.Infomational)
        {
            string str = "".PadRight(30, '*') + "\r\n";
            strContent = "\r\n" + str + "\r\n" + strContent + "\r\n\r\n" + str;
            Log(strPath, strContent, logType);
        }
        public static void Log(string strContent)
        {
            Log(strPath: "Logs\\", strContent, logType: LogType.Infomational);
        }
        public static void LogSplit(string strContent)
        {
            LogSplit(strPath: "Logs\\", strContent, logType: LogType.Infomational);
        }
        #endregion
    }
}
