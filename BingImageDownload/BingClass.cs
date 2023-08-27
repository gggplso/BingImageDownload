using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingImageDownload
{
    /// <summary>
    /// 【版权】仅限于壁纸使用。它们是受版权保护的图像，因此您不应将其用于其他目的，但可以将其用作桌面壁纸。
    /// </summary>
    public class BingClass
    {
        public List<BingImage>? images { get; set; }
        public BingTooltips? tooltips { get; set; }
        public override string ToString()
        {
            return string.Format($"images:{images}\r\ntooltips:{tooltips}");
        }
    }
    public class BingImage
    {
        public string? startdate { get; set; }
        public string? fullstartdate { get; set; }
        public string? enddate { get; set; }
        public string? url { get; set; }
        public string? urlbase { get; set; }
        public string? copyright { get; set; }
        public string? copyrightlink { get; set; }
        public string? title { get; set; }
        public string? quiz { get; set; }
        public bool? wp { get; set; }
        public string? hsh { get; set; }
        public int? drk { get; set; }
        public int? top { get; set; }
        public int? bot { get; set; }
        public List<BingHS>? hs { get; set; }
    }
    public class BingTooltips
    {
        public string? loading { get; set; }
        public string? previous { get; set; }
        public string? next { get; set; }
        public string? walle { get; set; }
        public string? walls { get; set; }
        public string? play { get; set; }
        public string? pause { get; set; }
        public override string ToString()
        {
            return string.Format($"tooltips:\r\n\tloading:{loading}\r\n\tprevious{previous}\r\n\tnext{next}\r\n\twalle{walle}\r\n\twalls{walls}\r\n\tplay{play}\r\n\tpause{pause}");
        }
    }
    public class BingHS
    {
        public override string ToString()
        {
            return string.Empty;
        }
    }
}
