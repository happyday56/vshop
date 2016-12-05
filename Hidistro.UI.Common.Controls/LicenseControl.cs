namespace Hidistro.UI.Common.Controls
{
    using Hidistro.Core;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class LicenseControl : WebControl
    {
        private readonly string renderFormat = "";

        protected override void Render(HtmlTextWriter writer)
        {
            if (!CopyrightLicenser.CheckCopyright())
            {
                writer.Write(string.Format(this.renderFormat, SettingsManager.GetMasterSettings(false).SiteUrl));
            }
        }
    }
}

