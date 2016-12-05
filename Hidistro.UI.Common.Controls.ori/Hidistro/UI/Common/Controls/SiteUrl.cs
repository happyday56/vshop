namespace Hidistro.UI.Common.Controls
{
    using Hidistro.Core;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class SiteUrl : HyperLink
    {
        private string AAZ0JeEma(2x58C7QQ)d9L4MH;
        private string ABOZyE4bfGcbd5Z8N5SO7b2;

        protected override void Render(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(base.NavigateUrl) && !string.IsNullOrEmpty(this.UrlName))
            {
                if (!string.IsNullOrEmpty(this.RequstName))
                {
                    base.NavigateUrl = Globals.GetSiteUrls().UrlData.FormatUrl(this.UrlName, new object[] { this.Page.Request.QueryString[this.RequstName] });
                }
                else
                {
                    base.NavigateUrl = Globals.GetSiteUrls().UrlData.FormatUrl(this.UrlName);
                }
            }
            base.Render(writer);
        }

        public string RequstName
        {
            get
            {
                return this.ABOZyE4bfGcbd5Z8N5SO7b2;
            }
            set
            {
                this.ABOZyE4bfGcbd5Z8N5SO7b2 = value;
            }
        }

        public string UrlName
        {
            get
            {
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH;
            }
            set
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH = value;
            }
        }
    }
}

