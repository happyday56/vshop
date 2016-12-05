namespace Hidistro.Core
{
    using Hidistro.Core.Entities;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Caching;
    using System.Xml;

    public static class SettingsManager
    {
        private const string MasterSettingsCacheKey = "FileCache-MasterSettings";

        private const string WXSettingsCacheKey = "FileCache-WXSettings";

        #region "主文件配置信息"

        public static SiteSettings GetMasterSettings(bool cacheable)
        {
            if (!cacheable)
            {
                HiCache.Remove("FileCache-MasterSettings");
            }
            XmlDocument document = HiCache.Get("FileCache-MasterSettings") as XmlDocument;
            if (document == null)
            {
                string masterSettingsFilename = GetMasterSettingsFilename();
                if (!File.Exists(masterSettingsFilename))
                {
                    return null;
                }
                document = new XmlDocument();
                document.Load(masterSettingsFilename);
                if (cacheable)
                {
                    HiCache.Max("FileCache-MasterSettings", document, new CacheDependency(masterSettingsFilename));
                }
            }
            return SiteSettings.FromXml(document);
        }

        private static string GetMasterSettingsFilename()
        {
            HttpContext current = HttpContext.Current;
            return ((current != null) ? current.Request.MapPath("~/config/SiteSettings.config") : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config\SiteSettings.config"));
        }

        public static void Save(SiteSettings settings)
        {
            SaveMasterSettings(settings);
            HiCache.Remove("FileCache-MasterSettings");
        }

        private static void SaveMasterSettings(SiteSettings settings)
        {
            string masterSettingsFilename = GetMasterSettingsFilename();
            XmlDocument doc = new XmlDocument();
            if (File.Exists(masterSettingsFilename))
            {
                doc.Load(masterSettingsFilename);
            }
            settings.WriteToXml(doc);
            doc.Save(masterSettingsFilename);
        }

        #endregion "主文件配置信息"

        #region "微信配置信息"

        public static WXSettings GetWXSettings(bool cacheable)
        {
            if (!cacheable)
            {
                HiCache.Remove(WXSettingsCacheKey);
            }
            XmlDocument document = HiCache.Get(WXSettingsCacheKey) as XmlDocument;
            if (null == document)
            {
                string wxSettingsFilename = GetWXSettingsFilename();
                if (!File.Exists(wxSettingsFilename))
                {
                    return null;
                }
                document = new XmlDocument();
                document.Load(wxSettingsFilename);
                if (cacheable)
                {
                    HiCache.Max(WXSettingsCacheKey, document, new CacheDependency(wxSettingsFilename));
                }
            }
            return WXSettings.FromXml(document);
        }

        private static void Save(WXSettings settings)
        {
            SaveWXSettings(settings);
            HiCache.Remove(WXSettingsCacheKey);
        }

        private static void SaveWXSettings(WXSettings settings)
        {
            string wxSettingsFilename = GetWXSettingsFilename();
            XmlDocument doc = new XmlDocument();
            if (File.Exists(wxSettingsFilename))
            {
                doc.Load(wxSettingsFilename);
            }
            settings.WriteToXml(doc);
            doc.Save(wxSettingsFilename);
        }

        private static string GetWXSettingsFilename()
        {
            HttpContext current = HttpContext.Current;
            return ((null != current) ? current.Request.MapPath("~/config/WXSettings.config") : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config\WXSettings.config"));
        }

        #endregion "微信配置信息"

    }
}

