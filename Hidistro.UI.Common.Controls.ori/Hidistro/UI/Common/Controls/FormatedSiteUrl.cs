namespace Hidistro.UI.Common.Controls
{
    using Hidistro.Core;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class FormatedSiteUrl : HyperLink
    {
        private string AAZ0JeEma(2x58C7QQ)d9L4MH;
        private string ABOZyE4bfGcbd5Z8N5SO7b2;
        private object AC2pFbvHCa1v4C1DIZ8SngolueQb;

        protected override void OnDataBinding(EventArgs e)
        {
            this.AC2pFbvHCa1v4C1DIZ8SngolueQb = DataBinder.Eval(this.Page.GetDataItem(), this.DataField);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if ((string.IsNullOrEmpty(base.NavigateUrl) && !string.IsNullOrEmpty(this.UrlName)) && (this.AC2pFbvHCa1v4C1DIZ8SngolueQb != null))
            {
                base.NavigateUrl = Globals.GetSiteUrls().UrlData.FormatUrl(this.UrlName, new object[] { this.AC2pFbvHCa1v4C1DIZ8SngolueQb });
            }
            base.Render(writer);
        }

        public string DataField
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

