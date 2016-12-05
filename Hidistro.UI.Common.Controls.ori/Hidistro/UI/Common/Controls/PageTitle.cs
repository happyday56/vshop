namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using System;
    using System.Globalization;
    using System.Web;
    using System.Web.UI;

    [ParseChildren(false), PersistChildren(true)]
    public class PageTitle : Control
    {
        private const string AAZ0JeEma(2x58C7QQ)d9L4MH = "Hishop.Title.Value";

        public static void AddSiteNameTitle(string title)
        {
            AddTitle(string.Format(CultureInfo.InvariantCulture, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ewAwAH0AIAAtACAAewAxAH0A"), new object[] { title, SettingsManager.GetMasterSettings(true).SiteName }));
        }

        public static void AddTitle(string title)
        {
            if (HttpContext.Current == null)
            {
                throw new ArgumentNullException(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YwBvAG4AdABlAHgAdAA="));
            }
            HttpContext.Current.Items[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("SABpAHMAaABvAHAALgBUAGkAdABsAGUALgBWAGEAbAB1AGUA")] = title;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string siteName = this.Context.Items[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("SABpAHMAaABvAHAALgBUAGkAdABsAGUALgBWAGEAbAB1AGUA")] as string;
            if (string.IsNullOrEmpty(siteName))
            {
                siteName = SettingsManager.GetMasterSettings(true).SiteName;
            }
            writer.WriteLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PAB0AGkAdABsAGUAPgB7ADAAfQA8AC8AdABpAHQAbABlAD4A"), siteName);
        }
    }
}

