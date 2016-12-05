namespace Hidistro.UI.Common.Controls
{
    using Hidistro.Core;
    using System;
    using System.Globalization;
    using System.Web;
    using System.Web.UI;

    [PersistChildren(true), ParseChildren(false)]
    public class PageTitle : Control
    {
        private const string titleKey = "Hishop.Title.Value";

        public static void AddSiteNameTitle(string title)
        {
            AddTitle(string.Format(CultureInfo.InvariantCulture, "{0} - {1}", new object[] { title, SettingsManager.GetMasterSettings(true).SiteName }));
        }

        public static void AddTitle(string title)
        {
            if (HttpContext.Current == null)
            {
                throw new ArgumentNullException("context");
            }
            HttpContext.Current.Items["Hishop.Title.Value"] = title;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string siteName = this.Context.Items["Hishop.Title.Value"] as string;
            if (string.IsNullOrEmpty(siteName))
            {
                siteName = SettingsManager.GetMasterSettings(true).SiteName;
            }
            writer.WriteLine("<title>{0}</title>", siteName);
        }
    }
}

