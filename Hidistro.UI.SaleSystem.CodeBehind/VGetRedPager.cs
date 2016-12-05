namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    [ParseChildren(true)]
    public class VGetRedPager : VshopTemplatedWebControl
    {
        private HtmlInputHidden hdorderid;

        protected override void AttachChildControls()
        {
            string str = HttpContext.Current.Request.QueryString.Get("orderid");
            if (!string.IsNullOrEmpty(str))
            {
                this.hdorderid = (HtmlInputHidden) this.FindControl("hdOrderID");
                this.hdorderid.Value = str;
            }
            PageTitle.AddSiteNameTitle("获取代金券");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-VGetRedPager.html";
            }
            base.OnInit(e);
        }
    }
}

