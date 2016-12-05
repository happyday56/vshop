namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VGetRedShare : VshopTemplatedWebControl
    {
        private Literal litItemParams;

        protected override void AttachChildControls()
        {
            this.litItemParams = (Literal) this.FindControl("litItemParams");
            string str = HttpContext.Current.Request.QueryString.Get("orderid");
            if (!string.IsNullOrEmpty(str))
            {
                MemberInfo currentMember = MemberProcessor.GetCurrentMember();
                if (currentMember == null)
                {
                    return;
                }
                this.litItemParams.Text = Globals.HostPath(HttpContext.Current.Request.Url) + "/Storage/master/RedShare.jpg|" + currentMember.UserName + "的代金券|" + Globals.HostPath(HttpContext.Current.Request.Url) + "/Vshop/getredpager.aspx?orderid=" + str;
            }
            PageTitle.AddSiteNameTitle("分享代金券");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-VGetRedShare.html";
            }
            base.OnInit(e);
        }
    }
}

