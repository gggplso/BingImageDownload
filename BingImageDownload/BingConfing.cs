namespace BingImageDownload
{
    internal class BingConfing
    {
        public BingApiUrl BingApiUrl { get; set; }
        public DownloadSetting DownloadSetting { get; set; }
        public NetworkInformation NetworkInformation { get; set; }
        public OtherSetting OtherSetting { get; set; }
    }
    internal class BingApiUrl
    {
        public string Url1 { get; set; }
        public string Url2 { get; set; }
        public string Url3 { get; set; }
        public string Url4 { get; set; }
        public string Url5 { get; set; }
        public string Url6 { get; set; }
        public int DaysAgo { get; set; }
        public int AfewDays { get; set; }
    }
    internal class DownloadSetting
    {
        public string PixelResolution { get; set; }
        public bool FileNameLanguageIsEnglish { get; set; }
        public string? DownloadPath { get; set; }
    }
    internal class NetworkInformation
    {
        public int NetWaitTime { get; set; }
        public int NetRetryCount { get; set; }
    }
    internal class OtherSetting
    {
        public int AutoExit { get; set; }
        public int ExitTime { get; set; }
    }
}
