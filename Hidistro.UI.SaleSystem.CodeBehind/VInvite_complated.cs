namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VInvite_complated : VWeiXinOAuthTemplatedWebControl
    {
        protected override void AttachChildControls()
        {
            
            PageTitle.AddSiteNameTitle("注册完成");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vInvite_complated.html";
            }
            base.OnInit(e);
        }
    }
}

