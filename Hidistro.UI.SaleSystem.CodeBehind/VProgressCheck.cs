namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Entities.Members;
    using Hidistro.Entities.Orders;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web.UI;

    [ParseChildren(true)]
    public class VProgressCheck : VWeiXinOAuthTemplatedWebControl
    {
        private VshopTemplatedRepeater rptOrderReturns;

        protected override void AttachChildControls()
        {
            PageTitle.AddSiteNameTitle("进度查询");
            new OrderQuery();
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            this.rptOrderReturns = (VshopTemplatedRepeater) this.FindControl("rptOrderReturns");
            this.rptOrderReturns.DataSource = ShoppingProcessor.GetOrderReturnTable(currentMember.UserId, "");
            this.rptOrderReturns.DataBind();
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VProgressCheck.html";
            }
            base.OnInit(e);
        }
    }
}

