namespace Hidistro.UI.Common.Controls
{
    using Hidistro.Core;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class LicenseControl : WebControl
    {
        private readonly string AAZ0JeEma(2x58C7QQ)d9L4MH = "";

        protected override void Render(HtmlTextWriter writer)
        {
            if (!CopyrightLicenser.CheckCopyright())
            {
                writer.Write(string.Format(this.AAZ0JeEma(2x58C7QQ)d9L4MH, SettingsManager.GetMasterSettings(false).SiteUrl));
            }
        }
    }
}

