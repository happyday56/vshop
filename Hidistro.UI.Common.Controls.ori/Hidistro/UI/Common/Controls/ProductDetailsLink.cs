namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ProductDetailsLink : HyperLink
    {
        [CompilerGenerated]
        private bool AAZ0JeEma(2x58C7QQ)d9L4MH;
        [CompilerGenerated]
        private object ABOZyE4bfGcbd5Z8N5SO7b2;
        public const string TagID = "ProductDetailsLink";

        public ProductDetailsLink()
        {
            base.ID = "ProductDetailsLink";
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if ((this.ProductId != null) && (this.ProductId != DBNull.Value))
            {
                base.NavigateUrl = Globals.GetSiteUrls().UrlData.FormatUrl(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cAByAG8AZAB1AGMAdABEAGUAdABhAGkAbABzAA=="), new object[] { this.ProductId });
                string str = Globals.ApplicationPath + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBWAHMAaABvAHAALwBQAHIAbwBkAHUAYwB0AEQAZQB0AGEAaQBsAHMALgBhAHMAcAB4AD8AUAByAG8AZAB1AGMAdABJAGQAPQA=") + this.ProductId;
                if (Globals.GetCurrentDistributorId() > 0)
                {
                    base.NavigateUrl = str + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JgAmAFIAZQBmAGUAcgByAGEAbABJAGQAPQA=") + Globals.GetCurrentDistributorId();
                }
            }
            base.Target = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBiAGwAYQBuAGsA");
            base.Render(writer);
        }

        public bool ImageLink
        {
            [CompilerGenerated]
            get
            {
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH;
            }
            [CompilerGenerated]
            set
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH = value;
            }
        }

        public object ProductId
        {
            [CompilerGenerated]
            get
            {
                return this.ABOZyE4bfGcbd5Z8N5SO7b2;
            }
            [CompilerGenerated]
            set
            {
                this.ABOZyE4bfGcbd5Z8N5SO7b2 = value;
            }
        }
    }
}

